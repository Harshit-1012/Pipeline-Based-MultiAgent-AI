using AiMultiAgentUI.Interfaces;
using AiMultiAgentUI.Services;

namespace AiMultiAgentUI.Agents
{
    public class BaseAgent
    {
        protected readonly IAIProvider _ai;
        protected readonly AgentLogger _logger;
        protected readonly string _name;

        protected BaseAgent(IAIProvider ai, AgentLogger logger, string name)
        {
            _ai = ai;
            _logger = logger;
            _name = name;
        }

        protected async Task<string> Execute(string prompt)
        {
            await _logger.Log($"🤖 {_name}: Thinking...");

            var result = await _ai.Ask(prompt);

            await _logger.Log($"✅ {_name}: Done");

            return result;
        }
    }
}
