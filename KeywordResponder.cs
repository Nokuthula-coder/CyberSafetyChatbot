using System;
using System.Collections.Generic;

namespace CyberSafetyChatbotGUI
{
    public class KeywordResponder
    {
        private readonly Dictionary<string, List<string>> keywordResponses;
        private readonly Random random = new Random();

        public KeywordResponder()
        {
            keywordResponses = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase)
            {
                ["password"] = new List<string> {
                    "Use a strong and unique password for each account.",
                    "Avoid including easy-to-guess personal info in passwords.",
                    "Consider using a trusted password manager.",
                    "Make it at least 12 characters with numbers and symbols."
                },
                ["scam"] = new List<string> {
                    "Scams often create urgency. Take a breath and verify.",
                    "Never share OTPs or banking details via message.",
                    "Check the sender email carefully, scammers fake it."
                },
                ["privacy"] = new List<string> {
                    "Review app permissions regularly.",
                    "Limit personal info you share online.",
                    "Use private browsing and check privacy settings."
                },
                ["phishing"] = new List<string> {
                    "Check the sender before trusting an email.",
                    "Avoid opening unexpected links or attachments.",
                    "Do not provide passwords or banking details through suspicious messages."
                },
                ["malware"] = new List<string> {
                    "Keep your OS and antivirus updated.",
                    "Don't download from untrusted sites."
                },
                ["safebrowsing"] = new List<string> {
                    "Look for HTTPS and lock icon before entering details.",
                    "Avoid public WiFi for banking without VPN."
                }
            };
        }

        public string GetResponse(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
                return string.Empty;

            if (keywordResponses.TryGetValue(topic, out var responses) && responses.Count > 0)
            {
                int index = random.Next(responses.Count);
                return responses[index];
            }

            return string.Empty;
        }

        public List<string> GetTopics()
        {
            return new List<string>(keywordResponses.Keys);
        }
    }
}