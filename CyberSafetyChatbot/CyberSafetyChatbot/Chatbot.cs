using System;


namespace CyberSafetyChatbot
{
    public class Chatbot
    {
        // Objects used by the chatbot
        private UserProfile userProfile;
        private ResponseHandler responseHandler;
        private VoiceGreeting voiceGreeting;
        private AsciiArt asciiArt;
         
        //Constructor
        public Chatbot()
        {
            userProfile = new UserProfile();
            responseHandler = new ResponseHandler();
            voiceGreeting  = new VoiceGreeting();
            asciiArt = new AsciiArt();

        }
        public void Start()
        {
            Console.Title = "Cybersecurity Awareness Assistant";

            Console.Clear();

            //1. Play the voice greeting
            voiceGreeting.PlayGreeting();

            //2. Display ASCII art of a robot.
            asciiArt.DisplayLogo();

            //3. Ask for the user's name
            AskForName();

            //4. Validate the name
            // Validation is handled inside AskForname()

            //5. Display personalised greeting
            DisplayWelcomeMessage();

            //6. Start the chatbot conversation loop
            StartConversation();
        }

        private void AskForName()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Bot: ");
            Console.ResetColor();

            Console.WriteLine("Hello! What is your name?");
            string name = Console.ReadLine() ?? "";

            // Validate the user's name.
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Name cannot be empty. Please enter your name:");
                Console.ResetColor();
                Console.Write("Please enter your name:");
                name = Console.ReadLine() ?? "";
            }

            //Store validated name using the automatic property
            userProfile.Name = name.Trim();
        }

        private void DisplayWelcomeMessage()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;

            Console.WriteLine();
            Console.WriteLine("\n===============================================================================================");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Bot: Welcome, {userProfile.Name}!");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(
                "I'm your  Cybersecurity Awareness Assissant, I'm here to help you stay safe online!");

            Console.WriteLine();
            Console.WriteLine("\nYou can ask me about:");
            Console.WriteLine("- Password safety");
            Console.WriteLine("- Phishing");
            Console.WriteLine("- Safe browsing & Privacy");
            Console.WriteLine("- My purpose");
            Console.WriteLine("- How are you?");
            Console.WriteLine("- What can I ask you about?");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Type \"exit\'when you want to end the conversation");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("===================================================================================================");
            Console.ResetColor();
        }

        private void StartConversation()
        {
            while (true)
            {
                Console.WriteLine();

                Console.ForegroundColor= ConsoleColor.Blue;
                Console.Write($"{userProfile.Name}:  ");

                Console.ResetColor();

                string userInput = Console.ReadLine() ??"";

                // Validate the user's input.
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Bot: Input cannot be empty. Please ask a question or provide a topic.");
                    Console.ResetColor();

                    //Return to the beginning of the loop 
                    continue;
                }

                // Remove unneccessary spaces
                userInput = userInput.Trim();


                //Check whether the user wants to exit the conversation.
                if (userInput.ToLower() == "exit" || userInput.ToLower() == "quit")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Goodbye,{userProfile.Name}. Thank you for using the Cyber Safety Chatbot. Stay safe online!");
                    Console.ResetColor();
                    break;
                }

                // Get the chatbot's response based on the user's input.
                string response = responseHandler.GetResponse(userInput, userProfile.Name);

                // Display the chatbot's response.
                Console.WriteLine("------------------------------------------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"Bot: {response}");
                Console.ResetColor();
                Console.WriteLine("------------------------------------------------------------------------------------------------------------");
            }
        }
    }
}

