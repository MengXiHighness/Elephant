using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.AFP.Common.Core
{
    public interface IResourceProvider
    {
        object GetResource(string addinName_NodeName, string key);

        object GetHostResource(string nodeName, string key);
    }
}
