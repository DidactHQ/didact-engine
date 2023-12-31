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

            for (int i = 0; i < _maxDegreeOfParallelism; i++)
            {
                _threads[i] = new Thread(() => { Console.WriteLine("Something"); })
                {
                    IsBackground = true,
                    Name = $"{nameof(DidactThreadPoolScheduler)} Thread {i}"
                };
            }

            _threads.ToList().ForEach(t => t.Start());
        }
    }
}
