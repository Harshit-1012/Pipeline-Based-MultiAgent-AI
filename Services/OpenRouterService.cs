using AiMultiAgentUI.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class OpenRouterService : IAIProvider
{
    private readonly HttpClient _http = new();
    private readonly string _apiKey;
    private readonly string _model;

    public OpenRouterService(IConfiguration config)
    {
        _apiKey = "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";
        _model = config["OpenRouter:Model"] ?? "openai/gpt-4o-mini";
    }

    public async Task<string> Ask(string prompt)
    {
        var request = new HttpRequestMessage(
            HttpMethod.Post,
            "https://openrouter.ai/api/v1/chat/completions"
        );

        request.Headers.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);

        request.Headers.Add("HTTP-Referer", "https://yourapp.com"); // required by OpenRouter
        request.Headers.Add("X-Title", "MyApp"); // optional but recommended

        var body = new
        {
            model = _model,
            messages = new[]
            {
                new { role = "user", content = prompt }
            }
        };

        request.Content = new StringContent(
            JsonSerializer.Serialize(body),
            Encoding.UTF8,
            "application/json"
        );

        var response = await _http.SendAsync(request);
        var json = await response.Content.ReadAsStringAsync();

        using var doc = JsonDocument.Parse(json);

        return doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? "";
    }
}