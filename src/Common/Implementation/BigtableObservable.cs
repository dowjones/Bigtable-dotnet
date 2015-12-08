using System;
using System.Collections.Generic;

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
        internal IDisposable Disposable { get; private set; }

        internal IAsyncEnumerator<TStream> Stream { get; private set; }

        internal Func<TStream, TData> Converter { get; private set; }

        public BigtableObservable(IDisposable disposable, IAsyncEnumerator<TStream> stream, Func<TStream, TData> converter)
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
