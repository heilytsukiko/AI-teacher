using Microsoft.AspNetCore.Mvc;
using Backend.Services; // Это обязательная строка

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AIController : ControllerBase
{
    private readonly IAIService _aiService;

    public AIController(IAIService aiService)
    {
        _aiService = aiService;
    }

    [HttpPost("chat")]
    public async Task<IActionResult> Chat([FromBody] string message)
    {
        var response = await _aiService.ChatWithTeacher(message);
        return Ok(new { answer = response });
    }
}