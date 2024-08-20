using DidactCore.DependencyInjection;
using DidactCore.Engine;
using DidactCore.Flows;
using DidactEngine.TaskSchedulers;

namespace DidactEngine.Services.BackgroundServices
{
    public class WorkerBackgroundService : BackgroundService
    {
        private readonly ILogger<WorkerBackgroundService> _logger;
        private readonly IServiceProvider _serviceProvider;
        //private readonly IFlowRepository _flowRepository;
        //private readonly IFlowExecutor _flowExecutor;

        public WorkerBackgroundService(ILogger<WorkerBackgroundService> logger, IServiceProvider serviceProvider)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            //_flowRepository = flowRepository ?? throw new ArgumentNullException(nameof(flowRepository));
            //_flowExecutor = flowExecutor ?? throw new ArgumentNullException(nameof(flowExecutor));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Create a new scope for resolving scoped services
            using (var scope = _serviceProvider.CreateScope())
            {
                // Resolve the scoped services
                var engineSupervisor = scope.ServiceProvider.GetRequiredService<IEngineSupervisor>();
                var flowExecutor = scope.ServiceProvider.GetRequiredService<IFlowExecutor>();
                var flowRepository = scope.ServiceProvider.GetRequiredService<IFlowRepository>();
                var flowLogger = scope.ServiceProvider.GetRequiredService<IFlowLogger>();
                var flowConfigurator = scope.ServiceProvider.GetRequiredService<IFlowConfigurator>();
                var didactDependencyInjector = scope.ServiceProvider.GetRequiredService<IDidactDependencyInjector>();

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
                                /* Use taskFactory to create a second factory task inside the first factory task.
                                 * Here is why:
                                 * If a Flow uses ConfigureAwait(false) at any point, then when we execute the Flow, we lose our custom task scheduler and all of our custom threads until that chain of tasks completes.
                                 * We need a way to GUARANTEE that the next Flow STARTS BACK ONTO our custom task scheduler and our custom threads.
                                 * So we await the inner task, let it complete, and then start a new one for the next Flow execution.
                                 * ============================================================================
                                 * Think of this as an asynchronous context RESET between Flow executions.
                                 * ============================================================================
                                 * This is done infinitely inside of the first factory task's while loop, so it's an infinite parent task that continues Flow executions. */
                                await taskFactory.StartNew(async () =>
                                {
                                    /* Actual Flow steps:
                                     * 1. Get the Flow from a Queue and FlowRun.
                                     * 2. Create a Flow instance from the Flow.
                                     * 3. Execute the Flow instance.
                                     * */

                                    _logger.LogInformation("Task heartbeat 1. | threadName: {threadName} | isThreadPoolThread: {tpt} | scheduler: {scheduler}",
                                        Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread, TaskScheduler.Current);

                                    await Task.Delay(3000);

                                    _logger.LogInformation("Task heartbeat 2. | threadName: {threadName} | isThreadPoolThread: {tpt} | scheduler: {scheduler}",
                                        Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread, TaskScheduler.Current);

                                    await Task.Delay(3000).ConfigureAwait(false);

                                    _logger.LogInformation("Task heartbeat 3. | threadName: {threadName} | isThreadPoolThread: {tpt} | scheduler: {scheduler}",
                                        Thread.CurrentThread.Name, Thread.CurrentThread.IsThreadPoolThread, TaskScheduler.Current);

                                    await Task.Delay(3000);
                                }, CancellationToken.None, TaskCreationOptions.DenyChildAttach, scheduler).Unwrap(); // Make sure to Unwrap the task so that we await the inner task, not the factory task.
                            }
                        }, CancellationToken.None, TaskCreationOptions.DenyChildAttach, scheduler);

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
}
