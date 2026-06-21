namespace AiMultiAgentUI.Interfaces
{
    public interface IAIProvider
    {
        Task<string> Ask(string prompt);
    }
}
