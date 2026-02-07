using Backend.Data;
using Backend.Models;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Backend.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EssayController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IAIService _aiService;

    public EssayController(AppDbContext context, IAIService aiService)
    {
        _context = context;
        _aiService = aiService;
    }

    [HttpPost("check")]
public async Task<IActionResult> CheckEssay([FromBody] EssayRequest request)
{
    // 1. Получаем ID пользователя
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null) return Unauthorized();
    int userId = int.Parse(userIdClaim.Value);

    // 2. Находим пользователя в БД, чтобы узнать его уровень
    var user = await _context.Users.FindAsync(userId);
    if (user == null) return NotFound("User not found");

    // 3. Запрос к Gemini
    string systemInstruction = "You are an expert IELTS examiner. Provide a detailed feedback.";
    var feedback = await _aiService.GenerateContentAsync(request.Content, systemInstruction);

    // 4. Сохраняем эссе
    var essay = new Essay
    {
        Content = request.Content,
        Feedback = feedback,
        // Вместо заглушки "A2" берем реальный уровень пользователя из его профиля
        Score = user.LanguageLevel?.ToString() ?? "Not Tested",
        UserId = userId,
        CreatedAt = DateTime.UtcNow
    };

    _context.Essays.Add(essay);
    await _context.SaveChangesAsync();

    return Ok(new EssayResponse { Feedback = feedback, CheckedAt = essay.CreatedAt });
}
}