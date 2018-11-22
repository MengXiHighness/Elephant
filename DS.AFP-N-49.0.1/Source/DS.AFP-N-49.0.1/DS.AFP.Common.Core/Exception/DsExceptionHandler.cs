///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-11-13 11:13:09
/// 描  述：平台异常处理类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 异常处理类（负责异常的处理，记录异常日志）
    /// </summary>
    public class DsExceptionHandler :  IExceptionHandler
    {
        private static ILoggerFacade logger;
        private static object lock_obj = new object();
        public static ILoggerFacade Logger
        {
            get { return logger; }
            set 
            {
                lock (lock_obj)
                {
                    logger = value;
                }
            }
        }

        /// <summary>
        /// 异常处理
        /// <code>
        /// catch (Exception e)
        /// {
        ///     dsExceptionHandler.HandleException(e);
        ///     throw e;
        /// }
        /// </code>
        /// </summary>
        /// <param name="exception">待处理的异常</param>
        /// <returns>始终为true</returns>
        public bool HandleException(System.Exception exception)
        {
            if(Logger!=null)
                Logger.Error(exception);
            return true;
        }
    }
}
