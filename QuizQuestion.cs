namespace CyberSafetyChatbotGUI.Models
{
    public sealed class QuizQuestion
    {
        public int Id { get; init; }
        public string Question { get; init; } = string.Empty;
        public string QuestionText { get => Question; init => Question = value; }
        public IReadOnlyList<string> Options { get; init; } = Array.Empty<string>();
        public int CorrectOptionIndex { get; init; }
        public string Explanation { get; init; } = string.Empty;
    }
}
