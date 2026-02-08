using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Message
{
    public int Id { get; set; }

    public int ConversationId { get; set; }
    public Conversation Conversation { get; set; } = null!;

    [MaxLength(20)]
    public string Role { get; set; } = ""; // "user" | "assistant"

    [Required]
    public string Text { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
