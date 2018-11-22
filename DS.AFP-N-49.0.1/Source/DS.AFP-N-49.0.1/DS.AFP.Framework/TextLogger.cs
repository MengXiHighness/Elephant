
using System;
using System.Globalization;
using System.IO;
using DS.AFP.Common.Core;
using DS.AFP.Framework;

namespace DS.AFP.Framework
{
    /// <summary>
    /// Implementation of <see cref="ILoggerFacade"/> that logs into a <see cref="TextWriter"/>.
    /// </summary>
    public class TextLogger : ILoggerFacade, IDisposable
    {
        private readonly TextWriter writer;

        /// <summary>
        /// Initializes a new instance of <see cref="TextLogger"/> that writes to
        /// the console output.
        /// </summary>
        public TextLogger()
            : this(Console.Out)
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="TextLogger"/>.
        /// </summary>
        /// <param name="writer">The writer to use for writing log entries.</param>
        public TextLogger(TextWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            this.writer = writer;
        }

        /// <summary>
        /// Disposes the associated <see cref="TextWriter"/>.
        /// </summary>
        /// <param name="disposing">When <see langword="true"/>, disposes the associated <see cref="TextWriter"/>.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (writer != null)
                {
                    writer.Dispose();
                }
            }
        }

        ///<summary>
        ///Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        ///</summary>
        /// <remarks>Calls <see cref="Dispose(bool)"/></remarks>.
        ///<filterpriority>2</filterpriority>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Debug(object message, Exception exception)
        {
            string messageToLog = String.Format(CultureInfo.InvariantCulture, Resources.DefaultTextLoggerPattern, DateTime.Now, message);
            if (exception != null)
                messageToLog += exception.StackTrace;
            writer.WriteLine(messageToLog);
        }

        public void Debug(object message)
        {
            string messageToLog = String.Format(CultureInfo.InvariantCulture, Resources.DefaultTextLoggerPattern, DateTime.Now, message);
            writer.WriteLine(messageToLog);
        }

        public void Error(object message, Exception exception)
        {
            string messageToLog = String.Format(CultureInfo.InvariantCulture, Resources.DefaultTextLoggerPattern, DateTime.Now, message);
            if (exception != null)
                messageToLog += exception.StackTrace;
            writer.WriteLine(messageToLog);
        }

        public void Error(object message)
        {
            string messageToLog = String.Format(CultureInfo.InvariantCulture, Resources.DefaultTextLoggerPattern, DateTime.Now, message);
            writer.WriteLine(messageToLog);
        }

        public void Fatal(object message, Exception exception)
        {
            string messageToLog = String.Format(CultureInfo.InvariantCulture, Resources.DefaultTextLoggerPattern, DateTime.Now, message);
            if (exception != null)
                messageToLog += exception.StackTrace;
            writer.WriteLine(messageToLog);
        }

        public void Fatal(object message)
        {
            string messageToLog = String.Format(CultureInfo.InvariantCulture, Resources.DefaultTextLoggerPattern, DateTime.Now, message);
            writer.WriteLine(messageToLog);
        }

        public void Info(object message, Exception exception)
        {
            string messageToLog = String.Format(CultureInfo.InvariantCulture, Resources.DefaultTextLoggerPattern, DateTime.Now, message);
            if (exception != null)
                messageToLog += exception.StackTrace;
            writer.WriteLine(messageToLog);
        }

        public void Info(object message)
        {
            string messageToLog = String.Format(CultureInfo.InvariantCulture, Resources.DefaultTextLoggerPattern, DateTime.Now, message);
            writer.WriteLine(messageToLog);
        }

        public bool IsDebugEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsErrorEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsFatalEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsInfoEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsTraceEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsWarnEnabled
        {
            get { throw new NotImplementedException(); }
        }

        public void Trace(object message, Exception exception)
        {
            string messageToLog = String.Format(CultureInfo.InvariantCulture, Resources.DefaultTextLoggerPattern, DateTime.Now, message);
            if (exception != null)
                messageToLog += exception.StackTrace;
            writer.WriteLine(messageToLog);
        }

        public void Trace(object message)
        {
            string messageToLog = String.Format(CultureInfo.InvariantCulture, Resources.DefaultTextLoggerPattern, DateTime.Now, message);
            writer.WriteLine(messageToLog);
        }

        public void Warn(object message, Exception exception)
        {
            string messageToLog = String.Format(CultureInfo.InvariantCulture, Resources.DefaultTextLoggerPattern, DateTime.Now, message);
            if (exception != null)
                messageToLog += exception.StackTrace;
            writer.WriteLine(messageToLog);
        }

        public void Warn(object message)
        {
            string messageToLog = String.Format(CultureInfo.InvariantCulture, Resources.DefaultTextLoggerPattern, DateTime.Now, message);
            writer.WriteLine(messageToLog);
        }
    }
}