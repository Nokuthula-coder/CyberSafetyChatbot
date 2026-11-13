namespace CyberSafetyChatbotGUI.Services
{
    public enum UserIntent
    {
        Unknown,
        AddTask,
        AddTaskWithReminder,
        StartQuiz,
        ShowTasks,
        ShowActivityLog,
        CybersecurityQuestion
    }

    public sealed class IntentResult
    {
        public UserIntent Intent { get; init; }
        public string TaskTitle { get; init; } = string.Empty;
        public DateTime? ReminderDate { get; init; }
    }
}
