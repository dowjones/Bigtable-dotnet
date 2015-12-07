using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigtableNet.Common.Implementation
{
    public class ChainedObservable<TStream, TData, T> : IObservable<T>
        where TData : class
        where T : class
    {
        private readonly IDisposable _disposable;

        private readonly IAsyncEnumerator<TStream> _stream;

        private readonly Func<TStream, T> _converter;

        public ChainedObservable(Observable<TStream, TData> source, Func<TData, T> converter)
        {
            _disposable = null;
            _converter = stream => converter(source.Converter(stream));
            _stream = source.Stream;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            var result = new AsyncEnumeratorObserver<TStream, T>(_stream, observer, _converter);
            // Consider deleting
            result.Observe();
            return _disposable;
        }
    }
}
