using Microsoft.AspNetCore.SignalR;

namespace Didact.Hubs
{
    public class BlockFlowStateMetricsHub : Hub
    {
        public async Task SendMessage(string message = "Test message")
        {
            await Clients.All.SendAsync("ReceiveMessage", message);
        }
    }
}
