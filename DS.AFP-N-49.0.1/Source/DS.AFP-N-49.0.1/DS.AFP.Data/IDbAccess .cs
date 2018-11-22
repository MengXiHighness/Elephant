using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NHibernate;
using Spring.Data.NHibernate;

namespace DS.AFP.Data
{
    /// <summary>
    /// 数据访问接口
    /// </summary>
    public interface IDbAccess : IHibernateOperations,IAdoDbAccess,INhibernateDbAccess
    {
        
    }
}
