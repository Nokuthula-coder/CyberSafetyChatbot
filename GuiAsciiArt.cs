using System;

namespace CyberSafetyChatbotGUI
{
    public static class GuiAsciiArt
    {
        public static string GetBanner()
        {
            // Monospaced ASCII banner intended for a RichTextBox using Consolas or similar.
            return @"
 ===============================================================
 ==============  CYBERSECURITY AWARENESS CHATBOT  =============
 ===============================================================

   ____                     _____     __           ____     ___   
  / __/__ ______ _________ / ___/_ __/ /  ___ ____/ __/__ _/ _/__ 
 _\ \/ -_) __/ // / __/ -_) /__/ // / _ \/ -_) __/\ \/ _ `/ _/ -_)
/___/\__/\__/\_,_/_/  \__/\___/\_, /_.__/\__/_/ /___/\_,_/_/ \__/ 
                              /___/                               

                     STAY SAFE ONLINE!

 ===============================================================

";
        }
    }
}