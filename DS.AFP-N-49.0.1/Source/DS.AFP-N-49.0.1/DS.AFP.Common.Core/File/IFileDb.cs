using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Common.Core
{
    public interface IFileDB
    {
        void Write(PersistentData data);
    }
}
