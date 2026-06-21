using AiMultiAgentUI.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace AiMultiAgentUI.Services;

public class AgentLogger
{
    private readonly IHubContext<AgentHub> _hub;

    public AgentLogger(IHubContext<AgentHub> hub)
    {
        _hub = hub;
    }

    public async Task Log(string message)
    {
        await _hub.Clients.All.SendAsync("ReceiveLog", message);
    }


}