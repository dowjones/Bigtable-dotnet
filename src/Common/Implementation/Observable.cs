using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BigtableNet.Common.Implementation
{
    public class Observable<TStream, TData> : IObservable<TData> 
        where TData : class
    {
        internal IDisposable Disposable { get; private set; }

        internal IAsyncEnumerator<TStream> Stream { get; private set; }

        internal Func<TStream, TData> Converter { get; private set; }

        public Observable(IDisposable disposable, IAsyncEnumerator<TStream> stream, Func<TStream, TData> converter)
        {
            Disposable = disposable;
            Converter = converter;
            Stream = stream;
        }

        public IDisposable Subscribe(IObserver<TData> observer)
        {
            var result = new AsyncEnumeratorObserver<TStream, TData>(Stream, observer, Converter);
            // Consider deleting
            result.Observe();
            return Disposable;
        }
    }
}
