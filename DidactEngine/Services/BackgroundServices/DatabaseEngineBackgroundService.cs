//using DidactEngine.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace DidactEngine.Services.BackgroundServices
{
    public class DatabaseEngineBackgroundService : BackgroundService
    {
        private readonly ILogger<DatabaseEngineBackgroundService> _logger;
        //private readonly IHubContext<BlockFlowStateMetricsHub> _hubContext;

        //public DatabaseEngineBackgroundService(ILogger<DatabaseEngineBackgroundService> logger, IHubContext<BlockFlowStateMetricsHub> hubContext)
        //{
        //    _logger = logger;
        //    _hubContext = hubContext;
        //}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting {name}...", nameof(DatabaseEngineBackgroundService));

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Ping from the {name}.", nameof(DatabaseEngineBackgroundService));
                    //await _hubContext.Clients.All.SendAsync("SendMessage");
                    await Task.Delay(5000);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical("{name} failed with the following exception:{nl}{exception}",
                    nameof(DatabaseEngineBackgroundService), Environment.NewLine, ex);
                throw;
            }
        }
    }
}
