using AiMultiAgentUI.Interfaces;
using AiMultiAgentUI.Services;

namespace AiMultiAgentUI.Agents
{
    public class ArchitectAgent : BaseAgent
    {
        public ArchitectAgent(IAIProvider ai, AgentLogger logger)
        : base(ai, logger, "Architect") { }

        public Task<string> Run(string input)
        {
            return Execute($"""
You are a Senior Software Architect.

Requirements:
{input}

Design a complete technical architecture.

Include:

1. Architecture Overview
2. Recommended Tech Stack
3. Frontend Components
4. Backend Components
5. Database Design
6. External Integrations
7. Security Considerations
8. Scalability Considerations
9. API Design Overview
10. Deployment Architecture

Provide diagrams in text format where useful.

Output in markdown.
"""); 
        }
    }
}
