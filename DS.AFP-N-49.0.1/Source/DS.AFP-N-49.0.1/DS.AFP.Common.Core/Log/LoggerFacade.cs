using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;
using DS.AFP.Common.Core;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 日志对外调用类（负责具体实现Debug日志、Error日志等的记录）
    /// <code>
    /// return new LoggerFacade().GetInstance("DS.AFP.WPF.App");
    /// </code>
    /// </summary>
    public class LoggerFacade : LoggerFacadeBase
    {
        private const string logName = "DS.AFP";

       
        public ILog Log
        {
            get
            {
                if(log == null)
                    log = LogManager.GetLogger(logName);
                return log;
            }
        }
        public LoggerFacade() { }
        public LoggerFacade(string logName)
        {
            log = LogManager.GetLogger(logName);
        }

        public LoggerFacade(Type type)
        {
            log = LogManager.GetLogger(type);
        }
        public override  LoggerFacade GetInstance(string logName = logName)
        {
            LoggerFacade lf = new LoggerFacade(logName);            
            return lf;
        }
        public override LoggerFacade GetInstance(Type type)
        {
            LoggerFacade lf = new LoggerFacade(type);            

            return lf;
        }
        private const string ExceptionMessageTemplate = "{0}\r\n{1}";

        /// <summary>
        /// 记录Debug类型日志
        /// </summary>
        public override void Debug(object message, Exception exception)
        {
            //InvokeLogEventHandler("Debug", ExceptionMessageTemplate.FormatString(message, exception.StackTrace));
            base.Debug(message, exception);
        }

        /// <summary>
        /// 记录Debug类型日志
        /// </summary>
        public override void Debug(object message)
        {
            //InvokeLogEventHandler("Debug", ExceptionMessageTemplate.FormatString(message, null));
            Log.Debug(message);
        }

        /// <summary>
        /// 记录Error类型日志
        /// </summary>
        public override void Error(object message, Exception exception)
        {
            //InvokeLogEventHandler("Error", ExceptionMessageTemplate.FormatString(message, exception.StackTrace));
            Log.Error(message, exception);
        }

        /// <summary>
        /// 记录Error类型日志
        /// </summary>
        public override void Error(object message)
        {
            //InvokeLogEventHandler("Debug", ExceptionMessageTemplate.FormatString(message, null));
            Log.Error(message);
        }

        /// <summary>
        /// 记录Fatal类型日志
        /// </summary>
        public override void Fatal(object message, Exception exception)
        {
            //InvokeLogEventHandler("Fatal", ExceptionMessageTemplate.FormatString(message, exception.StackTrace));
            Log.Fatal(message, exception);
        }

        /// <summary>
        /// 记录Fatal类型日志
        /// </summary>
        public override void Fatal(object message)
        {
            //InvokeLogEventHandler("Fatal", ExceptionMessageTemplate.FormatString(message, null));
            Log.Fatal(message);
        }

        /// <summary>
        /// 记录Info类型日志
        /// </summary>
        public override void Info(object message, Exception exception)
        {
            //InvokeLogEventHandler("Info", ExceptionMessageTemplate.FormatString(message, exception.StackTrace));
            Log.Info(message, exception);
        }

        /// <summary>
        /// 记录Info类型日志
        /// </summary>
        public override void Info(object message)
        {
            //InvokeLogEventHandler("Info", ExceptionMessageTemplate.FormatString(message, null));
            Log.Info(message);
        }

        /// <summary>
        /// 记录Trace类型日志
        /// </summary>
        public override void Trace(object message, Exception exception)
        {
            //InvokeLogEventHandler("Trace", ExceptionMessageTemplate.FormatString(message, exception.StackTrace));
            Log.Trace(message, exception);
        }

        /// <summary>
        /// 记录Trace类型日志
        /// </summary>
        public override void Trace(object message)
        {
            //InvokeLogEventHandler("Trace", ExceptionMessageTemplate.FormatString(message, null));
            Log.Trace(message);
        }

        /// <summary>
        /// 记录Warn类型日志
        /// </summary>
        public override void Warn(object message, Exception exception)
        {
            //InvokeLogEventHandler("Warn", ExceptionMessageTemplate.FormatString(message, exception.StackTrace));
            Log.Warn(message, exception);
        }

        /// <summary>
        /// 记录Warn类型日志
        /// </summary>
        public override void Warn(object message)
        {
            //InvokeLogEventHandler("Warn", ExceptionMessageTemplate.FormatString(message, null));
            Log.Warn(message);
        }

        /// <summary>
        /// 记录Debug类型日志
        /// </summary>
        public override void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            //InvokeLogEventHandler("Debug", ExceptionMessageTemplate.FormatString(null, exception.StackTrace));
            Log.Debug(formatProvider, formatMessageCallback, exception);
        }

        /// <summary>
        /// 记录Debug类型日志
        /// </summary>
        public override void Debug(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            Log.Debug(formatProvider, formatMessageCallback);
        }

        /// <summary>
        /// 记录Debug类型日志
        /// </summary>
        public override void Debug(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            //InvokeLogEventHandler("Debug", ExceptionMessageTemplate.FormatString(null, exception.StackTrace));
            Log.Debug(formatMessageCallback, exception);
        }

        /// <summary>
        /// 记录Debug类型日志
        /// </summary>
        public override void Debug(Action<FormatMessageHandler> formatMessageCallback)
        {
            Log.Debug(formatMessageCallback);
        }

        /// <summary>
        /// 自定义Debug输出格式
        /// </summary>
        public override void DebugFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            Log.DebugFormat(formatProvider, format, exception, args);
        }

        /// <summary>
        /// 自定义Debug输出格式
        /// </summary>
        public override void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Log.DebugFormat(formatProvider, format, args);
        }

        /// <summary>
        /// 自定义Debug输出格式
        /// </summary>
        public override void DebugFormat(string format, Exception exception, params object[] args)
        {
            Log.DebugFormat(format, exception, args);
        }

        /// <summary>
        /// 自定义Debug输出格式
        /// </summary>
        public override void DebugFormat(string format, params object[] args)
        {
            Log.DebugFormat(format, args);
        }

        /// <summary>
        /// 记录Error类型日志
        /// </summary>
        public override void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            //InvokeLogEventHandler("Error", ExceptionMessageTemplate.FormatString(null, exception.StackTrace));
            Log.Error(formatProvider, formatMessageCallback, exception);
        }

        /// <summary>
        /// 记录Error类型日志
        /// </summary>
        public override void Error(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            Log.Error(formatProvider, formatMessageCallback);
        }

        /// <summary>
        /// 记录Error类型日志
        /// </summary>
        public override void Error(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            //InvokeLogEventHandler("Error", ExceptionMessageTemplate.FormatString(null, exception.StackTrace));
            Log.Error(formatMessageCallback, exception);
        }

        /// <summary>
        /// 记录Error类型日志
        /// </summary>
        public override void Error(Action<FormatMessageHandler> formatMessageCallback)
        {
            Log.Error(formatMessageCallback);
        }

        /// <summary>
        /// 自定义Error输出格式
        /// </summary>
        public override void ErrorFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            Log.ErrorFormat(formatProvider, format,exception, args);
        }

        /// <summary>
        /// 自定义Error输出格式
        /// </summary>
        public override void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Log.ErrorFormat(formatProvider, format, args);
        }

        /// <summary>
        /// 自定义Error输出格式
        /// </summary>
        public override void ErrorFormat(string format, Exception exception, params object[] args)
        {
            Log.ErrorFormat(format, exception, args);
        }

        /// <summary>
        /// 自定义Error输出格式
        /// </summary>
        public override void ErrorFormat(string format, params object[] args)
        {
            Log.ErrorFormat(format, args);
        }

        /// <summary>
        /// 记录Fatal类型日志
        /// </summary>
        public override void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            //InvokeLogEventHandler("Fatal", ExceptionMessageTemplate.FormatString(null, exception.StackTrace));
            Log.Fatal(formatProvider, formatMessageCallback, exception);
        }

        /// <summary>
        /// 记录Fatal类型日志
        /// </summary>
        public override void Fatal(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            Log.Fatal(formatProvider, formatMessageCallback);
        }

        /// <summary>
        /// 记录Fatal类型日志
        /// </summary>
        public override void Fatal(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            //InvokeLogEventHandler("Fatal", ExceptionMessageTemplate.FormatString(null, exception.StackTrace));
            Log.Fatal(formatMessageCallback, exception);
        }

        /// <summary>
        /// 记录Fatal类型日志
        /// </summary>
        public override void Fatal(Action<FormatMessageHandler> formatMessageCallback)
        {
            Log.Fatal(formatMessageCallback);
        }

        /// <summary>
        /// 自定义Fatal输出格式
        /// </summary>
        public override void FatalFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            Log.FatalFormat(formatProvider, format, exception, args);
        }

        /// <summary>
        /// 自定义Fatal输出格式
        /// </summary>
        public override void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Log.FatalFormat(formatProvider, format, args);
        }

        /// <summary>
        /// 自定义Fatal输出格式
        /// </summary>
        public override void FatalFormat(string format, Exception exception, params object[] args)
        {
            Log.FatalFormat(format, exception, args);
        }

        /// <summary>
        /// 自定义Fatal输出格式
        /// </summary>
        public override void FatalFormat(string format, params object[] args)
        {
            Log.FatalFormat(format, args);
        }

        /// <summary>
        /// 记录Info类型日志
        /// </summary>
        public override void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            //InvokeLogEventHandler("Info", ExceptionMessageTemplate.FormatString(null, exception.StackTrace));
            Log.Info(formatProvider, formatMessageCallback, exception);
        }

        /// <summary>
        /// 记录Info类型日志
        /// </summary>
        public override void Info(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            Log.Info(formatProvider, formatMessageCallback);
        }

        /// <summary>
        /// 记录Info类型日志
        /// </summary>
        public override void Info(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            //InvokeLogEventHandler("Info", ExceptionMessageTemplate.FormatString(null, exception.StackTrace));
            Log.Info(formatMessageCallback, exception);
        }

        /// <summary>
        /// 记录Info类型日志
        /// </summary>
        public override void Info(Action<FormatMessageHandler> formatMessageCallback)
        {
            Log.Info(formatMessageCallback);
        }

        /// <summary>
        /// 自定义Info输出格式
        /// </summary>
        public override void InfoFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            Log.InfoFormat(formatProvider, format, exception, args);
        }

        /// <summary>
        /// 自定义Info输出格式
        /// </summary>
        public override void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Log.InfoFormat(formatProvider, format, args);
        }

        /// <summary>
        /// 自定义Info输出格式
        /// </summary>
        public override void InfoFormat(string format, Exception exception, params object[] args)
        {
            Log.InfoFormat(format, exception, args);
        }

        /// <summary>
        /// 自定义Info输出格式
        /// </summary>
        public override void InfoFormat(string format, params object[] args)
        {
            Log.InfoFormat(format, args);
        }

        /// <summary>
        /// 记录Trace类型日志
        /// </summary>
        public override void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            //InvokeLogEventHandler("Trace", ExceptionMessageTemplate.FormatString(null, exception.StackTrace));
            Log.Trace(formatProvider, formatMessageCallback, exception);
        }

        /// <summary>
        /// 记录Trace类型日志
        /// </summary>
        public override void Trace(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            Log.Trace(formatProvider, formatMessageCallback);
        }

        /// <summary>
        /// 记录Trace类型日志
        /// </summary>
        public override void Trace(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            //InvokeLogEventHandler("Trace", ExceptionMessageTemplate.FormatString(null, exception.StackTrace));
            Log.Trace(formatMessageCallback, exception);
        }

        /// <summary>
        /// 记录Trace类型日志
        /// </summary>
        public override void Trace(Action<FormatMessageHandler> formatMessageCallback)
        {
            Log.Trace(formatMessageCallback);
        }

        /// <summary>
        /// 自定义Trace输出格式
        /// </summary>
        public override void TraceFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            Log.TraceFormat(formatProvider, format, exception, args);
        }

        /// <summary>
        /// 自定义Trace输出格式
        /// </summary>
        public override void TraceFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Log.TraceFormat(formatProvider, format, args);
        }

        /// <summary>
        /// 自定义Trace输出格式
        /// </summary>
        public override void TraceFormat(string format, Exception exception, params object[] args)
        {
            Log.TraceFormat(format, exception, args);
        }

        /// <summary>
        /// 自定义Trace输出格式
        /// </summary>
        public override void TraceFormat(string format, params object[] args)
        {
            Log.TraceFormat(format, args);
        }

        /// <summary>
        /// 记录Warn类型日志
        /// </summary>
        public override void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            //InvokeLogEventHandler("Warn", ExceptionMessageTemplate.FormatString(null, exception.StackTrace));
            Log.Warn(formatProvider, formatMessageCallback, exception);
        }

        /// <summary>
        /// 记录Warn类型日志
        /// </summary>
        public override void Warn(IFormatProvider formatProvider, Action<FormatMessageHandler> formatMessageCallback)
        {
            Log.Warn(formatProvider, formatMessageCallback);
        }

        /// <summary>
        /// 记录Warn类型日志
        /// </summary>
        public override void Warn(Action<FormatMessageHandler> formatMessageCallback, Exception exception)
        {
            //InvokeLogEventHandler("Warn", ExceptionMessageTemplate.FormatString(null, exception.StackTrace));
            Log.Warn(formatMessageCallback, exception);
        }

        /// <summary>
        /// 记录Warn类型日志
        /// </summary>
        public override void Warn(Action<FormatMessageHandler> formatMessageCallback)
        {
            Log.Warn(formatMessageCallback);
        }

        /// <summary>
        /// 自定义Warn输出格式
        /// </summary>
        public override void WarnFormat(IFormatProvider formatProvider, string format, Exception exception, params object[] args)
        {
            Log.WarnFormat(formatProvider, format, exception, args);
        }

        /// <summary>
        /// 自定义Warn输出格式
        /// </summary>
        public override void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Log.WarnFormat(formatProvider, format, args);
        }

        /// <summary>
        /// 自定义Warn输出格式
        /// </summary>
        public override void WarnFormat(string format, Exception exception, params object[] args)
        {
            Log.WarnFormat(format, exception, args);
        }

        /// <summary>
        /// 自定义Warn输出格式
        /// </summary>
        public override void WarnFormat(string format, params object[] args)
        {
            Log.WarnFormat(format, args);
        }
    }

    /// <summary>
    /// 日志信息（含日志级别与日志信息）
    /// </summary>
    public class LogTraceInfo : EventArgs
    {
        public string Level
        {
            get;
            set;
        }
        public string Message
        {
            get;
            set;
        }
    }
}
