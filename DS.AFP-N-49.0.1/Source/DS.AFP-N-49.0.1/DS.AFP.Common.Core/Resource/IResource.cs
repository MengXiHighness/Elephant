using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 资源接口（Resource的基类）
    /// </summary>
    public interface IResource
    {
        string GetString(string resKey);
        object GetObject(string resKey);
        Stream GetStream(string resKey);
    }
}
