using System.ComponentModel.DataAnnotations;

namespace Backend.Models;

public class Conversation
{
    public int Id { get; set; }

    public int UserId { get; set; }
    public User User { get; set; } = null!;

    public DateTime StartedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(200)]
    public string? Title { get; set; }

    public List<Message> Messages { get; set; } = new();
}
