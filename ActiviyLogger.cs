using CyberSafetyChatbotGUI.Models;

namespace CyberSafetyChatbotGUI.Services
{
    public sealed class ActivityLogger
    {
        private readonly List<ActivityEntry> entries = new();

        public IReadOnlyList<ActivityEntry> Entries => entries;

        public void Log(string activityType, string description, string outcome)
        {
            entries.Insert(0, new ActivityEntry
            {
                ActivityType = activityType,
                Description = description,
                Outcome = outcome
            });
        }

        public void Clear() => entries.Clear();
    }
}
