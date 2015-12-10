using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BigtableNet.Common.Implementation
{
    public class IsolatatingQueue
    {
        public static int WaitDelaySliceMs = 1;
    }
    public class IsolatatingQueue<T> : IDisposable
    {
        private readonly AutoResetEvent _trigger = new AutoResetEvent(false);
        private readonly ManualResetEventSlim _doneLatch = new ManualResetEventSlim(false);
        private readonly Action<T> _onNext;
        private bool _quit;

        private List<T> _frontQueue = new List<T>();
        private List<T> _backQueue = new List<T>();

        public IsolatatingQueue(Action<T> onNext)
        {
            _onNext = onNext;
            ThreadPool.QueueUserWorkItem(ProcessQueue);
        }

        private void ProcessQueue(object state)
        {
            while (!_quit)
            {
                _trigger.WaitOne();

                var needsProcessing = false;

                lock (_trigger)
                {
                    if (_backQueue.Count > 0)
                    {
                        Console.WriteLine("Swap {0} and {1}", _backQueue.Count, _frontQueue.Count);
                        var temp = _backQueue;
                        _backQueue = _frontQueue;
                        _frontQueue = temp;
                        needsProcessing = true;
                    }
                }

                if (needsProcessing)
                {
                    foreach (var item in _frontQueue)
                    {
                        _onNext(item);
                    }
                    _frontQueue.Clear();
                }
            }

            _doneLatch.Set();
        }

        public void Enqueue(T data)
        {
            if (_quit)
            {
                throw new OperationCanceledException("IsolatingQueue enqueued after disposal");
            }

            lock (_trigger)
            {
                _backQueue.Add(data);
            }

            _trigger.Set();
        }

        public async Task Wait()
        {
            bool waitDone = false;
            while (!waitDone)
            {
                lock (_trigger)
                {
                    waitDone = _backQueue.Count + _frontQueue.Count == 0;
                }

                if (!waitDone)
                {
                    await Task.Delay(IsolatatingQueue.WaitDelaySliceMs);
                }
            }
        }

        public void Dispose()
        {
            _quit = true;
            _trigger.Set();
            _doneLatch.Wait();
        }
    }
}
