namespace Backend.Models; 

public enum CefrLevel 
{ 
    A1, A2, B1, B2, C1, C2 
}

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; } = true;
    // public string? EmailConfirmationToken { get; set; }
    // Текущий уровень (может быть null, если тест еще не пройден)
    public CefrLevel? LanguageLevel { get; set; }
    // Флаг: установлен уровень системой или вручную пользователем
    public bool IsLevelManuallySet { get; set; } = false;
    // Фидбэк от ИИ в формате JSON или просто текст
    public string? AiAssessmentDetails { get; set; }
    public DateTime? LastTestedAt { get; set; }
    public string? PasswordResetToken { get; set; }
    public DateTime? ResetTokenExpires { get; set; }

}

public class RegisterDto
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class LoginDto
{
    public required string Email { get; set; } 
    public required string Password { get; set; }
}

public class InterviewRequest
{
    public int UserId { get; set; }
    public List<string> Answers { get; set; } = new();
}

public class NextQuestionResponse
{
    public string Question { get; set; } = string.Empty;
    public bool IsFinished { get; set; }
}

public class EssayRequest
{
    public string Content { get; set; } = string.Empty;
}

public class EssayResponse
{
    public string Feedback { get; set; } = string.Empty;
    public string? SuggestedLevel { get; set; }
    public DateTime CheckedAt { get; set; } = DateTime.UtcNow;
}