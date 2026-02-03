using Backend.Models;
using System.Text;
using System.Text.Json;

namespace Backend.Services;

public class AiInterviewService
{
    private readonly HttpClient _http;
    // Твой API ключ
    private readonly string _apiKey = "AIzaSyBsXY_BUKgi7qkMA-l-LJneLMKks4a3ph8";
    // URL для Gemini 1.5 Flash
    private const string GeminiUrl = "https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash:generateContent";

    public AiInterviewService(HttpClient httpClient)
    {
        _http = httpClient;
    }

    public async Task<string> GetNextQuestionAsync(List<string> chatHistory)
    {
        if (string.IsNullOrEmpty(_apiKey)) return "What is your main goal in learning English?";

        if (chatHistory.Count == 0)
        {
            return "Introduce yourself and tell me what you did yesterday.";
        }

        var prompt = $@"
        You are an adaptive English interviewer. Based on the previous conversation, ask ONE follow-up question to better determine the user's CEFR level.
        
        CONVERSATION SO FAR:
        {string.Join("\nUser: ", chatHistory)}

        STRATEGY:
        - If the user is fluent, ask more complex questions.
        - If the user struggles, ask simpler, everyday questions.
        - Do not repeat yourself.
        
        Return ONLY the text of the next question.";

        var requestBody = new { contents = new[] { new { parts = new[] { new { text = prompt } } } } };
        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        var response = await _http.PostAsync($"{GeminiUrl}?key={_apiKey}", content);
        
        if (!response.IsSuccessStatusCode) return "Could you tell me more about your experience with English?";

        var jsonResponse = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(jsonResponse);
        
        // Извлекаем текст из структуры ответа Gemini
        return doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text").GetString() ?? "What are your hobbies?";
    }

    public async Task<AssessmentResult> AnalyzeInterviewAsync(List<string> chatHistory)
    {
        var formattedHistory = string.Join("\nUser: ", chatHistory);

        var prompt = $@"
        Act as a professional CEFR English examiner. 
        Analyze the following interview:
        User: {formattedHistory}

        OUTPUT FORMAT:
        Return ONLY a JSON object: {{ ""Level"": ""B2"", ""Feedback"": ""text"", ""ConfidenceScore"": 0.95 }}
        Feedback must be in Russian. Levels: A1, A2, B1, B2, C1, C2.";

        var requestBody = new { contents = new[] { new { parts = new[] { new { text = prompt } } } } };
        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

        var response = await _http.PostAsync($"{GeminiUrl}?key={_apiKey}", content);
        
        if (!response.IsSuccessStatusCode)
        {
            return new AssessmentResult { Level = "A1", Feedback = "Ошибка связи с ИИ", ConfidenceScore = 0 };
        }

        var jsonResponse = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(jsonResponse);
        var aiText = doc.RootElement.GetProperty("candidates")[0].GetProperty("content").GetProperty("parts")[0].GetProperty("text").GetString();

        // Очистка от markdown (```json ... ```)
        var cleanJson = aiText?.Replace("```json", "").Replace("```", "").Trim() ?? "{}";

        return JsonSerializer.Deserialize<AssessmentResult>(cleanJson) 
               ?? new AssessmentResult { Level = "A1", Feedback = "Ошибка разбора", ConfidenceScore = 0 };
    }
}