using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;
using Backend.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IPasswordService _passwordService;
    private readonly IEmailService _emailService;

    // Один объединенный конструктор для всех сервисов
    public AuthController(
        AppDbContext db,
        IPasswordService passwordService,
        IEmailService emailService)
    {
        _db = db;
        _passwordService = passwordService;
        _emailService = emailService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto request)
    {
        // Проверка на существующего пользователя
        if (await _db.Users.AnyAsync(u => u.Email == request.Email))
            return BadRequest("Email уже используется.");

        var token = Guid.NewGuid().ToString();

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = _passwordService.HashPassword(request.Password),
            EmailConfirmationToken = token,
            EmailConfirmed = false // По умолчанию false
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        // Отправка письма с токеном
        await _emailService.SendConfirmationEmail(user.Email, token);

        return Ok("Регистрация успешна. Проверьте почту для подтверждения.");
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string token)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.EmailConfirmationToken == token);
        
        if (user == null) 
            return BadRequest("Неверный или просроченный токен.");

        user.EmailConfirmed = true;
        user.EmailConfirmationToken = null; // Удаляем токен после использования

        await _db.SaveChangesAsync();

        return Ok("Email подтверждён.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserDto request)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        
        if (user == null || !_passwordService.VerifyPassword(request.Password, user.PasswordHash))
        return Unauthorized("Неверный Email или пароль.");
        
        if (!user.EmailConfirmed)
        return BadRequest("Пожалуйста, подтвердите ваш Email перед входом.");

        return Ok("Вход выполнен успешно!");
    }
}