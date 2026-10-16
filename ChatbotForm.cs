using System;
using System.Drawing;
using System.IO;
using System.Media;
using System.Windows.Forms;

namespace CyberSafetyChatbotGUI
{
    public partial class ChatbotForm : Form
    {
        // ONE instance only - if you create new ChatBot in btnSend_Click memory will reset
        private ChatBot chatBot = new ChatBot();

        // Runtime UI: topics list to help test keyword recognition (Step 5)
        private ListBox? lstTopics;
        private Label? lblTopics;
        private Label? lblSentiment; // Step 9: sentiment indicator
        private Label? lblSentimentMatches; // Step C: show matched sentiment words
        public   ChatbotForm()
        {
            InitializeComponent();

            // Subscribe to bot responses via the event
            chatBot.ResponseHandler += DisplayBotResponse;

            // Enable Enter key handling for the form
            this.KeyPreview = true;
            this.KeyDown += ChatbotForm_KeyDown;

            // Make Enter press the Send button
            this.AcceptButton = btnSend;
        }

        private void ChatbotForm_Load(object? sender, EventArgs e)
        {
            // ASCII banner styling
            rtbBanner.BackColor = Color.Black;
            rtbBanner.ForeColor = Color.Lime;
            rtbBanner.Font = new Font("Consolas", 9, FontStyle.Bold);
            rtbBanner.ReadOnly = true;
            rtbBanner.Text = @"
   ----                     _____     __           ____     ___   
  / __/__ ______ _________ / ___/_ __/ /  ___ ____/ __/__ _/ _/__ 
 _\ \/ -_) __/ // / __/ -_) /__/ // / _ \/ -_) __/\ \/ _ `/ _/ -_)
/___/\__/\__/\_,_/_/  \__/\___/\_, /_.__/\__/_/ /___/\_,_/_/ \__/ 
                              /___/                               

                     STAY SAFE ONLINE!";

            // Welcome message
            rtbChatHistory.AppendText($"[{DateTime.Now:HH:mm}] Bot: Hello! I am CyberSafetyChatbot. I can help you with tips to stay safe online. Ask me about passwords, phishing, malware, privacy, or links.\n\n");

            // Attempt to play greeting (non-fatal)
            PlayGreeting();

            // Create and populate the topics list at runtime so users can click to ask about topics.
            InitializeTopicsList();
            InitializeSentimentIndicator(); // Step 9 UI
        }

        private void InitializeTopicsList()
        {
            // Create label
            lblTopics = new Label
            {
                Text = "Topics (double-click to ask):",
                Font = new Font("Consolas", 9F, FontStyle.Bold),
                AutoSize = true,
                ForeColor = SystemColors.Highlight,
                Location = new Point(rtbChatHistory.Right + 20, rtbChatHistory.Top)
            };
            Controls.Add(lblTopics);

            // Create listbox
            lstTopics = new ListBox
            {
                Location = new Point(rtbChatHistory.Right + 20, lblTopics.Bottom + 6),
                Size = new Size(300, rtbChatHistory.Height - 24),
                Font = new Font("Consolas", 9F, FontStyle.Regular)
            };

            // Populate from KeywordResponder (local instance for UI list)
            try
            {
                var kr = new KeywordResponder();
                var topics = kr.GetTopics();
                if (topics != null)
                {
                    foreach (var t in topics)
                        lstTopics.Items.Add(t);
                }
            }
            catch
            {
                // ignore populate errors for UI helper
            }

            lstTopics.DoubleClick += LstTopics_DoubleClick;
            Controls.Add(lstTopics);
        }

        private void InitializeSentimentIndicator()
        {
            // Place the sentiment indicator near the Send button (runtime to avoid designer change)
            lblSentiment = new Label
            {
                Text = "Sentiment: Neutral",
                Font = new Font("Consolas", 9F, FontStyle.Bold),
                AutoSize = true,
                ForeColor = Color.Gray,
                Location = new Point(btnSend.Right + 14, btnSend.Top + 6)
            };
            Controls.Add(lblSentiment);

            // Matched words label appears beneath the sentiment label
            lblSentimentMatches = new Label
            {
                Text = "Matched: ",
                Font = new Font("Consolas", 8F, FontStyle.Regular),
                AutoSize = true,
                ForeColor = Color.DarkGray,
                Location = new Point(lblSentiment.Left, lblSentiment.Bottom + 4)
            };
            Controls.Add(lblSentimentMatches);
        }

