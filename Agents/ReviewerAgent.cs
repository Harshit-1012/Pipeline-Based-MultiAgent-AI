using AiMultiAgentUI.Interfaces;
using AiMultiAgentUI.Services;
using System.Runtime.InteropServices;

namespace AiMultiAgentUI.Agents
{
    public class ReviewerAgent : BaseAgent
    {
        public ReviewerAgent(IAIProvider ai, AgentLogger logger)
            : base(ai, logger, "Reviewer") { }

        public Task<string> Run(string input)
        {
            return Execute($"""
You are a Principal Engineer performing a design and code review.

Implementation:
{input}

Review the solution.

Include:

1. Strengths
2. Risks
3. Security Concerns
4. Scalability Concerns
5. Performance Concerns
6. Missing Requirements
7. Code Quality Assessment
8. Recommended Improvements
9. Final Verdict

Provide actionable feedback.

Output in markdown.
""");
        }
    }
    
}
