using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace DS.AFP.Data
{
    /// <summary>
    /// DaoTemplate的封装类的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDaoTemplate<T> where T : class
    {
        ISession Session { get; }

        bool Save(T o);

        bool Save(T o, out object id);

        bool SaveOrUpdateAll(List<T> objectList);

        bool Delete(T o);

        bool Update(T o);

        bool DeleteAll(List<T> objectList);

        IList<T> GetAll();

        T Load(Object ID);
    }
}
