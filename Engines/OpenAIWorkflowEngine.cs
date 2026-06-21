using AiMultiAgentUI.Agents;
using AiMultiAgentUI.Interfaces;
using AiMultiAgentUI.Models;
using AiMultiAgentUI.Services;

namespace AiMultiAgentUI.Engines
{
    public class OpenAIWorkflowEngine : IWorkflowEngine
    {
        private readonly IAIProvider _ai;
        private readonly AgentLogger _logger;

        public OpenAIWorkflowEngine(IAIProvider ai, AgentLogger logger)
        {
            _ai = ai;
            _logger = logger;
        }
        public async Task<WorkflowResult> RunAsync(string idea)
        {
            var po = new ProductOwnerAgent(_ai, _logger);
            var arch = new ArchitectAgent(_ai, _logger);
            var dev = new DeveloperAgent(_ai, _logger);
            var rev = new ReviewerAgent(_ai, _logger);

            var req =  po.Run(idea).Result;
            var architecture =  arch.Run(req).Result;
            var code =  dev.Run(architecture).Result;
            var review = rev.Run(code).Result;

            return new WorkflowResult
            {
                Requirements = req,
                Architecture = architecture,
                Code = code,
                Review = review
            };
        }
    }
}
