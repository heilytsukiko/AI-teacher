namespace Backend.Models
{
    public class AssessmentResult
    {
        public required string Level { get; set; } // Уровень
        public required string Feedback { get; set; } // Обратная связь
        public double ConfidenceScore { get; set; } // Уверенность
    }
}