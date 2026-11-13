using System.Text.RegularExpressions;

namespace CyberSafetyChatbotGUI.Services
{
    public sealed class NlpProcessor
    {
        public IntentResult DetectIntent(string rawInput)
        {
            string input = (rawInput ?? string.Empty).Trim();
            string normalized = input.ToLowerInvariant();

            if (ContainsAny(normalized, "show activity log", "show recent actions", "what have you done for me"))
            {
                return new IntentResult { Intent = UserIntent.ShowActivityLog };
            }

            if (ContainsAny(normalized, "start quiz", "play quiz", "cyber quiz", "test my knowledge", "cybersecurity game"))
            {
                return new IntentResult { Intent = UserIntent.StartQuiz };
            }

            if (ContainsAny(normalized, "show tasks", "show my tasks", "view tasks", "what tasks do i have"))
            {
                return new IntentResult { Intent = UserIntent.ShowTasks };
            }

            if (normalized.Contains("remind me to", StringComparison.Ordinal))
            {
                string taskTitle = ExtractReminderTitle(input);
                DateTime? reminderDate = ExtractReminderDate(input);
                return new IntentResult
                {
                    Intent = UserIntent.AddTaskWithReminder,
                    TaskTitle = taskTitle,
                    ReminderDate = reminderDate
                };
            }

            if (ContainsAny(normalized, "add task", "add a task", "new task", "new a task", "create task", "create a task"))
            {
                return new IntentResult
                {
                    Intent = UserIntent.AddTask,
                    TaskTitle = ExtractTaskTitle(input)
                };
            }

            if (ContainsAny(normalized, "password", "phishing", "phish", "malware", "virus", "privacy", "cybersecurity"))
            {
                return new IntentResult { Intent = UserIntent.CybersecurityQuestion };
            }

            return new IntentResult { Intent = UserIntent.Unknown };
        }

        public NlpResult Analyze(string input)
        {
            string text = (input ?? string.Empty).Trim();
            string normalized = text.ToLowerInvariant();
            string intent = normalized switch
            {
                _ when normalized.Contains("password") => "PasswordSafety",
                _ when normalized.Contains("phish") || normalized.Contains("email") => "Phishing",
                _ when normalized.Contains("malware") || normalized.Contains("virus") => "Malware",
                _ when normalized.Contains("privacy") || normalized.Contains("personal data") => "Privacy",
                _ when normalized.Contains("quiz") => "Quiz",
                _ when normalized.Contains("task") || normalized.Contains("remind") => "TaskManagement",
                _ when normalized.Contains("hello") || normalized.Contains("hi") => "Greeting",
                _ => "Unknown"
            };

            List<string> entities = Regex.Matches(text, @"\b(password|phishing|malware|privacy|quiz|task|reminder)\w*\b", RegexOptions.IgnoreCase)
                .Select(match => match.Value)
                .Distinct(StringComparer.OrdinalIgnoreCase)
                .ToList();

            return new NlpResult
            {
                OriginalText = text,
                NormalizedText = normalized,
                Intent = intent,
                Entities = entities,
                Confidence = intent == "Unknown" ? 0.35 : 0.92
            };
        }

        private static bool ContainsAny(string input, params string[] phrases)
        {
            return phrases.Any(input.Contains);
        }

        private static string ExtractReminderTitle(string input)
        {
            Match match = Regex.Match(input, @"remind\s+me\s+to\s+(?<title>.+)$", RegexOptions.IgnoreCase);
            if (!match.Success)
            {
                return string.Empty;
            }

            return Regex.Replace(match.Groups["title"].Value, @"\s+(?:(?:on|at)\s+.+|in\s+\d+\s+days?|today|tomorrow)$", string.Empty, RegexOptions.IgnoreCase).Trim(' ', '.', ',');
        }

        private static string ExtractTaskTitle(string input)
        {
            Match match = Regex.Match(input, @"(?:add|new|create)\s+(?:a\s+)?task\s*:?[\s-]*(?:to\s+)?(?<title>.+)$", RegexOptions.IgnoreCase);
            return match.Success ? match.Groups["title"].Value.Trim() : string.Empty;
        }

        private static DateTime? ExtractReminderDate(string input)
        {
            Match dateMatch = Regex.Match(input, @"(?:on|at)\s+(?<date>.+)$", RegexOptions.IgnoreCase);
            if (dateMatch.Success && DateTime.TryParse(dateMatch.Groups["date"].Value, out DateTime parsedDate))
            {
                return parsedDate;
            }

            if (Regex.IsMatch(input, @"\btomorrow\b", RegexOptions.IgnoreCase))
            {
                return DateTime.Today.AddDays(1);
            }

            if (Regex.IsMatch(input, @"\btoday\b", RegexOptions.IgnoreCase))
            {
                return DateTime.Today;
            }

            Match relativeDateMatch = Regex.Match(input, @"\bin\s+(?<days>\d+)\s+days?\b", RegexOptions.IgnoreCase);
            if (relativeDateMatch.Success && int.TryParse(relativeDateMatch.Groups["days"].Value, out int days))
            {
                return DateTime.Today.AddDays(days);
            }

            return null;
        }
    }

    public sealed class NlpResult
    {
        public string OriginalText { get; init; } = string.Empty;
        public string NormalizedText { get; init; } = string.Empty;
        public string Intent { get; init; } = "Unknown";
        public IReadOnlyList<string> Entities { get; init; } = Array.Empty<string>();
        public double Confidence { get; init; }
    }
}
