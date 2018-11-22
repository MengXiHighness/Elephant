using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.DS21;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// 发送处理器接口
    /// </summary>
    public interface ISendResolver
    {
        string UnRouteHandler(DSMsg dsmsg);
        string RouteHandler(DSMsg dsmsg);

       
    }
}
