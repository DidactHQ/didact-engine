using DidactEngine.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace DidactEngine.Services.BackgroundServices
{
    public class AssemblyReaderBackgroundService : BackgroundService
    {
        private readonly ILogger<AssemblyReaderBackgroundService> _logger;
        private readonly IHubContext<BlockFlowStateMetricsHub> _hubContext;

        public AssemblyReaderBackgroundService(ILogger<AssemblyReaderBackgroundService> logger, IHubContext<BlockFlowStateMetricsHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting {name}...", nameof(AssemblyReaderBackgroundService));

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Ping from the {name}.", nameof(AssemblyReaderBackgroundService));
                    await _hubContext.Clients.All.SendAsync("SendMessage");
                    await Task.Delay(7000);
                }
            }
            catch (Exception ex)
            {
                _logger.LogCritical("{name} failed with the following exception:{nl}{exception}",
                    nameof(AssemblyReaderBackgroundService), Environment.NewLine, ex);
                throw;
            }
        }
    }
}
