using AiMultiAgentUI.Interfaces;
using OpenAI.Chat;

namespace AiMultiAgentUI.Services
{
    public class OpenAIService : IAIProvider    
    {
        private readonly ChatClient _client;

        public OpenAIService(IConfiguration config)
        {
            var apiKey = config["OpenAI:ApiKey"];
            var model = config["OpenAI:Model"] ?? "gpt-4o-mini";

            _client = new ChatClient(model, apiKey);
        }

        public async Task<string> Ask(string prompt)
        {
            var response = await _client.CompleteChatAsync(prompt);
            return response.Value.Content[0].Text;
        }
        
    }
}
