using System;


namespace CyberSafetyChatbot
{
    public class AsciiArt
    {
        public void DisplayLogo()
        {
            //Use console colors to make the ASCII art more visually appealing- one colour for logo
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine(" ===============================================================");
            Console.WriteLine(" =============== CYBERSECURITY AWARENESS BOT ==================");
            Console.WriteLine("================================================================");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(@"
   ____                     _____     __           ____     ___   
  / __/__ ______ _________ / ___/_ __/ /  ___ ____/ __/__ _/ _/__ 
 _\ \/ -_) __/ // / __/ -_) /__/ // / _ \/ -_) __/\ \/ _ `/ _/ -_)
/___/\__/\__/\_,_/_/  \__/\___/\_, /_.__/\__/_/ /___/\_,_/_/ \__/ 
                              /___/                               

            ");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("          STAY SAFE ONLINE!");
            Console.WriteLine();
            Console.WriteLine("=================================================================");
        
            Console.ResetColor();
        }
    }
}
