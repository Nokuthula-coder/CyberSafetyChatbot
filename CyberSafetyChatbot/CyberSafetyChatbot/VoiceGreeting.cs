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
                // Locate the  Audio folder and greeting.wav file.
                string audioPath = Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                    "Audio",
                    "greeting.wav"
                    );

                // Check whether the WAV file exists
                if (File.Exists( audioPath ) )
                {
                    //Create the SoundPlayer
                    SoundPlayer player = new SoundPlayer(audioPath);
                    
                    // Play the sound
                        player.PlaySync();
                 
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
    
