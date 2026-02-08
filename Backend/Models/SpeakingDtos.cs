namespace Backend.Models;

public class SpeakRequest
{
    public int UserId { get; set; }
    public string Text { get; set; } = string.Empty;
    public int? ConversationId { get; set; } 
}

public class SpeakResponse
{
    public int ConversationId { get; set; }
    public string Text { get; set; } = string.Empty;
}
