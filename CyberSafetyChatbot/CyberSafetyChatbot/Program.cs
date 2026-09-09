using System;

namespace CyberSafetyChatbot
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            //Front door of application
            // its job is to create your chatbot object  and start the program

            Chatbot bot = new Chatbot();

            // Start the chatbot
            bot.Start();

        }
    }
}