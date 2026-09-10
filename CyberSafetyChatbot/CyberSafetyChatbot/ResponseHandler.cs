
namespace CyberSafetyChatbot
{
    public class ResponseHandler
    {
      
        public string GetResponse(string userInput, string userName)
        {
            //Convert user input to lowercase so comparisons are not affected by capital letters.
            string cleanedInput = userInput.ToLower().Trim();

            // Required question: How are you?
            if (cleanedInput.Contains("how are you"))
            {
                return $"I'm great,{userName}! thank you!" +
                    "I'm ready to help you  learn about Cybersecurity.";

            }

            //General greeting
            if (cleanedInput == "hello" ||
                cleanedInput == "hi" ||
                cleanedInput.Contains("hey"))
            {
                return $"Hello! It is great to chat with you,{userName}" +
                    "What would you like to know about Cybersecurity?";
            }



            // Required question: Purpose
            else if (cleanedInput.Contains("purpose"))
            {
                return "My purpose is to educate and assist users in understanding cybersecurity threats " +
                    " and show you how to stay safer online.";

            }

            // Alternative way user may ask about purpose
            else if (cleanedInput.Contains("what do you do"))
            {
                return "I am a Cybersecurity Awareness Assistant." +
                    "I provide information about password safety, phishing" +
                    "and safe browsing.";

            }

            //Required question: What can I ask about?
            else if (cleanedInput.Contains("What can i ask") ||
                cleanedInput.Contains("help") ||
                cleanedInput.Contains("topics"))
            {
                return "You can ask me about:\n- What is your purpose?\n- Password safety\n- Phishing" +
                    "\n- Safe browsing $ Privacy\n- Malware\nJust type your question:";

            }

            //Password safety
            else if (cleanedInput.Contains("password"))
            {
                return "PASSWORD SAFETY:\n- Use a strong, unique password for each account." +
                    "\n- Avoid using easily guessable information like birthdays or names." +
                    "\n- Consider using a password manager to securely store and generate passwords." +
                    "\n- Enable two-factor authentication (2FA) whenever possible.";
            }

            //Phishing
            else if (cleanedInput.Contains("phishing"))
            {
                return "PHISHING:\n- Be cautious of emails or messages from unknown senders." +
                    "\n- Avoid clicking on suspicious links or downloading attachments from untrusted sources." +
                    "\n- Verify the authenticity of requests for personal information by contacting the organization directly.";
            }

            //Safe browsing 
            else if (cleanedInput.Contains("safe browsing") || cleanedInput.Contains("privacy") || cleanedInput.Contains("safe"))
            {
                return "SAFE BROWSING & PRIVACY:\n- Keep your web browser and software up to date." +
                    "\n- Use secure websites (look for 'https://' in the URL)." +
                    "\n- Be mindful of the information you share online and adjust privacy settings on social media platforms.\n- Consider using a virtual private network (VPN) for added security.";

            }

            //Cybersecurity
            else if (cleanedInput.Contains("cybersecurity") || cleanedInput.Contains("cyber security"))
            {
                return "CYBERSECURITY: \n- Cybersecurity is the practice of protecting computers, " +
                    "\n- devices, networks and information against cyber threats and unauthorised access";

            }

            //Malware
            else if (cleanedInput.Contains("malware") || cleanedInput.Contains("virus"))
            {
                return "MALWARE:\n- Keep your operating system and software up to date." +
                    "\n- Use reputable antivirus and anti-malware software." +
                    "\n- Be cautious when downloading files or clicking on links from unknown sources." +
                    "\n- Regularly back up important data to recover in case of an infection.";
            }

            // Default response for unsupportrd questions
            else
            {
                return "I'm sorry, I did not quite understand that. Could you please rephrase your question or " +
                    "ask about another topic related to cybersecurity?";


            }
        }
    }
}
