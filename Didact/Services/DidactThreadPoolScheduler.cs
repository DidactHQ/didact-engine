using System.Collections.Concurrent;

namespace DidactEngine.Services
{
    public class DidactThreadPoolScheduler : TaskScheduler
    {
        private readonly ILogger<DidactThreadPoolScheduler> _logger;

        private readonly ThreadLocal<bool> _currentThreadIsExecuting = new(false);

        private readonly int _maxDegreeOfParallelism;

        private readonly Thread[] _threads;

        private readonly ConcurrentQueue<Task> _tasks;

        public DidactThreadPoolScheduler(ILogger<DidactThreadPoolScheduler> logger, int maxDegreeOfParallelism)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _maxDegreeOfParallelism = maxDegreeOfParallelism <= 0
                ? throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism))
                : maxDegreeOfParallelism;

            _tasks = new ConcurrentQueue<Task>();
            _threads = new Thread[maxDegreeOfParallelism];

            // Configure each thread
            for (int i = 0; i < _maxDegreeOfParallelism; i++)
            {
                _threads[i] = new Thread(() => ThreadExecutionLoop())
                {
                    IsBackground = true,
                    Name = $"{nameof(DidactThreadPoolScheduler)} Thread {i}"
                };
            }

            // Start each thread
            _threads.ToList().ForEach(t => t.Start());
        }

        private void ThreadExecutionLoop()
        {
            _currentThreadIsExecuting.Value = true;
            var currentThreadName = Thread.CurrentThread.Name;

            while (true)
            {
                try
                {
                    var taskDequeued = _tasks.TryDequeue(out var task);
                    if (taskDequeued)
                    {
                        TryExecuteTask(task!);
                    }
                }
                catch (ThreadInterruptedException ex)
                {
                    _logger.LogCritical("A {exName} occurred on thread {threadName}. See inner exception: {ex}", nameof(ThreadInterruptedException), currentThreadName, ex);
                    throw;
                }
                catch (Exception ex)
                {
                    _logger.LogCritical("An unhandled exception occurred on thread {threadName}. See inner exception: {ex}", currentThreadName, ex);
                    throw;
                }
            }
        }

        protected sealed override void QueueTask(Task task)
        {
            _tasks.Enqueue(task);
        }

        protected sealed override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            // If the task was previously enqueued, we can't arbitrarily remove it from the FIFO queue.
            // So we just have to wait for it to be executed.
            if (taskWasPreviouslyQueued)
            {
                return false;
            }
            // If the task was not previously enqueued, go ahead and inline execute it and skip the FIFO queue.
            else
            {
                return TryExecuteTask(task);
            }
        }

        protected sealed override IEnumerable<Task> GetScheduledTasks()
        {
            return _tasks.ToArray();
        }
    }
}
