using Backend.Data;
using Backend.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- 1. РЕГИСТРАЦИЯ СЕРВИСОВ ---
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen();

// Настройка базы данных
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
{
    if (!string.IsNullOrEmpty(connectionString) && connectionString.Contains("Host=")) 
    {
        options.UseNpgsql(connectionString);
    }
    else 
    {
        options.UseSqlite(connectionString ?? "Data Source=backend.db");
    }
});

builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddHttpClient<AiInterviewService>();
builder.Services.AddScoped<AiInterviewService>();
builder.Services.AddSingleton<IPasswordService, PasswordService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IAIService, GeminiService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendPolicy", policy =>
    {
        policy.AllowAnyOrigin() 
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

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
        Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
    }
}

// --- 4. НАСТРОЙКА MIDDLEWARE ---
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("FrontendPolicy"); 
app.UseHttpsRedirection();
app.MapControllers();

app.Run();