///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-11-13 11:13:09
/// 描  述：平台异常类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 插件异常（主要属性有插件名称与消息号）
    /// </summary>
    public partial class AddinException : Exception
    {
        /// <summary>
        /// 插件名称
        /// </summary>
        public string AddinName
        {
            get;
            private set;
        }
        
        /// <summary>
        /// 消息号
        /// </summary>
        public string MessageNo
        {
            get;
            private set;
        }
      
        public AddinException() { }
        public AddinException(string addinName,string messageNo, string message) : this(addinName,messageNo, message, null) { }
        public AddinException(string message) : this("The unknown", "", message, null) { }
        public AddinException(string message, Exception innerException) : this("The unknown", "", message, innerException) { }

        public AddinException(string addinName, string message) : this(addinName, "", message, null) { }
        public AddinException(string addinName, string message, Exception innerException) : this(addinName,"", message, innerException) { }
        public AddinException(string addinName,string messageNo, string message, Exception innerException):base(message,innerException)
        {
            AddinName = addinName;
            MessageNo = messageNo;
            ILoggerFacade logger = new LoggerFacade();
            logger.Error(message, innerException);
        }
    }
}
