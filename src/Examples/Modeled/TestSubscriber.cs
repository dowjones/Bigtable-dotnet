using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigtableNet.Models.Types;
using Examples.Bootstrap;

namespace Examples.Modeled
{
    internal class TestSubscriber : IObserver<BigRow>
    {
        private readonly Action _onComplete;

        public TestSubscriber(Action onComplete = null)
        {
            _onComplete = onComplete;
        }

        public void OnCompleted()
        {
            CommandLine.InformUser("Observer", "Done.");

            if( _onComplete != null )
                _onComplete();
        }

        public void OnError(Exception exception)
        {
            CommandLine.RenderException(exception);
        }

        public void OnNext(BigRow value)
        {
            CommandLine.DisplayRows( new[] { value } );
        }
    }
}
