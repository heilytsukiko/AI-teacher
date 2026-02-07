using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Models;
using Backend.Services;
using Backend.Data;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InterviewController : ControllerBase
{
    private readonly AiInterviewService _aiService;
    private readonly AppDbContext _context;

    public InterviewController(AiInterviewService aiService, AppDbContext context)
    {
        _aiService = aiService;
        _context = context;
    }

    [HttpGet("user-status/{id}")]
    public async Task<IActionResult> GetUserStatus(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound("Пользователь не найден");

        return Ok(new {
            id = user.Id,
            level = user.LanguageLevel?.ToString() ?? "Not Tested",
            feedback = user.AiAssessmentDetails,
            lastUpdate = user.LastTestedAt
        });
    }

    [HttpPost("next-question")]
    public async Task<IActionResult> GetNextQuestion([FromBody] List<string> currentAnswers)
    {
        if (currentAnswers.Count >= 5)
        {
            return Ok(new NextQuestionResponse { Question = "", IsFinished = true });
        }

        var nextQuestion = await _aiService.GetNextQuestionAsync(currentAnswers);
        return Ok(new NextQuestionResponse { Question = nextQuestion, IsFinished = false });
    }

    [HttpPost("analyze")]
    public async Task<IActionResult> AnalyzeInterview([FromBody] InterviewRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId);
        if (user == null) return NotFound("Пользователь не найден");

        try 
        {
            var result = await _aiService.AnalyzeInterviewAsync(request.Answers);

            // Сохраняем результат
            if (Enum.TryParse<CefrLevel>(result.Level?.Trim(), ignoreCase: true, out var level))
{
    user.LanguageLevel = level;
}
else
{
    user.LanguageLevel = null; // или CefrLevel.A1 как fallback, но лучше null
}
            user.AiAssessmentDetails = result.Feedback;
            user.LastTestedAt = DateTime.UtcNow;
            user.IsLevelManuallySet = false;

            await _context.SaveChangesAsync();

            return Ok(new 
            { 
                user.LanguageLevel, 
                user.AiAssessmentDetails, 
                Confidence = result.ConfidenceScore 
            });
        }
        catch (Exception ex)
        {
            return BadRequest($"Ошибка при анализе ИИ: {ex.Message}");
        }
    }
}
