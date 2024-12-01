



namespace Castle.DI.Comp.Tests.Utilities;

public class FauxLogger : ILogger
    {
        public ILogger CreateChildLogger(string loggerName)
        {
            return null;
        }

        public void Trace(string message)
        {
            Console.WriteLine(message);
        }

        public void Trace(Func<string> messageFactory)
        {
            Console.WriteLine(messageFactory());
        }

        public void Trace(string message, Exception exception)
        {
            Console.WriteLine(message);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void TraceFormat(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void TraceFormat(Exception exception, string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void TraceFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void Debug(string message)
        {
            Console.WriteLine(message);
        }

        public void Debug(Func<string> messageFactory)
        {
            Console.WriteLine(messageFactory());
        }

        public void Debug(string message, Exception exception)
        {
            Console.WriteLine(message);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void DebugFormat(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void DebugFormat(Exception exception, string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void Error(string message)
        {
            Console.WriteLine(message);
        }

        public void Error(Func<string> messageFactory)
        {
            Console.WriteLine(messageFactory());
        }

        public void Error(string message, Exception exception)
        {
            Console.WriteLine(message);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void ErrorFormat(Exception exception, string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void Fatal(string message)
        {
            Console.WriteLine(message);
        }

        public void Fatal(Func<string> messageFactory)
        {
            Console.WriteLine(messageFactory());
        }

        public void Fatal(string message, Exception exception)
        {
            Console.WriteLine(message);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void FatalFormat(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void FatalFormat(Exception exception, string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void Info(string message)
        {
            Console.WriteLine(message);
        }

        public void Info(Func<string> messageFactory)
        {
            Console.WriteLine(messageFactory());
        }

        public void Info(string message, Exception exception)
        {
            Console.WriteLine(message);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void InfoFormat(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void InfoFormat(Exception exception, string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void Warn(string message)
        {
            Console.WriteLine(message);
        }

        public void Warn(Func<string> messageFactory)
        {
            Console.WriteLine(messageFactory());
        }

        public void Warn(string message, Exception exception)
        {
            Console.WriteLine(message);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void WarnFormat(string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void WarnFormat(Exception exception, string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Console.WriteLine(format, args);
        }

        public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Console.WriteLine(format, args);
            Console.WriteLine(exception.Message);
            Console.WriteLine(exception.StackTrace);
        }

        public bool IsTraceEnabled { get; } = true;
        public bool IsDebugEnabled { get; } = true;
        public bool IsErrorEnabled { get; } = true;
        public bool IsFatalEnabled { get; } = true;
        public bool IsInfoEnabled { get; } = true;
        public bool IsWarnEnabled { get; } = true;
    }