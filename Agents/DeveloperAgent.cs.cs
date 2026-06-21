using AiMultiAgentUI.Interfaces;
using AiMultiAgentUI.Services;

namespace AiMultiAgentUI.Agents
{
    public class DeveloperAgent : BaseAgent
    {
        public DeveloperAgent(IAIProvider ai, AgentLogger logger)
            : base(ai, logger, "Developer") { }

        public Task<string> Run(string input)
        {
            return Execute($"""
You are a Senior Full Stack Developer.


Architecture:
{input}

Create an implementation plan.

Include:

1. Project Structure
2. API Endpoints
3. Database Schema
4. Core Classes
5. Sample Backend Code
6. Error Handling Strategy
7. Validation Strategy
8. Development Roadmap

Generate realistic code snippets where appropriate.

Output in markdown.
""");
        }
    }
}
