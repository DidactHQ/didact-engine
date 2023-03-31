using Didact.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Didact.Services
{
    public class DatabaseEngineBackgroundService : BackgroundService
    {
        private readonly ILogger<DatabaseEngineBackgroundService> _logger;
        private readonly IHubContext<BlockFlowStateMetricsHub> _hubContext;

        public DatabaseEngineBackgroundService(ILogger<DatabaseEngineBackgroundService> logger, IHubContext<BlockFlowStateMetricsHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting {name}...", nameof(DatabaseEngineBackgroundService));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Ping from the {name}.", nameof(DatabaseEngineBackgroundService));
                await _hubContext.Clients.All.SendAsync("SendMessage");
                await Task.Delay(5000);
            }
        }
    }
}
