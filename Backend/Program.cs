using Backend.Data;
using Backend.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// --- 1. РЕГИСТРАЦИЯ СЕРВИСОВ ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Вставь токен так: Bearer {токен}"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


// Настройка базы данных
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")?.Trim();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (!string.IsNullOrEmpty(connectionString) &&
        (connectionString.Contains("Host=") ||
         connectionString.StartsWith("postgres", StringComparison.OrdinalIgnoreCase)))
    {
        options.UseNpgsql(
            connectionString,
            npgsqlOptions => npgsqlOptions.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
        );
    }
    else
    {
        options.UseSqlite(connectionString ?? "Data Source=backend.db");
    }
});



builder.Services.AddSingleton<IPasswordService, PasswordService>(); 
builder.Services.AddHttpClient<AiInterviewService>();
builder.Services.AddScoped<AiInterviewService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAIService, GeminiService>();
builder.Services.AddScoped<ITokenService, TokenService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy
            .WithOrigins(
                "https://voice-ai-teacher.vercel.app",
                "http://localhost:5173"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
 
        var jwtKey = builder.Configuration["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key is missing in configuration");
        var issuer = builder.Configuration["Jwt:Issuer"] ?? "Backend";
        var audience = builder.Configuration["Jwt:Audience"] ?? "Frontend";

        options.TokenValidationParameters = new TokenValidationParameters {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey)),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = true 
        };
    });

    var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// --- 2. СБОРКА ПРИЛОЖЕНИЯ (ТОЛЬКО ОДИН РАЗ!) ---
var app = builder.Build();

// --- 3. АВТО-МИГРАЦИИ (Выполняются при старте) ---
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try 
    {
        var context = services.GetRequiredService<AppDbContext>();
        context.Database.Migrate(); 
        Console.WriteLine("Database check/migration completed successfully.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("=== MIGRATION ERROR ===");
        Console.WriteLine(ex.ToString());
    }
}

// --- 4. НАСТРОЙКА MIDDLEWARE ---
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty; 
});

app.UseCors("FrontendPolicy"); 

app.UseAuthentication(); 
app.UseAuthorization();  

app.MapControllers();

app.Run();