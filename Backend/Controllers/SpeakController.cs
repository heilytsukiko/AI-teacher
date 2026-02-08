using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpeakController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IAIService _ai;

    private const string InitialAssistantMessage =
        "Hi! I'm your English learning assistant :) I'm still in development, but new features will be added in the future. In the meantime, let me know what you'd like to discuss.";

    public SpeakController(AppDbContext context, IAIService ai)
    {
        _context = context;
        _ai = ai;
    }

    [HttpPost]
    public async Task<IActionResult> Speak([FromBody] SpeakRequest req)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == req.UserId);
        if (user == null) return NotFound("Пользователь не найден");

        var level = user.LanguageLevel ?? CefrLevel.A1;

        Conversation conversation;

        if (req.ConversationId == null || req.ConversationId <= 0)
        {
            conversation = new Conversation { UserId = user.Id };
            _context.Conversations.Add(conversation);
            await _context.SaveChangesAsync();

            _context.Messages.Add(new Message
            {
                ConversationId = conversation.Id,
                Role = "assistant",
                Text = InitialAssistantMessage
            });

            await _context.SaveChangesAsync();

            return Ok(new SpeakResponse
            {
                ConversationId = conversation.Id,
                Text = InitialAssistantMessage
            });
        }

        conversation = await _context.Conversations
            .FirstOrDefaultAsync(c => c.Id == req.ConversationId && c.UserId == user.Id);

        if (conversation == null) return NotFound("Диалог не найден");


        _context.Messages.Add(new Message
        {
            ConversationId = conversation.Id,
            Role = "user",
            Text = req.Text
        });
        await _context.SaveChangesAsync();

        var lastMessages = await _context.Messages
            .Where(m => m.ConversationId == conversation.Id)
            .OrderByDescending(m => m.CreatedAt)
            .Take(12)
            .OrderBy(m => m.CreatedAt)
            .ToListAsync();

        var historyText = string.Join("\n", lastMessages.Select(m =>
            $"{m.Role.ToUpper()}: {m.Text}"
        ));

        var systemPrompt = BuildSystemPrompt(level);

        var combined = $"{historyText}\nASSISTANT:";
        var answer = await _ai.GenerateContentAsync(combined, systemPrompt);

        _context.Messages.Add(new Message
        {
            ConversationId = conversation.Id,
            Role = "assistant",
            Text = answer
        });
        await _context.SaveChangesAsync();

        return Ok(new SpeakResponse
        {
            ConversationId = conversation.Id,
            Text = answer
        });
    }

    private static string BuildSystemPrompt(CefrLevel level)
    {
        string target = level switch
        {
            CefrLevel.A1 => "A1+",
            CefrLevel.A2 => "A2+",
            CefrLevel.B1 => "B1+",
            CefrLevel.B2 => "B2+",
            CefrLevel.C1 => "C1",
            CefrLevel.C2 => "C2",
            _ => "A1"
        };

        return $@"
You are a friendly English speaking partner.

User CEFR level: {level}. Target: {target}.
Rules:
- Speak at the target level, not higher.
- Keep answers short (2–4 sentences).
- Ask exactly ONE follow-up question.
- Correct at most ONE mistake gently.
- Do NOT mention CEFR or levels.
";
    }
}
