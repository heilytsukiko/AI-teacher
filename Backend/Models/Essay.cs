using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Essay
{
    public int Id { get; set; }
    
    [Required]
    public string Content { get; set; } = string.Empty; // Текст эссе
    
    public string? Feedback { get; set; } // Ответ от Gemini
    
    public string? Score { get; set; } // Оценка (например, Band 7.0)
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Связь с пользователем
    public int UserId { get; set; }
    public User? User { get; set; }
}