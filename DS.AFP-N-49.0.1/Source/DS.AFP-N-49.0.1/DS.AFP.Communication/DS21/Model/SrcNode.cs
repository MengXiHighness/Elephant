using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.DS21
{
    /// <summary>
    /// DS21源节点类型
    /// </summary>
    public class SrcNode
    {

        public string Addr { get; set; }
        public string Type { get; set; }
        public string Id { get; set; }

        public SrcNode()
        {

        }

        ~SrcNode()
        {

        }

        public virtual void Dispose()
        {

        }

    }
}
