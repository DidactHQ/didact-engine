using System.Reflection;

namespace DidactEngine.Services.BackgroundServices
{
    public class WorkerBackgroundService : BackgroundService
    {
        private readonly ILogger<WorkerBackgroundService> _logger;

        public WorkerBackgroundService(ILogger<WorkerBackgroundService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting {name}...", nameof(WorkerBackgroundService));

            //try
            //{
            //    while (!stoppingToken.IsCancellationRequested)
            //    {
            //        _logger.LogInformation("Ping from the {name}.", nameof(WorkerBackgroundService));
            //        await Task.Delay(5000);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogCritical("{name} failed with the following exception:{nl}{exception}",
            //        nameof(WorkerBackgroundService), Environment.NewLine, ex);
            //    throw;
            //}

            try
            {
                var taskList = new List<Task>();

                for (int i = 0; i < 10; i++)
                {
                    var workerTask = Task.Run(async () =>
                    {
                        var workerGuid = Guid.NewGuid();

                        while (!stoppingToken.IsCancellationRequested)
                        {
                            _logger.LogInformation("Task heartbeat from Task.Run{Guid} | {now}", workerGuid.ToString(), DateTime.Now.ToLongTimeString());
                            await Task.Delay(3000);
                        }
                    });

                    _logger.LogInformation("Adding workerTask {i} to taskList", i.ToString());
                    taskList.Add(workerTask);
                }

                //var newTask = Task.Factory.StartNew(async () =>
                //{
                //    _logger.LogInformation("Hi");
                //    await Task.Delay(3000);
                //}, CancellationToken.None, TaskCreationOptions.RunContinuationsAsynchronously, TaskScheduler.Default);

                await Task.WhenAll(taskList);
            }
            catch (Exception ex)
            {
                _logger.LogCritical("{name} failed with the following exception:{nl}{exception}",
                    nameof(WorkerBackgroundService), Environment.NewLine, ex);
                throw;
            }
        }
    }
}
