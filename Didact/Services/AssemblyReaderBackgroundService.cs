namespace Didact.Services
{
    public class AssemblyReaderBackgroundService : BackgroundService
    {
        private readonly ILogger<AssemblyReaderBackgroundService> _logger;

        public AssemblyReaderBackgroundService(ILogger<AssemblyReaderBackgroundService> logger) =>
            _logger = logger;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting {name}...", nameof(AssemblyReaderBackgroundService));

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Ping from the {name}.", nameof(AssemblyReaderBackgroundService));
                await Task.Delay(7000);
            }
        }
    }
}
