using DidactEngine.TaskSchedulers;

namespace DidactEngine.Services.BackgroundServices
{
    public class WorkerBackgroundService : BackgroundService
    {
        private readonly ILogger<WorkerBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;

        public WorkerBackgroundService(ILogger<WorkerBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Starting {name}...", nameof(WorkerBackgroundService));

            try
            {
                var taskList = new List<Task>();
                var scheduler = ActivatorUtilities.CreateInstance<DidactThreadPoolScheduler>(_serviceProvider, Environment.ProcessorCount);
                var taskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskContinuationOptions.None, scheduler);

                for (int i = 0; i < 1; i++)
                {
                    var workerTask = taskFactory.StartNew(async () =>
                    {
                        while (!stoppingToken.IsCancellationRequested)
                        {
                            _logger.LogInformation("Task heartbeat. | threadName: {threadName} | isThreadPoolThread: {tpt} | scheduler: {scheduler}",
                                Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread, TaskScheduler.Current);
                            await Task.Delay(3000).ConfigureAwait(true);
                            _logger.LogInformation("Task heartbeat. | threadName: {threadName} | isThreadPoolThread: {tpt} | scheduler: {scheduler}",
                                Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread, TaskScheduler.Current);
                            await Task.Delay(3000).ContinueWith((task) => { }, scheduler: scheduler);
                            _logger.LogInformation("Task heartbeat. | threadName: {threadName} | isThreadPoolThread: {tpt} | scheduler: {scheduler}",
                                Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread, TaskScheduler.Current);
                        }
                    }, CancellationToken.None, TaskCreationOptions.None, scheduler);

                    _logger.LogInformation("Adding workerTask {i} to taskList", i.ToString());
                    taskList.Add(workerTask);
                }

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
