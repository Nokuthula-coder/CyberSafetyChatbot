using System.Text.RegularExpressions;

namespace CyberSafetyChatbotGUI
{
    public enum Sentiment
    {
        Neutral,
        Worried,
        Curious,
        Frustrated
    }

    public class SentimentDetector
    {
        private static bool WordMatch(string input, string pattern) =>
            Regex.IsMatch(input ?? string.Empty, $@"\b({pattern})\b", RegexOptions.IgnoreCase);

        // Detect the sentiment of a given input
        public Sentiment Detect(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Sentiment.Neutral;

            string cleaned = input.ToLowerInvariant();

            if (WordMatch(cleaned, "worried|scared|afraid|anxious"))
                return Sentiment.Worried;

            if (WordMatch(cleaned, "curious|interested|wondering") || (cleaned.Contains("how") && cleaned.Contains("work")))
                return Sentiment.Curious;

            if (WordMatch(cleaned, "frustrated|annoyed|angry|tired|upset"))
                return Sentiment.Frustrated;

            return Sentiment.Neutral;
        }

        // Get a support message for a sentiment (unchanged behaviour)
        public string GetSupportMessage(Sentiment sentiment)
        {
            return sentiment switch
            {
                Sentiment.Worried => "I understand why that feels worrying. ",
                Sentiment.Frustrated => "That does sound frustrating — let's sort it out. ",
                Sentiment.Curious => "Great curiosity! ",
                _ => string.Empty
            };
        }

        // Build a full reply to the user's input, using detected sentiment and a follow-up prompt.
        public string ReplyToInput(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return "I'm here to help whenever you're ready to share.";
            }

            Sentiment sentiment = Detect(input);
            string support = GetSupportMessage(sentiment);

            string followUp = sentiment switch
            {
                Sentiment.Worried => "Can you tell me more about what's worrying you right now?",
                Sentiment.Frustrated => "What specifically is causing the frustration? I can help break it down step by step.",
                Sentiment.Curious => "What would you like to learn more about or explore?",
                _ => "How can I help you today?"
            };

            // Ensure proper spacing and return combined response.
            return (support + followUp).Trim();
        }
    }
}



