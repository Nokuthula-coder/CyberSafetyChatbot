namespace CyberSafetyChatbotGUI.Models
{
    public sealed class QuizAnswerResult
    {
        public int QuestionId { get; init; }
        public bool IsCorrect { get; init; }
        public int Score { get; init; }
        public int TotalQuestions { get; init; }
        public string Feedback { get; init; } = string.Empty;
    }
}
