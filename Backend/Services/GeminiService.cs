using Google.GenAI;

namespace Backend.Services;

public interface IAIService
{
    Task<string> GenerateContentAsync(string userMessage, string systemInstruction);
}

public class GeminiService : IAIService
{
    private readonly string _apiKey;

    public GeminiService(IConfiguration config)
    {
        _apiKey = config["Gemini:ApiKey"] ?? throw new ArgumentNullException("API Key missing");
    }

    public async Task<string> GenerateContentAsync(string userMessage, string systemInstruction)
    {
        try
        {
            var client = new Client(apiKey: _apiKey);

            // Формируем запрос, объединяя системную роль и текст пользователя
            var fullPrompt = $"{systemInstruction}\n\nStudent's work/message: {userMessage}";

            var response = await client.Models.GenerateContentAsync(
                model: "gemini-3-flash-preview", 
                contents: fullPrompt
            );

            return response.Candidates?[0].Content?.Parts?[0].Text ?? "No response content";
        }
        catch (Exception ex)
        {
            return $"Error: {ex.Message}";
        }
    }
}