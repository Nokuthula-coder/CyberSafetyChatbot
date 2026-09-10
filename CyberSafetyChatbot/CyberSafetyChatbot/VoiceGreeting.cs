using System;
using System.IO;
using System.Media;

namespace CyberSafetyChatbot
{
    public class VoiceGreeting
    {
        public void PlayGreeting()
        {
            try
            {

                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Audio", "greeting.wav");

                // Fallback if not in Audio folder
                if (!File.Exists(path))
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");

                if (File.Exists(path))
                {
                    
                    using ( SoundPlayer player = new SoundPlayer(path))
                        { 

                        // Play the sound
                        player.PlaySync();
                    }
                 
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Voice greeting audio file not found.");
                    Console.ResetColor();
                }
            }

            catch (Exception ex)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("Unable to play the voice greeting.");
                Console.WriteLine("Error: " + ex.Message);
                Console.ResetColor();


            }
        }
    }
}
    
