# PROG6221 Programming 2A - POE Part 2

## Student Details
Name: Nokuthula Innocentia Mtsweni, Student Number: ST10499051

## Project Overview
Cybersecurity Awareness Chatbot with GUI, memory, sentiment detection and delegate pattern. Migrated from Part 1 console.

## Part 2 Features
- GUI with centered form and branding
- Keyword recognition using Dictionary<string, List<string>>
- Random responses with single Random instance
- Conversation flow using currentTopic (tell me more)
- Memory: UserName, FavouriteTopic stored
- Sentiment detection: Worried, Curious, Frustrated + supportive response
- Error handling for blank input
- Delegate BotResponseHandler to decouple logic from GUI
- Voice greeting from Assets/greeting.wav
- ASCII art banner in Consolas font

## Cybersecurity Topics Supported
password, scam, privacy, phishing, malware, safe browsing

## Memory and Sentiment Features
Uses currentTopic for follow-ups and favouriteTopic for personalisation. Sentiment examples: worried, curious, frustrated.

## How to Run the Project
- Open CyberSafetyChatbotGUI.sln
- Startup project: CyberSafetyChatbotGUI
- Assets/greeting.wav set to Copy if newer
- F5 to run in VS 2022,.NET 8.0
- Test features from page 15 table

## Project Structure
/CyberSafetyChatbotGUI
  ChatbotForm.cs 
  ChatBot.cs 
  KeywordResponder.cs
  MemoryStore.cs
  SentimentDetector.cs
  VoiceGreeting.cs
  Assets/greeting.wav
/.github/workflows/dotnet.yml

## GitHub / CI Information
GitHub link:.github/workflows/dotnet-desktop.yml

<img width="1857" height="902" alt="Screenshot 2026-10-16 at 23-33-41 All workflows" src="https://github.com/user-attachments/assets/26941422-b349-4438-ae5d-70435bb42745" />



## Tags and Releases
- v1.0-console - Part 1: Initial console version
- v1.1-refactor - Part 1: Refactored for GUI migration
- v2.0-gui - Part 2: GUI created and Part 1 features migrated
- v2.1-dynamic-responses - Part 2: Keyword recognition, random responses and conversation flow added
- v2.2-memory-sentiment - Part 2: Memory, sentiment detection, validation and final polish completed

## Video Presentation Link
https://youtu.be/FPN5bvyXfDE




## References

- Microsoft, 2024. _SoundPlayer Class (System.Media)_. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.media.soundplayer [Accessed: 15 October 2026]

- Microsoft, 2024. _How to: Play a Sound from a Windows Form_. Available at: https://learn.microsoft.com/en-us/dotnet/desktop/winforms/media/how-to-play-a-sound [Accessed: 15 October 2026]


## Author

Student: Nokuthula Innocentia Mtsweni

#Cyber Safety Awareness Bot - part 1- PROG6221
Student:Nokuthula Mtsweni


## Project Overview
A C# console chatbot that educates users about Cybersecurity awareness. It includes voice greeting, ASCII art logo, personalised conversation, and responds to common cybersecurity questions.

This project was built for PROG6221 Part 1 - 2026.

## Features Implemented 

- Voice Greeting
    - Plays `greeting.wav` using `System.Media.SoundPlayer`
    - File located at `Audio/greeting.wav`
    - Build Action = Content, Copy if newer
    - Error handling if file not found

- ASCII Art
    - Displays robot logo with cyan colour
    - Uses double backslashes `\\` for correct display
    - Method: `AsciiArt.DisplayLogo()`

- Personalisation
    - Asks for user name with validation `string.IsNullOrWhiteSpace`
    - Stores in `UserProfile` class
    - Displays welcome message: "Welcome, {name}!"

- Response System
    - Uses `ToLower()` for case-insensitive comparison
    - Uses `if-else if` with `Contains()`
    - Fixed `Contains("hi")` bug - now uses `== "hi"` to avoid matching `phishing`
    - Topics covered:
        - How are you? -> Personalised response with name
        - Purpose
        - Password safety
        - Phishing (fixed bug where phishing contains hi)
        - Safe browsing / Security / VPN
        - Malware
        - What can I ask?

## How to Run

1. Clone repository:
   ```bash
   git clone https://github.com/Nokuthula-coder/CyberSafetyChatbot-POE-Part1.git
2. Open `CyberSafetyChatbot.sln` in Visual Studio 2022

3. Make sure `Audio/greeting.wav` properties:
      - Build Action: Content
      - Copy to Output Directory: Copy if newer

4. Press F5 or Build > Rebuild > Start

5. Test commands:
      - `hello`
      - `how are you`
      - `what is your purpose`
      - `phishing`
      - `password`
      - `malware`
      - `security`
      - `exit` to quit

## Project Structure

CyberSafetyChatbot/
├── Audio/
│   └── greeting.wav
├── .github/
│   └── workflows/
│       └── ci.yml (GitHub Actions CI)
├── Program.cs (Entry point - creates Chatbot and calls Start())
├── Chatbot.cs (Main flow - voice, ASCII, name validation, loop)
├── VoiceGreeting.cs (Plays WAV with error handling)
├── AsciiArt.cs (Displays robot logo)
├── UserProfile.cs (Stores Name property)
├── ResponseHandler.cs (GetResponse(userInput, userName) logic)
└── README.md

## GitHub Actions CI

Workflow located at `.github/workflows/ci.yml`
- Runs on: windows-latest
- Triggers on push to main/master
- Steps: checkout, setup .NET 8, restore, build

<img width="1763" height="1446" alt="Screenshot_10-9-2026_215511_github com" src="https://github.com/user-attachments/assets/6e0a7c1f-da80-4dab-b1a9-a82a686ed4ad" />


## YouTube Video Link

Video demonstration of all features:
https://youtu.be/txvBXci2mnl

Video shows:
- Voice greeting playing
- ASCII art display
- Name input and validation
- All chatbot responses including phishing fix and {userName} interpolation with $ sign
- Exit command

## References

- Pieterse, H., 2021. The Cyber Threat Landscape in South Africa: A 10-Year Review. _The African Journal of Information and Communication (AJIC)_, 28, pp.1-12.

- Microsoft, 2024. _SoundPlayer Class (System.Media)_. Available at: https://learn.microsoft.com/en-us/dotnet/api/system.media.soundplayer [Accessed: 10 May 2025]

- Microsoft, 2024. _How to: Play a Sound from a Windows Form_. Available at: https://learn.microsoft.com/en-us/dotnet/desktop/winforms/media/how-to-play-a-sound [Accessed: 10 May 2025]

- GeeksforGeeks, 2023. _C# | String Interpolation_. Available at: https://www.geeksforgeeks.org/c-sharp-string-interpolation/ [Accessed: 10 May 2025]


## Author

Student: Nokuthula Innocentia Mtsweni



