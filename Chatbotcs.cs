using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CyberSafetyChatbotGUI
{
    // Delegate for UI callbacks
    public delegate void BotResponseHandler(string message);

    public class ChatBot
    {
        // Use an event so subscribers add/remove handlers safely
        public event BotResponseHandler? ResponseHandler;

        private KeywordResponder keywordResponder = new KeywordResponder();
        private Random random = new Random();
        private MemoryStore memory = new MemoryStore();
        private SentimentDetector sentimentDetector = new SentimentDetector();
        private List<string> followUpPhrases = new List<string>
        {
            "tell me more",
            "give me another tip",
            "explain more",
            "what else",
            "continue",
            "more"
        };

        // Backwards-compatible helper used by the form
        public Sentiment DetectSentiment(string input) => sentimentDetector.Detect(input ?? string.Empty);

        // Fix: use existing type/method name in context
        public Sentiment AnalyzeSentiment(string input) => sentimentDetector.Detect(input ?? string.Empty);

        public string ProcessInput(string userInput)
        {
            // Defensive: treat null/empty input gracefully (no exception)
            if (string.IsNullOrWhiteSpace(userInput))
            {
                const string prompt = "Please type a question or cybersecurity topic first.";
                ResponseHandler?.Invoke(prompt);
                return prompt;
            }

            string trimmed = userInput.Trim();
            if (string.IsNullOrWhiteSpace(trimmed))
            {
                const string prompt = "Please type a question or cybersecurity topic first.";
                ResponseHandler?.Invoke(prompt);
                return prompt;
            }

            // 3. Normalize (for simple checks)
            string lower = trimmed.ToLowerInvariant();

            // Provide an alias used throughout the method to match prior variable name usage
            string cleaned = lower;

            // 4. Detect sentiment early
            Sentiment sentiment = sentimentDetector.Detect(lower);
            string support = sentimentDetector.GetSupportMessage(sentiment);

            // 5. Name & interest capture (must run before topic detection)
            if (lower.StartsWith("my name is", StringComparison.Ordinal))
            {
                // Use robust extractor rather than brittle substring indexing
                memory.UserName = ExtractName(trimmed);
                string msg = $"Nice to meet you, {memory.UserName}!";
                ResponseHandler?.Invoke(msg);
                return msg;
            }

            if (cleaned.Contains("i am ") && trimmed.Length < 40) // simple name capture, allow slightly longer
            {
                memory.UserName = ExtractName(trimmed);
                string msg = $"Nice to meet you, {memory.UserName}!";
                ResponseHandler?.Invoke(msg);
                return msg;
            }

            if (cleaned.Contains("i am interested in"))
            {
                string interest = cleaned.Replace("i am interested in", "").Trim();
                memory.FavouriteTopic = interest;
                string msg = $"Great! I will remember that you are interested in {interest}.";
                ResponseHandler?.Invoke(msg);
                return msg;
            }

            // 2b. Memory recall queries (Step 8: Memory and recall)
            if (TryGetMemoryRecallResponse(cleaned, out var recallResponse))
            {
                string final = (support + recallResponse).Trim();
                ResponseHandler?.Invoke(final);
                return final;
            }

            // 3. Find topic (use word-boundary matching and null-safety)
            string? foundTopic = null;
            foreach (var topic in keywordResponder.GetTopics() ?? Enumerable.Empty<string>())
            {
                if (string.IsNullOrWhiteSpace(topic))
                    continue;

                var escaped = Regex.Escape(topic.ToLowerInvariant());
                if (Regex.IsMatch(cleaned, $@"\b{escaped}\b", RegexOptions.CultureInvariant))
                {
                    foundTopic = topic;
                    break;
                }
            }

            // 4. Follow-up handling (word-boundary safe)
            bool isFollowUp = followUpPhrases.Any(p =>
            {
                var esc = Regex.Escape(p.ToLowerInvariant());
                return Regex.IsMatch(cleaned, $@"\b{esc}\b", RegexOptions.CultureInvariant);
            });

            if (foundTopic != null)
            {
                memory.CurrentTopic = foundTopic; // save for follow-ups
            }
            else if (isFollowUp && !string.IsNullOrEmpty(memory.CurrentTopic))
            {
                foundTopic = memory.CurrentTopic;
            }

            // 5. Build response
            string response;
            if (foundTopic != null)
            {
                response = keywordResponder.GetResponse(foundTopic) ?? "Here is something useful about that topic.";
            }
            else
            {
                // If user expresses emotion but no topic, ask for more detail
                if (sentiment != Sentiment.Neutral)
                {
                    response = "Can you tell me more about what's worrying you? Try asking about passwords, phishing, privacy, or scams.";
                }
                else
                {
                    response = "I'm not sure I understand. Can you try rephrasing or ask me about passwords, scams, privacy, or phishing?";
                }
            }

            // 6. Final response = sentiment support + answer
            string finalResponse = (support + response).Trim();
            ResponseHandler?.Invoke(finalResponse);
            return finalResponse;
        }

        private bool TryGetMemoryRecallResponse(string cleanedLower, out string response)
        {
            response = string.Empty;

            // Name recall queries
            if (Regex.IsMatch(cleanedLower, @"\b(what('?s| is) my name|who am i|what am i called|do you remember my name)\b", RegexOptions.CultureInvariant))
            {
                if (!string.IsNullOrWhiteSpace(memory.UserName))
                {
                    response = $"You told me your name is {memory.UserName}.";
                }
                else
                {
                    response = "I don't know your name yet. You can tell me by saying \"My name is ...\".";
                }

                return true;
            }

            // Favourite / interest recall queries
            if (Regex.IsMatch(cleanedLower, @"\b(what('?s| is) my (favourite|favorite) topic|what am i interested in|what do i like|remind me (my )?(favourite|favorite) topic)\b", RegexOptions.CultureInvariant))
            {
                if (!string.IsNullOrWhiteSpace(memory.FavouriteTopic))
                {
                    response = $"You said you are interested in {memory.FavouriteTopic}.";
                }
                else
                {
                    response = "I don't know your favourite topic yet. Tell me by saying \"I am interested in ...\".";
                }

                return true;
            }

            // Do you remember queries (general)
            if (Regex.IsMatch(cleanedLower, @"\b(do you remember|remember what i told you|what did i tell you)\b", RegexOptions.CultureInvariant))
            {
                var parts = new List<string>();
                if (!string.IsNullOrWhiteSpace(memory.UserName))
                    parts.Add($"your name is {memory.UserName}");
                if (!string.IsNullOrWhiteSpace(memory.FavouriteTopic))
                    parts.Add($"you are interested in {memory.FavouriteTopic}");
                if (!string.IsNullOrWhiteSpace(memory.CurrentTopic))
                    parts.Add($"we were discussing {memory.CurrentTopic}");

                if (parts.Count > 0)
                    response = "I remember " + string.Join(", and ", parts) + ".";
                else
                    response = "I don't have any saved details yet. You can tell me your name or what you're interested in.";

                return true;
            }

            return false;
        }

        private string ExtractName(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return string.Empty;

            string lower = input.ToLowerInvariant();
            string[] separators = new[] { "my name is", "i am", "i'm" };
            foreach (var sep in separators)
            {
                int idx = lower.IndexOf(sep, StringComparison.Ordinal);
                if (idx >= 0)
                {
                    string candidate = input.Substring(idx + sep.Length).Trim();
                    int end = candidate.IndexOfAny(new[] { '.', '!', '?' });
                    if (end > 0)
                        candidate = candidate.Substring(0, end).Trim();

                    var token = candidate.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault() ?? candidate;
                    return token.Trim().Trim(',', ';', ':', '.', '!', '?', '"', '\'',
                        // Additional trims for robustness
                        '‘', '’', '“', '”', '•', '…');
                }
            }

            // Fallback: last word of the input
            var last = input.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Last();
            return last.Trim().Trim(',', ';', ':', '.', '!', '?', '"', '\'',
                // Additional trims for robustness
                '‘', '’', '“', '”', '•', '…');
        }
    }
}