using Backend.Data;
using Backend.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "My API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Вставь токен так: Bearer {токен}"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")?.Trim();

if (string.IsNullOrWhiteSpace(connectionString))
    throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");


if (connectionString.StartsWith("postgres://", StringComparison.OrdinalIgnoreCase) ||
    connectionString.StartsWith("postgresql://", StringComparison.OrdinalIgnoreCase))
{
    var databaseUri = new Uri(connectionString);
    var userInfo = databaseUri.UserInfo.Split(':', 2);

    var db = databaseUri.AbsolutePath.TrimStart('/');

    connectionString =
        $"Host={databaseUri.Host};" +
        $"Port={(databaseUri.IsDefaultPort ? 5432 : databaseUri.Port)};" +
        $"Database={db};" +
        $"Username={Uri.UnescapeDataString(userInfo[0])};" +
        $"Password={Uri.UnescapeDataString(userInfo.Length > 1 ? userInfo[1] : "")};" +
        $"SSL Mode=Require;Trust Server Certificate=true;";
}

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.ConfigureWarnings(w =>
        w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning));

    options.UseNpgsql(connectionString);
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
        policy.WithOrigins("https://voice-ai-teacher.vercel.app", "http://localhost:5173")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Jwt:Key"]
            ?? throw new InvalidOperationException("JWT Key is missing in configuration");

        var issuer = builder.Configuration["Jwt:Issuer"] ?? "Backend";
        var audience = builder.Configuration["Jwt:Audience"] ?? "Frontend";

        options.TokenValidationParameters = new TokenValidationParameters
        {
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

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

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