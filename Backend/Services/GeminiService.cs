using Google.GenAI; // Убедитесь, что выполнили: dotnet add package Google.GenAI
using Microsoft.Extensions.Configuration;

namespace Backend.Services;

public interface IAIService
{
    Task<string> ChatWithTeacher(string userMessage);
}

public class GeminiService : IAIService
{
    private readonly string _apiKey;

    public GeminiService(IConfiguration config)
    {
        _apiKey = config["Gemini:ApiKey"] ?? throw new ArgumentNullException("API Key missing");
    }

    public async Task<string> ChatWithTeacher(string userMessage)
    {
        try
        {
            // В официальном SDK 2026 года используется класс Client
            var client = new Client(apiKey: _apiKey);

            // Модель gemini-3-flash-preview доступна в v1beta
            var response = await client.Models.GenerateContentAsync(
                model: "gemini-3-flash-preview", 
                contents: $"You are a professional IELTS teacher. Student says: {userMessage}"
            );

            // Обращаемся к тексту ответа через структуру Candidates
            return response.Candidates?[0].Content?.Parts?[0].Text ?? "No response content";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}