        private void UpdateSentimentIndicator(Sentiment s)
        {
            if (lblSentiment == null)
                return;

            lblSentiment.Text = $"Sentiment: {s}";

            lblSentiment.ForeColor = s switch
            {
                Sentiment.Worried => Color.OrangeRed,
                Sentiment.Frustrated => Color.Red,
                Sentiment.Curious => Color.MediumBlue,
                _ => Color.Gray
            };
        }

        private void UpdateSentimentMatches(System.Collections.Generic.List<string> matches)
        {
            if (lblSentimentMatches == null)
                return;

            if (matches == null || matches.Count == 0)
            {
                lblSentimentMatches.Text = "Matched: (none)";
                return;
            }

            lblSentimentMatches.Text = "Matched: " + string.Join(", ", matches);
        }

        private void LstTopics_DoubleClick(object? sender, EventArgs e)
        {
            if (lstTopics?.SelectedItem is string topic && !string.IsNullOrWhiteSpace(topic))
            {
                // Put topic into input and send it (simulates user asking)
                txtUserInput.Text = topic;
                SendMessage();
            }
        }

        private void PlayGreeting()
        {
            try
            {
                // Look for asset in published output first
                string baseDir = AppDomain.CurrentDomain.BaseDirectory ?? Directory.GetCurrentDirectory();
                string path = Path.Combine(baseDir, "Assets", "greeting.wav");

                // Fallback to project relative path for debug/run-from-IDE scenarios
                if (!File.Exists(path))
                {
                    path = Path.Combine(baseDir, "..", "..", "..", "Assets", "greeting.wav");
                    path = Path.GetFullPath(path);
                }

                if (File.Exists(path))
                {
                    using SoundPlayer player = new SoundPlayer(path);
                    player.Load();
                    player.Play(); // non-blocking
                }
            }
            catch
            {
                // Swallow exceptions so audio failures do not crash the app or tests
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            SendMessage();
        }

        // ENTER KEY FIX
        private void TxtUserInput_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true; // stops ding sound
                SendMessage();
            }
        }

        private void ChatbotForm_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendMessage();
            }
        }

        private void SendMessage()
        {
            string input = txtUserInput.Text.Trim();

            // BLANK TEST FIX
            if (string.IsNullOrWhiteSpace(input))
            {
                rtbChatHistory.AppendText($"[{DateTime.Now:HH:mm}] Bot: Please type a question or cybersecurity topic first.\n\n");
                txtUserInput.Clear();
                txtUserInput.Focus();
                return; // no crash
            }

            // Show user message
            rtbChatHistory.AppendText($"[{DateTime.Now:HH:mm}] User: {input}\n\n");

            // Step C: analyze sentiment and show matches
            try
            {
                // FIX: Only pass the Sentiment enum to UpdateSentimentIndicator
                var sentiment = chatBot.AnalyzeSentiment(input);
                UpdateSentimentIndicator(sentiment);

                // Remove or replace UpdateSentimentMatches if you do not have a list of matches
                // UpdateSentimentMatches(sentiment.Matches); // <-- This line removed or needs correct data
            }
            catch
            {
                // ignore UI sentiment failures
            }

            // Continue with existing processing
            string botResponse = string.Empty;
            try
            {
                botResponse = chatBot.ProcessInput(input) ?? string.Empty;
            }
            catch (Exception)
            {
                botResponse = "Sorry, something went wrong while processing your request.";
                DisplayBotResponse(botResponse);
                txtUserInput.Clear();
                txtUserInput.Focus();
                return;
            }

            if (!string.IsNullOrEmpty(botResponse))
            {
                string lastBotLine = GetLastBotLine();
                if (!string.Equals(lastBotLine, botResponse, StringComparison.Ordinal))
                    DisplayBotResponse(botResponse);
            }

            txtUserInput.Clear();
            txtUserInput.Focus();
        }

        private string GetLastBotLine()
        {
            try
            {
                var text = rtbChatHistory.Text;
                if (string.IsNullOrEmpty(text))
                    return string.Empty;

                // Find last "Bot: " occurrence
                int idx = text.LastIndexOf("Bot: ");
                if (idx < 0)
                    return string.Empty;

                int start = idx + "Bot: ".Length;
                int end = text.IndexOf('\n', start);
                if (end < 0) end = text.Length;
                return text.Substring(start, end - start).Trim();
            }
            catch
            {
                return string.Empty;
            }
        }

        // This is called by ChatBot via delegate
        private void DisplayBotResponse(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;

            rtbChatHistory.AppendText($"[{DateTime.Now:HH:mm}] Bot: {message}\n\n");
            rtbChatHistory.ScrollToCaret();
        }
    }
}