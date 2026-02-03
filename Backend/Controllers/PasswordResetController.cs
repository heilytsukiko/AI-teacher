using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Models;
using Backend.Services; // Чтобы видеть IPasswordService

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PasswordResetController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IPasswordService _passwordService; // Твой сервис

    public PasswordResetController(AppDbContext context, IPasswordService passwordService)
    {
        _context = context;
        _passwordService = passwordService;
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] string email)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null) return NotFound("Пользователь с таким Email не найден");

        user.PasswordResetToken = Guid.NewGuid().ToString();
        user.ResetTokenExpires = DateTime.UtcNow.AddMinutes(15);

        await _context.SaveChangesAsync();

        // Имитация отправки письма
        Console.WriteLine($"--- ССЫЛКА: https://ielts-teacher.com/reset-password?token={user.PasswordResetToken}");

        return Ok("Инструкции отправлены. Проверьте консоль бэкенда.");
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.PasswordResetToken == request.Token);

        if (user == null || user.ResetTokenExpires < DateTime.UtcNow)
        {
            return BadRequest("Токен недействителен или истек");
        }

        // Используем ТВОЙ метод хэширования
        user.PasswordHash = _passwordService.HashPassword(request.NewPassword);
        
        user.PasswordResetToken = null;
        user.ResetTokenExpires = null;

        await _context.SaveChangesAsync();

        return Ok("Пароль успешно обновлен");
    }
}

public class ResetPasswordRequest
{
    public string Token { get; set; } = string.Empty;
    public string NewPassword { get; set; } = string.Empty;
}