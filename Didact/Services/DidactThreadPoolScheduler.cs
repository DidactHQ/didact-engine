using System.Collections.Concurrent;

namespace DidactEngine.Services
{
    public class DidactThreadPoolScheduler : TaskScheduler
    {
        private readonly ThreadLocal<bool> _currentThreadIsExecuting = new();

        private readonly int _maxDegreeOfParallelism;

        private readonly Thread[] _threads;

        private readonly ConcurrentQueue<Task> _tasks;

        public DidactThreadPoolScheduler(int maxDegreeOfParallelism)
        {
            _maxDegreeOfParallelism = maxDegreeOfParallelism <= 0
                ? throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism))
                : maxDegreeOfParallelism;

            // Do I need this?
            _currentThreadIsExecuting.Value = false;
            _tasks = new ConcurrentQueue<Task>();
            _threads = new Thread[maxDegreeOfParallelism];

            // Configure each thread
            for (int i = 0; i < _maxDegreeOfParallelism; i++)
            {
                _threads[i] = new Thread(() => { Console.WriteLine("Something"); })
                {
                    IsBackground = true,
                    Name = $"{nameof(DidactThreadPoolScheduler)} Thread {i}"
                };
            }

            // Start each thread
            _threads.ToList().ForEach(t => t.Start());
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
