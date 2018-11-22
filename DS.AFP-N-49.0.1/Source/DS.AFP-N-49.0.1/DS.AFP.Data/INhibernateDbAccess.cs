using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using NHibernate;

namespace DS.AFP.Data
{
    /// <summary>
    /// NhibernateDbAccess接口
    /// </summary>
    public interface INhibernateDbAccess
    {
        IQueryOver<T, T> QueryOver<T>() where T : class;
        IQueryOver<T, T> QueryOver<T>(Expression<Func<T>> alias) where T : class;
        IQueryOver<T, T> QueryOver<T>(string entityName) where T : class;
        IQueryOver<T, T> QueryOver<T>(string entityName, Expression<Func<T>> alias) where T : class;
    }
}
