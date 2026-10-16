using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CyberSafetyChatbotGUI
{
    partial class ChatbotForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private IContainer components = null;

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ComponentResourceManager resources = new ComponentResourceManager(typeof(ChatbotForm));
            lblTitle = new Label();
            rtbChatHistory = new RichTextBox();
            txtUserInput = new TextBox();
            btnSend = new Button();
            rtbBanner = new RichTextBox();
            SuspendLayout();
            // 
            // lblTitle
            // 
            lblTitle.AutoSize = true;
            lblTitle.Font = new Font("Segoe UI", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTitle.ForeColor = SystemColors.Highlight;
            lblTitle.Location = new Point(44, 9);
            lblTitle.Name = "lblTitle";
            lblTitle.Size = new Size(416, 31);
            lblTitle.TabIndex = 0;
            lblTitle.Text = "CyberSafetyChatbot- Stay Safe Online";
            lblTitle.Click += label1_Click;
            // 
            // rtbChatHistory
            // 
            rtbChatHistory.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbChatHistory.Location = new Point(44, 286);
            rtbChatHistory.Name = "rtbChatHistory";
            rtbChatHistory.ReadOnly = true;
            rtbChatHistory.Size = new Size(537, 225);
            rtbChatHistory.TabIndex = 2;
            rtbChatHistory.TabStop = false;
            rtbChatHistory.Text = "";
            // 
            // txtUserInput
            // 
            txtUserInput.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            txtUserInput.Location = new Point(44, 592);
            txtUserInput.Name = "txtUserInput";
            txtUserInput.Size = new Size(309, 25);
            txtUserInput.TabIndex = 3;
            txtUserInput.KeyDown += TxtUserInput_KeyDown;
            // 
            // btnSend
            // 
            btnSend.BackColor = Color.DodgerBlue;
            btnSend.ForeColor = SystemColors.Control;
            btnSend.Location = new Point(359, 592);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(90, 29);
            btnSend.TabIndex = 4;
            btnSend.Text = "Send";
            btnSend.UseVisualStyleBackColor = false;
            btnSend.Click += btnSend_Click;
            // 
            // rtbBanner
            // 
            rtbBanner.BackColor = SystemColors.MenuText;
            rtbBanner.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rtbBanner.ForeColor = Color.LimeGreen;
            rtbBanner.Location = new Point(44, 85);
            rtbBanner.Name = "rtbBanner";
            rtbBanner.ReadOnly = true;
            rtbBanner.Size = new Size(561, 144);
            rtbBanner.TabIndex = 1;
            rtbBanner.Text = resources.GetString("rtbBanner.Text");
            // 
            // ChatbotForm
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(988, 640);
            Controls.Add(btnSend);
            Controls.Add(txtUserInput);
            Controls.Add(rtbChatHistory);
            Controls.Add(rtbBanner);
            Controls.Add(lblTitle);
            Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Name = "ChatbotForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "CyberSafetyChatbot";
            Load += ChatbotForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private Label lblTitle;
        private RichTextBox rtbChatHistory;
        private TextBox txtUserInput;
        private Button btnSend;
        private RichTextBox rtbBanner;

        // Add this method to the partial class ChatbotForm (in this file or the main ChatbotForm.cs file)
        private void label1_Click(object sender, EventArgs e)
        {
            // You can leave this empty or add desired behavior for the label click event
        }
    }
}



