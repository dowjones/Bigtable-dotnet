//using System;
//using System.Collections.Generic;
//using System.Reactive.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace BigtableNet.Common.Implementation
//{
//    /// <summary>
//    /// The observable implementation is not in a stable state and should not be used
//    /// </summary>
//    /// <typeparam name="TStream"></typeparam>
//    /// <typeparam name="TData"></typeparam>
//    public class AsyncEnumeratorObservable<TStream, TData> : IObservable<TData>
//        where TData : class
//    {
//        private readonly IAsyncEnumerator<TStream> _reader;
//        private readonly IObserver<TData> _observer;
//        private readonly Func<TStream, TData> _convert;
//        private readonly CancellationToken _cancellation;

//        public bool IsCancelled
//        {
//            get { return _cancellation.IsCancellationRequested; }
//        }

//        public AsyncEnumeratorObservable(IAsyncEnumerator<TStream> reader, IObserver<TData> observer, Func<TStream, TData> converter, CancellationToken cancellationToken = default(CancellationToken))
//        {
//            _convert = converter;
//            _reader = reader;
//            _observer = observer;
//            _cancellation = cancellationToken == default(CancellationToken) ? CancellationToken.None : cancellationToken;
//            var observable = 
//        }

//        public IDisposable Subscribe(IObserver<TData> observer)
//        {
//            return Observable.Create(async () =>
//                {
//                    try
//                    {
//                        // Iterate stream
//                        while (await _reader.MoveNext())
//                        {
//                            // Escape hatch
//                            if (_cancellation.IsCancellationRequested)
//                            {
//                                return;
//                            }

//                            // Convert entry and emit
//                            _observer.OnNext(_convert(_reader.Current));
//                        }

//                        // Announce completion
//                        _observer.OnCompleted();
//                    }
//                    catch (Exception exception)
//                    {
//                        // Announce exception
//                        _observer.OnError(exception);
//                    }
//            }).Subscribe(observer); //.OnNext,observer.OnError,observer.OnCompleted,_cancellation
//        }

//        public void Dispose()
//        {
//            _reader.Dispose();
//        }
//    }
//}
