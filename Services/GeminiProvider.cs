using AiMultiAgentUI.Interfaces;
using System.Text;
using System.Text.Json;

namespace AiMultiAgentUI.Services
{
    public class GeminiProvider : IAIProvider
    {
        private readonly HttpClient _http = new();
        private readonly string _key;

        public GeminiProvider(IConfiguration config)
        {
            _key = config["Gemini:ApiKey"];
        }

        public async Task<string> Ask(string prompt)
        {
            var url =
            $"https://generativelanguage.googleapis.com/v1beta/models/gemini-2.5-flash:generateContent?key={_key}";

            var body = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[] { new { text = prompt } }
                }
            }
            };

            var json = JsonSerializer.Serialize(body);

            var res = await _http.PostAsync(url,
                new StringContent(json, Encoding.UTF8, "application/json"));

            var content = await res.Content.ReadAsStringAsync();

            using var doc = JsonDocument.Parse(content);

            return doc.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString() ?? "";
        }
    }
}
