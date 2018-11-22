using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.DS21;
using DS.AFP.Framework;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// 接收处理器接口
    /// </summary>
    public interface IReceiveResolver
    {
        void Handler(DSMsg dsmsg, IDS21Message ds21Message);
    }
}
