///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-11-13 11:13:09
/// 描  述：日志接口类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Logging;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 日志接口（LoggerFacadeBase的基类）
    /// </summary>
    public interface ILoggerFacade : ILog
    {
        /// <summary>
        /// 根据名称得到日志实例
        /// </summary>
        /// <param name="logName"></param>
        /// <returns></returns>
        LoggerFacade GetInstance(string logName);

        /// <summary>
        /// 根据类型得到日志实例
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        LoggerFacade GetInstance(Type type);

        /// <summary>
        /// 日志事件
        /// </summary>
        //event EventHandler<LogTraceInfo> LogEvent;
    }
}
