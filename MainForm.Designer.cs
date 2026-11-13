using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CyberSafetyChatbotGUI
{
    partial class MainForm
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
            lblTitle = new Label();
            rtbChatHistory = new RichTextBox();
            txtUserInput = new TextBox();
            btnSend = new Button();
            rtbBanner = new RichTextBox();
            tabChat = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            tabPage3 = new TabPage();
            txtTaskTitle = new TextBox();
            txtTaskDescription = new TextBox();
            chkUseReminder = new CheckBox();
            dtpReminder = new DateTimePicker();
            buttonAddTask = new Button();
            dgvTasks = new DataGridView();
            tabChat.SuspendLayout();
            tabPage1.SuspendLayout();
            ((ISupportInitialize)dgvTasks).BeginInit();
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
            rtbBanner.Text = "";
            rtbBanner.TextChanged += rtbBanner_TextChanged;
            // 
            // tabChat
            // 
            tabChat.Controls.Add(tabPage1);
            tabChat.Controls.Add(tabPage2);
            tabChat.Controls.Add(tabPage3);
            tabChat.Location = new Point(466, 9);
            tabChat.Name = "tabChat";
            tabChat.SelectedIndex = 0;
            tabChat.Size = new Size(250, 49);
            tabChat.TabIndex = 5;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(txtTaskTitle);
            tabPage1.Location = new Point(4, 27);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(242, 18);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Tasks";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 27);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(242, 18);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Quiz";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            tabPage3.Location = new Point(4, 27);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(242, 18);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Activitiy";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtTaskTitle
            // 
            txtTaskTitle.Location = new Point(-4, -3);
            txtTaskTitle.Name = "txtTaskTitle";
            txtTaskTitle.Size = new Size(67, 25);
            txtTaskTitle.TabIndex = 0;
            // 
            // txtTaskDescription
            // 
            txtTaskDescription.Location = new Point(466, 54);
            txtTaskDescription.Multiline = true;
            txtTaskDescription.Name = "txtTaskDescription";
            txtTaskDescription.Size = new Size(125, 34);
            txtTaskDescription.TabIndex = 6;
            // 
            // chkUseReminder
            // 
            chkUseReminder.AutoSize = true;
            chkUseReminder.Location = new Point(466, 66);
            chkUseReminder.Name = "chkUseReminder";
            chkUseReminder.Size = new Size(102, 22);
            chkUseReminder.TabIndex = 7;
            chkUseReminder.Text = "checkBox1";
            chkUseReminder.UseVisualStyleBackColor = true;
            // 
            // dtpReminder
            // 
            dtpReminder.Location = new Point(0, -2);
            dtpReminder.Name = "dtpReminder";
            dtpReminder.Size = new Size(250, 25);
            dtpReminder.TabIndex = 8;
            // 
            // buttonAddTask
            // 
            buttonAddTask.Location = new Point(725, 100);
            buttonAddTask.Name = "buttonAddTask";
            buttonAddTask.Size = new Size(94, 29);
            buttonAddTask.TabIndex = 9;
            buttonAddTask.Text = "button1";
            buttonAddTask.UseVisualStyleBackColor = true;
            // 
            // dgvTasks
            // 
            dgvTasks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTasks.Location = new Point(-13, 54);
            dgvTasks.Name = "dgvTasks";
            dgvTasks.RowHeadersWidth = 51;
            dgvTasks.Size = new Size(300, 188);
            dgvTasks.TabIndex = 10;
            // 
            // ChatbotForm
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(988, 640);
            Controls.Add(dgvTasks);
            Controls.Add(buttonAddTask);
            Controls.Add(dtpReminder);
            Controls.Add(chkUseReminder);
            Controls.Add(txtTaskDescription);
            Controls.Add(tabChat);
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
            tabChat.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((ISupportInitialize)dgvTasks).EndInit();
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
        private TabControl tabChat;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private TabPage tabPage3;
        private TextBox txtTaskTitle;
        private TextBox txtTaskDescription;
        private CheckBox chkUseReminder;
        private DateTimePicker dtpReminder;
        private Button buttonAddTask;
        private DataGridView dgvTasks;
    }
}



