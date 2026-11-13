4 using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;

namespace CybersecurityChatbotGUI
{
    public class VoiceGreeting
    {
        public void PlayWelcome()
        {
            // Run on background thread so it doesn't block UI
            Task.Run(() => PlayGreetingAsync());
        }

        private async Task PlayGreetingAsync()
        {
            try
            {
                string soundPath = Path.Combine(AppContext.BaseDirectory, "Assets", "greeting.wav");

                Console.WriteLine($"Looking for greeting.wav at: {soundPath}");
                Console.WriteLine($"File exists: {File.Exists(soundPath)}");

                if (!File.Exists(soundPath))
                {
                    Console.WriteLine($"Greeting sound not found.");
                    Console.WriteLine($"Current directory: {Directory.GetCurrentDirectory()}");
                    Console.WriteLine($"Files in directory: {string.Join(", ", Directory.GetFiles(AppContext.BaseDirectory, "*.wav"))}");
                    return;
                }

                // Simple direct playback using SoundPlayer
                using (SoundPlayer player = new SoundPlayer(soundPath))
                {
                    player.PlaySync();
                    Console.WriteLine("Greeting played successfully!");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing greeting: {ex.Message}");
            }
        }
    }
}


















