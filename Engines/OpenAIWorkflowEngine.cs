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

            var req = await po.Run(idea);
            var architecture = await arch.Run(req);
            var code = await dev.Run(architecture);
            var review = await rev.Run(code);

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
