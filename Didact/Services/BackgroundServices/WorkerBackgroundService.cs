using System.Reflection;

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
                var scheduler = ActivatorUtilities.CreateInstance<DidactThreadPoolScheduler>(_serviceProvider, Environment.ProcessorCount);
                var taskFactory = new TaskFactory(CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskContinuationOptions.None, scheduler);

                for (int i = 0; i < 2; i++)
                {
                    Task workerTask;
                    if (i == 0)
                    {
                        workerTask = Task.Run(async () =>
                        {
                            var workerGuid = Guid.NewGuid();
                            ThreadPool.GetMaxThreads(out var workerThreadCount, out var ioThreadCount);

                            while (!stoppingToken.IsCancellationRequested)
                            {
                                _logger.LogInformation("Task heartbeat from Task.Run{Guid} | {now} | {scheduler} | {threadId} | isThreadPoolThread: {tpt} | threadName: {threadName}",
                                    workerGuid.ToString(), DateTime.Now.ToLongTimeString(), TaskScheduler.Current, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread, Thread.CurrentThread.Name);
                                await Task.Delay(3000);
                            }
                        });
                    }

                    else
                    {
                        workerTask = taskFactory.StartNew(async () =>
                        {
                            var workerGuid = Guid.NewGuid();
                            ThreadPool.GetMaxThreads(out var workerThreadCount, out var ioThreadCount);

                            while (!stoppingToken.IsCancellationRequested)
                            {
                                _logger.LogInformation("Task heartbeat from Task.Run{Guid} | {now} | {scheduler} | {threadId} | isThreadPoolThread: {tpt} | threadName: {threadName}",
                                    workerGuid.ToString(), DateTime.Now.ToLongTimeString(), TaskScheduler.Current, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread, Thread.CurrentThread.Name);
                                await Task.Delay(3000).ContinueWith((task) => { /*_logger.LogInformation("Continuing to custom scheduler...");*/ }, scheduler: scheduler);
                            }
                        });
                    }

                    _logger.LogInformation("Adding workerTask {i} to taskList", i.ToString());
                    taskList.Add(workerTask);
                }

                //var newTask = Task.Factory.StartNew(async () =>
                //{
                //    _logger.LogInformation("Hi");
                //    await Task.Delay(3000);
                //}, CancellationToken.None, TaskCreationOptions.RunContinuationsAsynchronously, TaskScheduler.Default);

                //await Task.WhenAll(taskList);
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
