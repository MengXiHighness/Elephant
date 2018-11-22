using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;
using DS.AFP.Common.Core;

namespace DS.AFP.Data
{
    /// <summary>
    /// 数据访问异常处理
    /// </summary>
    public class DbExceptionHandler : ExceptionHandler, IExceptionHandler
    {
        public override bool HandleException(System.Exception exception)
        {
            ILoggerFacade logger = new LoggerFacade();
            logger.Error("Data access exception", exception);
            throw exception;
            return true;
        }
    }
}
