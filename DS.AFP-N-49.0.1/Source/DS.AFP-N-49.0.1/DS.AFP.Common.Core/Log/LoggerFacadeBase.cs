using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 日志对外调用类（LoggerFacade的基类）
    /// </summary>
    public abstract class LoggerFacadeBase : ILoggerFacade
    {
        static readonly object m_eventLock = new object();
        //static EventHandler<LogTraceInfo> logEvent;
        //public event EventHandler<LogTraceInfo> LogEvent
        //{
        //    //显式实现'add'方法
        //    add
        //    {
        //        //加私有锁，并向委托链表增加一个处理程序(以'value'为参数)
        //        lock (m_eventLock) { logEvent += value; }
        //    }
        //    //显式实现'remove'方法
        //    remove
        //    {
        //        //加私有锁，并从委托链表从中移除处理程序(以'value'为参数)
        //        lock (m_eventLock) { logEvent -= value; }
        //    }
        //}
        //protected void InvokeLogEventHandler(string level, string message)
        //{
        //    if (logEvent != null)
        //    {
        //        LogTraceInfo logTraceInfo = new LogTraceInfo() { Level = level, Message = message };
        //        logEvent(null, logTraceInfo);
        //    }
        //}

        protected ILog log = null;

        public abstract LoggerFacade GetInstance(string logName);

        public abstract LoggerFacade GetInstance(Type type);
       

        public  bool IsDebugEnabled
        {
            get { return log.IsDebugEnabled; }
        }

        public  bool IsErrorEnabled
        {
            get { return log.IsErrorEnabled; }
        }

        public virtual bool IsFatalEnabled
        {
            get { return log.IsFatalEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return log.IsInfoEnabled; }
        }

        public bool IsTraceEnabled
        {
            get { return log.IsTraceEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return log.IsWarnEnabled; }
        }

        public virtual void Debug(object message, Exception exception)
        {
            log.Debug(message, exception);
        }

        public virtual void Debug(object message)
        {
            log.Debug(message);
        }

        public virtual void Error(object message, Exception exception)
        {
            log.Error(message, exception);
        }

        public virtual void Error(object message)
        {
            log.Error(message);
        }

        public virtual void Fatal(object message, Exception exception)
        {
            log.Fatal(message, exception);
        }

        public virtual void Fatal(object message)
        {
            log.Fatal(message);
        }

        public virtual void Info(object message, Exception exception)
        {
            log.Info(message, exception);
        }

        public virtual void Info(object message)
        {
            log.Info(message);
        }

        public virtual void Trace(object message, Exception exception)
        {
            log.Trace(message, exception);
        }

        public virtual void Trace(object message)
        {
            log.Trace(message);
        }

        public virtual void Warn(object message, Exception exception)
        {
            log.Warn(message, exception);
        }

        public virtual void Warn(object message)
        {
            log.Warn(message);
        }

        public virtual void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            log.Debug(formatProvider, formatMessageCallback, exception);
        }

        public virtual void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            log.Debug(formatProvider, formatMessageCallback);
        }

        public virtual void Debug(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            log.Debug(formatMessageCallback, exception);
        }

        public virtual void Debug(Action<FormatMessageHandler> formatMessageCallback)
        {
            log.Debug(formatMessageCallback);
        }

        public virtual void DebugFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            log.DebugFormat(formatProvider, format, exception, args);
        }

        public virtual void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            log.DebugFormat(formatProvider, format, args);
        }

        public virtual void DebugFormat(string format, Exception exception, params object[] args)
        {
            log.DebugFormat(format, exception, args);
        }

        public virtual void DebugFormat(string format, params object[] args)
        {
            log.DebugFormat(format, args);
        }

        public virtual void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            log.Error(formatProvider, formatMessageCallback, exception);
        }

        public virtual void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            log.Error(formatProvider, formatMessageCallback);
        }

        public virtual void Error(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            log.Error(formatMessageCallback, exception);
        }

        public virtual void Error(Action<FormatMessageHandler> formatMessageCallback)
        {
            log.Error(formatMessageCallback);
        }

        public virtual void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            log.ErrorFormat(formatProvider, format,exception, args);
        }

        public virtual void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            log.ErrorFormat(formatProvider, format, args);
        }

        public virtual void ErrorFormat(string format, Exception exception, params object[] args)
        {
            log.ErrorFormat(format, exception, args);
        }

        public virtual void ErrorFormat(string format, params object[] args)
        {
            log.ErrorFormat(format, args);
        }

        public virtual void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            log.Fatal(formatProvider, formatMessageCallback, exception);
        }

        public virtual void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            log.Fatal(formatProvider, formatMessageCallback);
        }

        public virtual void Fatal(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            log.Fatal(formatMessageCallback, exception);
        }

        public virtual void Fatal(Action<FormatMessageHandler> formatMessageCallback)
        {
            log.Fatal(formatMessageCallback);
        }

        public virtual void FatalFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            log.FatalFormat(formatProvider, format, exception, args);
        }

        public virtual void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            log.FatalFormat(formatProvider, format, args);
        }

        public virtual void FatalFormat(string format, Exception exception, params object[] args)
        {
            log.FatalFormat(format, exception, args);
        }

        public virtual void FatalFormat(string format, params object[] args)
        {
            log.FatalFormat(format, args);
        }

        public virtual void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            log.Info(formatProvider, formatMessageCallback, exception);
        }

        public virtual void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            log.Info(formatProvider, formatMessageCallback);
        }

        public virtual void Info(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            log.Info(formatMessageCallback, exception);
        }

        public virtual void Info(Action<FormatMessageHandler> formatMessageCallback)
        {
            log.Info(formatMessageCallback);
        }

        public virtual void InfoFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            log.InfoFormat(formatProvider, format, exception, args);
        }

        public virtual void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            log.InfoFormat(formatProvider, format, args);
        }

        public virtual void InfoFormat(string format, Exception exception, params object[] args)
        {
            log.InfoFormat(format, exception, args);
        }

        public virtual void InfoFormat(string format, params object[] args)
        {
            log.InfoFormat(format, args);
        }

        public virtual void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            log.Trace(formatProvider, formatMessageCallback, exception);
        }

        public virtual void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            log.Trace(formatProvider, formatMessageCallback);
        }

        public virtual void Trace(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            log.Trace(formatMessageCallback, exception);
        }

        public virtual void Trace(Action<FormatMessageHandler> formatMessageCallback)
        {
            log.Trace(formatMessageCallback);
        }

        public virtual void TraceFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            log.TraceFormat(formatProvider, format, exception, args);
        }

        public virtual void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            log.TraceFormat(formatProvider, format, args);
        }

        public virtual void TraceFormat(string format, Exception exception, params object[] args)
        {
            log.TraceFormat(format, exception, args);
        }

        public virtual void TraceFormat(string format, params object[] args)
        {
            log.TraceFormat(format, args);
        }

        public virtual void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            log.Warn(formatProvider, formatMessageCallback, exception);
        }

        public virtual void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            log.Warn(formatProvider, formatMessageCallback);
        }

        public virtual void Warn(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            log.Warn(formatMessageCallback, exception);
        }

        public virtual void Warn(Action<FormatMessageHandler> formatMessageCallback)
        {
            log.Warn(formatMessageCallback);
        }

        public virtual void WarnFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            log.WarnFormat(formatProvider, format, exception, args);
        }

        public virtual void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            log.WarnFormat(formatProvider, format, args);
        }

        public virtual void WarnFormat(string format, Exception exception, params object[] args)
        {
            log.WarnFormat(format, exception, args);
        }

        public virtual void WarnFormat(string format, params object[] args)
        {
            log.WarnFormat(format, args);
        }
    }
}
