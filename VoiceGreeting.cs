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
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "greeting.wav");
                if (!File.Exists(path))
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "greeting.wav");
                    path = Path.GetFullPath(path);
                }

                if (File.Exists(path))
                {
                    using var player = new SoundPlayer(path);
                    player.Load();
                    player.Play();
                }
            }
            catch
            {
            }
        }
    }
}



















