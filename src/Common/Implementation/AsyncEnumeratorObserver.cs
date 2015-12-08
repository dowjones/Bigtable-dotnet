using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace BigtableNet.Common.Implementation
{
    /// <summary>
    /// The observable implementation is not in a stable state and should not be used
    /// </summary>
    /// <typeparam name="TStream"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class AsyncEnumeratorObserver<TStream, TData> : IDisposable
        where TData : class
    {
        private readonly IAsyncEnumerator<TStream> _reader;
        private readonly IObserver<TData> _observer;
        private readonly Func<TStream, TData> _convert;
        private readonly CancellationToken _cancellation;
        private bool _isComplete;

        public bool IsCancelled
        {
            get { return _cancellation.IsCancellationRequested; }
        }

        public AsyncEnumeratorObserver(IAsyncEnumerator<TStream> reader, IObserver<TData> observer, Func<TStream, TData> converter, CancellationToken cancellationToken = default(CancellationToken))
        {
            _convert = converter;
            _reader = reader;
            _observer = observer;
            _cancellation = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
        }

        protected internal async Task Observe()
        {
            try
            {
                // Iterate stream
                while (await _reader.MoveNext())
                {
                    // Escape hatch
                    if (_cancellation.IsCancellationRequested)
                    {
                        await Task.Yield();
                        return;
                    }

                    // Convert entry and emit
                    _observer.OnNext(_convert(_reader.Current));
                }

                // Announce completion
                _isComplete = true;
                _observer.OnCompleted();
            }
            catch (Exception exception)
            {
                _observer.OnError(exception);
            }

            // Switch off grpc thread
            await Task.Yield();
        }

        public void Dispose()
        {
            if (!_isComplete && !IsCancelled)
            {
                _isComplete = true;

                // TODO: Should this actually fire OnCompleted
                _observer.OnCompleted();
            }
        }
    }
}
