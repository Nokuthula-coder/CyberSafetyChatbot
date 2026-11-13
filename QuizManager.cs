using CyberSafetyChatbotGUI.Models;

namespace CyberSafetyChatbotGUI.Services
{
    public sealed class QuizManager
    {
        private readonly IReadOnlyList<QuizQuestion> questions = new List<QuizQuestion>
        {
            new()
            {
                Id = 1,
                Question = "Which is the safest response to an unexpected password-reset email?",
                Options = new[] { "Click the link immediately", "Verify it through the official website", "Reply with your password" },
                CorrectOptionIndex = 1,
                Explanation = "Open the official website yourself rather than trusting an unexpected link."
            },
            new()
            {
                Id = 2,
                Question = "What makes a password stronger?",
                Options = new[] { "Using your birthday", "Reusing one password everywhere", "Using a long, unique passphrase" },
                CorrectOptionIndex = 2,
                Explanation = "Long, unique passwords reduce the impact of a compromised account."
            },
            new()
            {
                Id = 3,
                Question = "What should you do with a suspicious attachment?",
                Options = new[] { "Open it to check", "Forward it to friends", "Verify the sender and scan it first" },
                CorrectOptionIndex = 2,
                Explanation = "Verify the sender and scan unexpected attachments before opening them."
            },
            new()
            {
                Id = 4,
                Question = "Which action improves account security most directly?",
                Options = new[] { "Enable multi-factor authentication", "Share an OTP with support", "Disable updates" },
                CorrectOptionIndex = 0,
                Explanation = "Multi-factor authentication adds protection beyond the password."
            },
            new()
            {
                Id = 5,
                Question = "What is a safe approach on public Wi-Fi?",
                Options = new[] { "Use online banking without protection", "Avoid sensitive activity or use a trusted VPN", "Turn off device security" },
                CorrectOptionIndex = 1,
                Explanation = "Avoid sensitive activity on untrusted networks or use a trusted VPN."
            },
            new()
            {
                Id = 6,
                Question = "What is the main purpose of two-factor authentication?",
                Options = new[] { "Make passwords shorter", "Add another verification step", "Remove the need for account security", "Share login details faster" },
                CorrectOptionIndex = 1,
                Explanation = "Two-factor authentication adds another verification step beyond the password."
            },
            new()
            {
                Id = 7,
                Question = "What is social engineering?",
                Options = new[] { "Manipulating people into revealing information", "Designing social media logos", "Repairing computer hardware", "Compressing files" },
                CorrectOptionIndex = 0,
                Explanation = "Social engineering targets people through manipulation rather than only attacking technology."
            },
            new()
            {
                Id = 8,
                Question = "Before opening a link in a suspicious message, what should you do?",
                Options = new[] { "Open it quickly", "Verify the sender and inspect the destination", "Disable security software", "Send personal information first" },
                CorrectOptionIndex = 1,
                Explanation = "Checking the sender and destination helps identify suspicious links."
            },
            new()
            {
                Id = 9,
                Question = "Is reusing one very strong password on every account recommended?",
                Options = new[] { "True", "False" },
                CorrectOptionIndex = 1,
                Explanation = "Password reuse allows one breach to endanger several accounts."
            },
            new()
            {
                Id = 10,
                Question = "Which message is most likely phishing?",
                Options = new[] { "An expected receipt from a known shop", "An urgent request for an OTP", "A saved contact greeting", "A calendar reminder you created" },
                CorrectOptionIndex = 1,
                Explanation = "Requests for one-time passwords and urgent action are common phishing warning signs."
            },
            new()
            {
                Id = 11,
                Question = "Why should software updates be installed?",
                Options = new[] { "They remove all privacy settings", "They can fix security vulnerabilities", "They make passwords public", "They disable antivirus protection" },
                CorrectOptionIndex = 1,
                Explanation = "Updates often contain fixes for known security vulnerabilities."
            },
            new()
            {
                Id = 12,
                Question = "What should you do if you suspect an account is compromised?",
                Options = new[] { "Ignore it", "Change the password and contact the provider", "Share the password with friends", "Delete all security alerts" },
                CorrectOptionIndex = 1,
                Explanation = "Change the password, enable MFA, and contact the provider through an official channel."
            }
        };

        private int score;
        private int answered;
        private int currentIndex;
        private bool quizStarted;

        public IReadOnlyList<QuizQuestion> Questions => questions;
        public int Score => score;
        public int Answered => answered;
        public int CurrentIndex => currentIndex;
        public bool QuizStarted => quizStarted;
        public int TotalQuestions => questions.Count;
        public QuizQuestion CurrentQuestion => questions[currentIndex];

        public void Reset()
        {
            score = 0;
            answered = 0;
            currentIndex = 0;
            quizStarted = false;
        }

        public void StartQuiz()
        {
            Reset();
            quizStarted = true;
        }

        public QuizAnswerResult SubmitAnswer(QuizQuestion question, int selectedOptionIndex)
        {
            bool isCorrect = selectedOptionIndex == question.CorrectOptionIndex;
            if (isCorrect)
                score++;

            answered++;
            return new QuizAnswerResult
            {
                QuestionId = question.Id,
                IsCorrect = isCorrect,
                Score = score,
                TotalQuestions = questions.Count,
                Feedback = isCorrect ? "Correct! " + question.Explanation : "Not quite. " + question.Explanation
            };
        }

        public QuizAnswerResult CheckAnswer(int selectedIndex)
        {
            if (!quizStarted)
                StartQuiz();

            return SubmitAnswer(CurrentQuestion, selectedIndex);
        }

        public bool MoveNext()
        {
            if (currentIndex >= questions.Count - 1)
            {
                quizStarted = false;
                return false;
            }

            currentIndex++;
            return true;
        }
    }
}
