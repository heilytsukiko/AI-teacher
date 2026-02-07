using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Data;
using Backend.Services;
using Backend.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] 
public class EssayController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IAIService _aiService;

    public EssayController(AppDbContext db, IAIService aiService)
    {
        _db = db;
        _aiService = aiService;
    }

    [HttpPost("check")]
    public async Task<IActionResult> CheckEssay([FromBody] EssayRequest request)
    {
        // 1. Валидация входных данных
        if (string.IsNullOrWhiteSpace(request.Content))
            return BadRequest("Essay content cannot be empty.");

        // 2. Извлечение ID пользователя из JWT
        var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdStr == null) return Unauthorized();

        // 3. Поиск пользователя в БД
        var user = await _db.Users.FindAsync(int.Parse(userIdStr));
        var userLevel = user?.LanguageLevel?.ToString() ?? "A2";

        // 4. Формирование инструкции (Prompt Engineering)
        // 
        string systemInstruction = $@"
            You are a professional IELTS Writing Examiner. 
            The student's current level is {userLevel}.
            Analyze the essay based on:
            1. Task Response
            2. Coherence and Cohesion
            3. Lexical Resource
            4. Grammatical Range and Accuracy
            
            Format your response clearly. Point out errors and provide a brief band score estimate.";

        // 5. Вызов ИИ сервиса
        var feedback = await _aiService.GenerateContentAsync(request.Content, systemInstruction);

        // 6. Возврат результата
        return Ok(new EssayResponse 
        { 
            Feedback = feedback,
            SuggestedLevel = userLevel, // В будущем ИИ может сам возвращать новый уровень
            CheckedAt = DateTime.UtcNow
        });
    }
}