using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;

namespace CyberSafetyChatbotGUI
{
    public class VoiceGreeting
    {
        public async Task PlayAsync()
        {
            try
            {
                string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "greeting.wav");
                if (!File.Exists(path))
                {
                    path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Assets", "greeting.wav");
                    path = Path.GetFullPath(path);
                }

                if (File.Exists(path))
                {
                    await Task.Run(() =>
                    {
                        using var player = new SoundPlayer(path);
                        player.Load(); // may throw on bad format
                        player.Play(); // non-blocking
                    });
                }
            }
            catch
            {
                // Swallow exceptions; do not show UI (keep form stable). Consider logging if you add logging later.
            }
        }   
    }
}



















