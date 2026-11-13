namespace CyberSafetyChatbotGUI.Models
{
    public sealed class CyberTask
    {
        public int Id { get; set; }
        public int TaskId { get => Id; set => Id = value; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime? ReminderAt { get; set; }
        public DateTime? ReminderDate { get => ReminderAt; set => ReminderAt = value; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
