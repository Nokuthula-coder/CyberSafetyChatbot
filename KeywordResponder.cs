using System;
using System.Collections.Generic;

namespace CyberSafetyChatbotGUI
{
    public class KeywordResponder
    {
        private readonly Dictionary<string, List<string>> keywordResponses;
        private readonly Random random = new Random();
        private readonly Dictionary<string, int> lastIndexes = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

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
                },
                ["safe browsing"] = new List<string> {
                    "Look for HTTPS and the lock icon before entering details.",
                    "Avoid public Wi-Fi for banking, or use a trusted VPN."
                },
                ["security"] = new List<string> {
                    "Keep your devices updated and enable multi-factor authentication.",
                    "Use separate, strong passwords and review account alerts regularly."
                },
                ["vpn"] = new List<string> {
                    "A trusted VPN can help protect traffic on untrusted networks.",
                    "A VPN does not replace strong passwords, updates, or careful browsing."
                },
                ["link"] = new List<string> {
                    "Hover over an unexpected link to inspect its real destination before opening it.",
                    "Do not enter credentials after following a suspicious link; visit the official site directly."
                },
                ["links"] = new List<string> {
                    "Hover over an unexpected link to inspect its real destination before opening it.",
                    "Do not enter credentials after following a suspicious link; visit the official site directly."
                }
            };
        }

        public string GetResponse(string topic)
        {
            if (string.IsNullOrWhiteSpace(topic))
                return string.Empty;

            if (keywordResponses.TryGetValue(topic, out var responses) && responses.Count > 0)
            {
                return GetRandomResponse(topic, responses);
            }

            return string.Empty;
        }
        private int lastIndex = -1;

        private string GetRandomResponse(string topic, List<string> responses)
        {
            int previousIndex = lastIndexes.TryGetValue(topic, out int index) ? index : -1;
            int newIndex;

            do
            {
                newIndex = random.Next(responses.Count);
            }
            while (responses.Count > 1 && newIndex == previousIndex);

            lastIndexes[topic] = newIndex;
            return responses[newIndex];
        }

        public string GetRandomResponse(List<string> responses)
        {
            if (responses == null || responses.Count == 0)
                return string.Empty;

            int newIndex;

            do
            {
                newIndex = random.Next(responses.Count);
            }
            while (newIndex == lastIndex &&
                   responses.Count > 1);

            lastIndex = newIndex;

            return responses[newIndex];
        }
        public List<string> GetTopics()
        {
            return new List<string>(keywordResponses.Keys);
        }
    }
}