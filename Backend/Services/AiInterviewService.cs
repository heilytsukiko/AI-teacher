using Backend.Models;
using System.Text;
using System.Text.Json;

namespace Backend.Services;

public class AiInterviewService
{
    private readonly HttpClient _http;
    private readonly string _apiKey;

    private const string GeminiUrl =
        "https://generativelanguage.googleapis.com/v1beta/models/gemini-3-flash-preview:generateContent";

    public AiInterviewService(HttpClient httpClient, IConfiguration config)
    {
        _http = httpClient;
        _apiKey = config["Gemini:ApiKey"] ?? "";
    }

    public async Task<string> GetNextQuestionAsync(List<string> chatHistory)
    {
        if (string.IsNullOrWhiteSpace(_apiKey))
            return "What is your main goal in learning English?";

        if (chatHistory == null || chatHistory.Count == 0)
            return "Introduce yourself and tell me what you did yesterday.";

        var prompt = $@"
You are an adaptive English interviewer. Based on the previous conversation, ask ONE follow-up question to better determine the user's CEFR level.

CONVERSATION SO FAR:
{string.Join("\nUser: ", chatHistory)}

STRATEGY:
- If the user is fluent, ask more complex questions.
- If the user struggles, ask simpler, everyday questions.
- Do not repeat yourself.

Return ONLY the text of the next question.";

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _http.PostAsync($"{GeminiUrl}?key={_apiKey}", content);

        if (!response.IsSuccessStatusCode)
            return "Could you tell me more about your experience with English?";

        var jsonResponse = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(jsonResponse);

        return doc.RootElement
            .GetProperty("candidates")[0]
            .GetProperty("content")
            .GetProperty("parts")[0]
            .GetProperty("text")
            .GetString() ?? "What are your hobbies?";
    }

    public async Task<AssessmentResponse> AnalyzeInterviewAsync(List<string> chatHistory)
    {
        var formattedHistory = chatHistory == null ? "" : string.Join("\nUser: ", chatHistory);

        var prompt = $@"
Act as a professional CEFR English examiner.
Analyze the following interview:
User: {formattedHistory}

OUTPUT FORMAT:
Return ONLY a JSON object: {{ ""Level"": ""B2"", ""Feedback"": ""text"", ""ConfidenceScore"": 0.95 }}
Feedback must be in Russian. Levels: A1, A2, B1, B2, C1, C2.";

        var requestBody = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _http.PostAsync($"{GeminiUrl}?key={_apiKey}", content);

        if (!response.IsSuccessStatusCode)
{
    var errorBody = await response.Content.ReadAsStringAsync();

    return new AssessmentResponse
    {
        Level = "A1",
        Feedback = $"Gemini error: {(int)response.StatusCode} {response.ReasonPhrase}\n{errorBody}",
        ConfidenceScore = 0
    };
}

        var jsonResponse = await response.Content.ReadAsStringAsync();
        using var doc = JsonDocument.Parse(jsonResponse);

var aiText = doc.RootElement
    .GetProperty("candidates")[0]
    .GetProperty("content")
    .GetProperty("parts")[0]
    .GetProperty("text")
    .GetString();

var cleanJson = aiText?
    .Replace("```json", "")
    .Replace("```", "")
    .Trim() ?? "{}";

var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var result = JsonSerializer.Deserialize<AssessmentResponse>(cleanJson, options)
             ?? new AssessmentResponse();

result.Level = string.IsNullOrWhiteSpace(result.Level)
    ? "A1"
    : result.Level.Trim();

result.Feedback = string.IsNullOrWhiteSpace(result.Feedback)
    ? "Недостаточно данных для оценки"
    : result.Feedback;

return result;
    }
}