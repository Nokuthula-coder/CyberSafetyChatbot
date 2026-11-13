namespace CyberSafetyChatbotGUI.Models
{
    public sealed class ActivityEntry
    {
        public DateTime Timestamp { get; init; } = DateTime.Now;
        public string ActivityType { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string Outcome { get; init; } = string.Empty;
    }
}
