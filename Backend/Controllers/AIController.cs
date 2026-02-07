using Microsoft.AspNetCore.Mvc;
using Backend.Services;

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
        // Передаем сообщение и инструкцию по поведению (System Prompt)
        var response = await _aiService.GenerateContentAsync(message, "You are a helpful and supportive IELTS teacher. Answer briefly and encourage the student.");
        
        return Ok(new { answer = response });
    }
}