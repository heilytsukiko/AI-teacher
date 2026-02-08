namespace Backend.Models
{
    public class AssessmentResponse 
{
    public int Id { get; set; } 
    public string? Level { get; set; }
    public string? Feedback { get; set; }
    public double ConfidenceScore { get; set; }
}
}