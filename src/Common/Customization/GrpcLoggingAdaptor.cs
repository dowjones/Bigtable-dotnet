using System;
using Grpc.Core.Logging;

namespace BigtableNet.Common.Customization
{
    /// <summary>
    /// Provides a logging adaptor that uses delegates to allow consumers to redirect grpc logging to any log implementation without implementing 
    /// the grpc logger interface. The delegates pass the Type which sourced the log, a function which returns the log string (allowing logs to be 
    /// filtered by log level andpreventing the extra work of formatting a string when the results will not be used, and for warnings and errors, 
    /// the exception that is associated with the log entry (or null if no exception is available).
    /// </summary>
    public class GrpcLoggingAdaptor : ILogger
    {
        private Action<Type, Func<string>> _info;
        private Action<Type, Func<string>> _debug;
        private Action<Type, Func<string>, Exception> _warning;
        private Action<Type, Func<string>, Exception> _error;

        private Type _type;

        private GrpcLoggingAdaptor()
        {
        }

        public GrpcLoggingAdaptor(Action<Type, Func<string>> info, Action<Type, Func<string>> debug, Action<Type, Func<string>, Exception> warning, Action<Type, Func<string>, Exception> error, Type type = null)
        {
            _error = error ?? NullAction;
            _warning = warning ?? NullAction;
            _debug = debug ?? NullAction;
            _info = info ?? NullAction;
            _type = type ?? typeof (GrpcLoggingAdaptor);
        }

        private void NullAction(Type type, Func<string> ignored)
        {
        }

        private void NullAction(Type type, Func<string> ignored, Exception alsoIgnored)
        {
        }

        public ILogger ForType<T>()
        {
            return new GrpcLoggingAdaptor
            {
                _debug = _debug,
                _info = _info,
                _warning = _warning,
                _error = _error,
                _type = _type
            };
        }

        public void Debug(string message)
        {
            _debug(_type, () => message);
        }

        public void Debug(string format, params object[] formatArgs)
        {
            _debug(_type, () => String.Format(format, formatArgs));
        }

        public void Info(string message)
        {
            _info(_type, () => message);
        }

        public void Info(string format, params object[] formatArgs)
        {
            _info(_type, () => String.Format(format, formatArgs));
        }

        public void Warning(string message)
        {
            _warning(_type, () => message, null);
        }

        public void Warning(string format, params object[] formatArgs)
        {
            _warning(_type, () => String.Format(format, formatArgs), null);
        }

        public void Warning(Exception exception, string message)
        {
            _warning(_type, () => message, exception);
        }

        public void Error(string message)
        {
            _error(_type, () => message, null);
        }

        public void Error(string format, params object[] formatArgs)
        {
            _error(_type, () => String.Format(format, formatArgs), null);
        }

        public void Error(Exception exception, string message)
        {
            _error(_type, () => message, exception);
        }
    }
}
