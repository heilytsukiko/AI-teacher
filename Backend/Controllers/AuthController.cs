using Microsoft.AspNetCore.Mvc;
using Backend.Models;
using Backend.Services;
using Backend.Data;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _db;
    private readonly IPasswordService _passwordService;
    private readonly IEmailService _emailService;
    private readonly ITokenService _tokenService;

    public AuthController(
        AppDbContext db,
        IPasswordService passwordService,
        IEmailService emailService,
        ITokenService tokenService)
    {
        _db = db;
        _passwordService = passwordService;
        _emailService = emailService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        if (await _db.Users.AnyAsync(u => u.Email == request.Email))
            return BadRequest("Email уже используется.");

        var confirmationToken = Guid.NewGuid().ToString();

        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = _passwordService.HashPassword(request.Password),
            EmailConfirmationToken = confirmationToken,
            EmailConfirmed = false
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        // Отправка реального письма (или вызов сервиса)
        await _emailService.SendConfirmationEmail(user.Email, confirmationToken);

        return Ok(new { message = "Регистрация успешна. Проверьте почту для подтверждения." });
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail([FromQuery] string token)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.EmailConfirmationToken == token);
        
        if (user == null) 
            return BadRequest("Неверный или просроченный токен.");

        user.EmailConfirmed = true;
        user.EmailConfirmationToken = null; 

        await _db.SaveChangesAsync();

        return Ok("Email подтверждён успешно. Теперь вы можете войти.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        
        if (user == null || !_passwordService.VerifyPassword(request.Password, user.PasswordHash))
            return Unauthorized("Неверный Email или пароль.");
        
        if (!user.EmailConfirmed)
            return BadRequest("Пожалуйста, подтвердите ваш Email перед входом.");

        // Генерация JWT через наш сервис
        var token = _tokenService.CreateToken(user);

        return Ok(new { 
            Token = token,
            User = new { user.Id, user.Username, user.Email }
        });
    }
}