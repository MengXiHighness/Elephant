using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 平台基类异常（主属性有消息号）
    /// <code>
    /// public class ConfigurationFileNotExistException:AFPException
    /// {
    /// public ConfigurationFileNotExistException() : base("ConfigurationFileNotExistException", "配置文件不存在") { }
    /// }
    /// </code>
    /// </summary>
    public partial class AFPException : Exception
    {
        /// <summary>
        /// 消息号
        /// </summary>
        public string MessageNo
        {
            get;
            private set;
        }
       
        public AFPException() { }
        public AFPException(string messageNo, string message) : this(messageNo, message, null) { }
        public AFPException(string message, Exception innerException) : this("", message, innerException) { }
        public AFPException(string messageNo, string message, Exception innerException):base(message,innerException)
        {
            MessageNo = messageNo;
            ILoggerFacade logger = new LoggerFacade();
            logger.Error(message, innerException);
        }

    }
}
