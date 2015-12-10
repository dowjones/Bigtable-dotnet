using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BigtableNet.Common.Implementation
{
    /// <summary>
    /// The observable implementation is not in a stable state and should not be used
    /// </summary>
    /// <typeparam name="TStream"></typeparam>
    /// <typeparam name="TData"></typeparam>
    public class BigtableObservable<TStream, TData> : IObservable<TData> 
        where TData : class
    {
        private readonly IObservable<TData> _observable;
        private readonly Func<CancellationToken, Task<IAsyncEnumerator<TStream>>> _streamStarter;
        private readonly Func<TStream, TData> _converter;

        public BigtableObservable(Func<CancellationToken,Task<IAsyncEnumerator<TStream>>> streamStarter, Func<TStream, TData> converter)
        {
            _converter = converter;
            _streamStarter = streamStarter;
            _observable = Observable.Create<TData>(async (observer, token) => await Subscribe(observer,token));
        }

        public IDisposable Subscribe(IObserver<TData> observer)
        {
            return _observable.Subscribe(observer);
        }

        private async Task Subscribe(IObserver<TData> observer, CancellationToken token)
        {
            try
            {
                // Start a new stream per observer
                // This could be better, but for now I just need to 
                // support a single subscriber
                var stream = await _streamStarter(token);

                using (var queue = new IsolatatingQueue<TData>(observer.OnNext))
                {

                    // Iterate stream
                    while (await stream.MoveNext())
                    {
                        // Escape hatch, move next does support cancel even though it accepts it
                        if (token.IsCancellationRequested)
                        {
                            return;
                        }

                        // Send current item to isolation queue
                        queue.Enqueue(_converter(stream.Current));
                    }

                    // Wait for isolation queue to complete before destroying it
                    await queue.Wait();
                }

                // Announce completion
                observer.OnCompleted();
            }
            catch (Exception exception)
            {
                // Announce exception
                observer.OnError(exception);
            }
            
        }
    }
}
