using AiMultiAgentUI.Interfaces;
using AiMultiAgentUI.Services;

namespace AiMultiAgentUI.Agents
{
    public class ProductOwnerAgent : BaseAgent
    {
        public ProductOwnerAgent(IAIProvider ai, AgentLogger logger)
        : base(ai, logger, "Product Owner") { }

        public Task<string> Run(string idea)
        {
//            return Execute($"""
//Convert into requirements:

//{idea}
//""");

            return Execute($"""
                You are an experienced Product Owner.
                

Project Idea:
{idea}

Analyze the idea and create a software requirements document.

Include:

1. Project Summary
2. Business Goals
3. Target Users
4. Functional Requirements
5. Non-Functional Requirements
6. User Stories
7. Success Criteria
8. Assumptions and Constraints

Provide the output in clear markdown format.
""");
        }
    }
}
