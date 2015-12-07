using System;
using System.Threading.Tasks;

namespace Examples.LowLevel.Instrumentation
{
    public class TimedOperation 
    {
        public TimeSpan ElapsedTime { get; protected set; }

        public long ElapsedMilliseconds
        {
            get { return (long)Math.Round(ElapsedTime.TotalMilliseconds); }
        }

        public TimedOperation()
        {
            ElapsedTime = TimeSpan.Zero;
        }

        public async Task<TimedOperation> MeasureVoidAsync(Func<Task> operation)
        {
            // Mark current time
            var startTime = DateTime.Now;

            // Perform operation
            await operation();

            // Record results
            ElapsedTime = DateTime.Now - startTime;

            // Return 'this' for syntactic composition
            return this;
        }
    }


    public class TimedOperation<T> : TimedOperation
    {
        public T Value { get; private set; }

        public async Task<TimedOperation<T>> MeasureAsync(Func<Task<T>> operation)
        {
            // Mark current time
            var startTime = DateTime.Now;

            // Perform operation
            Value = await operation();

            // Record results
            ElapsedTime = DateTime.Now - startTime;

            // Return 'this' for syntactic composition
            return this;
        }
    }
}
