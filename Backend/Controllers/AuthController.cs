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
    private readonly ITokenService _tokenService;

    public AuthController(
        AppDbContext db,
        IPasswordService passwordService,
        ITokenService tokenService)
    {
        _db = db;
        _passwordService = passwordService;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto request)
    {
        // 1. Проверка, не занят ли Email
        if (await _db.Users.AnyAsync(u => u.Email == request.Email))
            return BadRequest("Email уже используется.");

        // 2. Создание пользователя без подтверждения почты
        var user = new User
        {
            Username = request.Username,
            Email = request.Email,
            PasswordHash = _passwordService.HashPassword(request.Password)
        };

        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return Ok(new { message = "Регистрация успешна. Теперь вы можете войти." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto request)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        
        // Проверка существования и пароля
        if (user == null || !_passwordService.VerifyPassword(request.Password, user.PasswordHash))
            return Unauthorized("Неверный Email или пароль.");
        
        // Сразу генерируем JWT
        var token = _tokenService.CreateToken(user);

        return Ok(new { 
            Token = token,
            User = new { user.Id, user.Username, user.Email }
        });
    }
}