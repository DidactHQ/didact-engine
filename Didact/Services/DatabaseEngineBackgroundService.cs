namespace Didact.Services
{
    public class DatabaseEngineBackgroundService : BackgroundService
    {
        private readonly ILogger<DatabaseEngineBackgroundService> _logger;

        public DatabaseEngineBackgroundService(ILogger<DatabaseEngineBackgroundService> logger) => 
            _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting {name}...", nameof(DatabaseEngineBackgroundService));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Ping from the {name}.", nameof(DatabaseEngineBackgroundService));
                await Task.Delay(5000);
            }
        }
    }
}
