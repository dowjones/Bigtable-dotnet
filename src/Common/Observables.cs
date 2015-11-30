using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigtableNet.Common
{
    public class Observable<TStream, TData> : IObservable<TData> where TData : class
    {
        internal IAsyncEnumerator<TStream> Stream { get; private set; }

        internal Func<TStream, TData> Converter { get; private set; }

        public Observable(IAsyncEnumerator<TStream> stream, Func<TStream, TData> converter)
        {
            Converter = converter;
            Stream = stream;
        }

        public IDisposable Subscribe(IObserver<TData> observer)
        {
            var result = new Observer<TStream, TData>(Stream, observer, Converter);
            return Task.Run(async () => await result.Read());
        }
    }

    public class ChainedObservable<TStream, TData, T> : IObservable<T>
        where TData : class
        where T : class
    {
        public IAsyncEnumerator<TStream> Stream { get; private set; }

        private readonly Func<TStream, T> _converter;

        public ChainedObservable(Observable<TStream, TData> source, Func<TData,T>  converter )
        {
            _converter = stream => converter(source.Converter(stream));
            Stream = source.Stream;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            var result = new Observer<TStream, T>(Stream, observer, _converter);
            return Task.Run(async () => await result.Read());
        }
    }

    public class Observer<TStream, TData> : IDisposable 
        where TData : class
    {
        private readonly IAsyncEnumerator<TStream> _reader;
        private readonly IObserver<TData> _observer;
        private readonly Func<TStream, TData> _convert;

        public Observer(IAsyncEnumerator<TStream> reader, IObserver<TData> observer, Func<TStream, TData> converter)
        {
            _convert = converter;
            _reader = reader;
            _observer = observer;
        }

        protected internal async Task Read()
        {
            try
            {
                while (await _reader.MoveNext())
                {
                    _observer.OnNext(_convert(_reader.Current));
                }

                _observer.OnCompleted();
            }
            catch (Exception exception)
            {
                _observer.OnError(exception);
            }
        }

        public void Dispose()
        {
            _observer.OnCompleted();
        }
    }
}
