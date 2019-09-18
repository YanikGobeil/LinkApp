using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkMobile.Helpers
{
    public class UniqueExecutionTaskQueue
    {
        private readonly Queue<(Func<CancellationToken, Task>, CancellationToken)> _executionQueue;
        private readonly SemaphoreSlim _executionLock;
        private readonly SemaphoreSlim _cancellationLock;
        private readonly SemaphoreSlim _singleExecutionLock;

        private CancellationTokenSource _cancellationTokenSource;

        public UniqueExecutionTaskQueue()
        {
            _executionQueue = new Queue<(Func<CancellationToken, Task>, CancellationToken)>();
            _executionLock = new SemaphoreSlim(1, 1);
            _cancellationLock = new SemaphoreSlim(1, 1);
            _singleExecutionLock = new SemaphoreSlim(1, 1);
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public async Task Execute(Func<CancellationToken, Task> scheduleFunc, bool cancelRunningTask = true)
        {
            if (scheduleFunc == null)
                return;

            if (!cancelRunningTask)
                await _singleExecutionLock.WaitAsync();

            try
            {
                await _cancellationLock.WaitAsync();

                try
                {
                    _cancellationTokenSource.Cancel();
                    _cancellationTokenSource = new CancellationTokenSource();
                    _executionQueue.Enqueue((scheduleFunc, _cancellationTokenSource.Token));
                }
                finally
                {
                    _cancellationLock.Release();
                }

                await _executionLock.WaitAsync();

                try
                {
                    var (func, token) = _executionQueue.Dequeue();
                    await func(token);
                }
                finally
                {
                    _executionLock.Release();
                }
            }
            finally
            {
                if (!cancelRunningTask)
                    _singleExecutionLock.Release();
            }
        }

        public async Task Cancel()
        {
            await _cancellationLock.WaitAsync();

            try
            {
                _cancellationTokenSource.Cancel();
            }
            finally
            {
                _cancellationLock.Release();
            }
        }

        public bool IsPending()
        {
            return _executionQueue.Count > 0;
        }
    }
}
