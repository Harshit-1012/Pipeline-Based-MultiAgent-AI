using AiMultiAgentUI.Models;

namespace AiMultiAgentUI.Interfaces
{
    public interface IWorkflowEngine
    {
        Task<WorkflowResult> RunAsync(string idea);
    }
}
