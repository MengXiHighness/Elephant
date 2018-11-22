using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DS.AFP.Data
{
    public interface IConnection
    {
        IDbConnection DbConnection
        { get; }
    }
}
