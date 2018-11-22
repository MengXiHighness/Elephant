using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using NHibernate;

namespace DS.AFP.Data
{
    /// <summary>
    /// 提供DaoTemplate的封装类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DaoTemplate<T> : IDaoTemplate<T> where T : class
    {
        /// <summary>
        /// 内部封装的NhibernateDbAccess
        /// </summary>
        public NhibernateDbAccess NhibernateDbAccess { get; set; }

        /// <summary>
        /// NhibernateDbAccess的会话工厂
        /// </summary>
        public NHibernate.ISession Session
        {
            get
            {
                ISession session = null;
                try
                {
                    session = NhibernateDbAccess.SessionFactory.GetCurrentSession();
                }
                catch
                {
                    if (session == null)
                        session = NhibernateDbAccess.SessionFactory.OpenSession();
                }
                return session;

            }
        }

        /// <summary>
        /// 封装NhibernateDbAccess的SaveOrUpdate
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Save(T o)
        {
            NhibernateDbAccess.SaveOrUpdate(o);
            return true;
        }

        /// <summary>
        /// 封装NhibernateDbAccess的Save
        /// </summary>
        /// <param name="o"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Save(T o, out object id)
        {
            id = NhibernateDbAccess.Save(o);
            return true;
        }

        /// <summary>
        /// 封装NhibernateDbAccess的SaveOrUpdateAll
        /// </summary>
        /// <param name="objectList"></param>
        /// <returns></returns>
        public bool SaveOrUpdateAll(List<T> objectList)
        {
            NhibernateDbAccess.SaveOrUpdateAll(objectList);
            return true;
        }

        /// <summary>
        /// 封装NhibernateDbAccess的Update
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Update(T o)
        {
            NhibernateDbAccess.Update(o);
            return true;
        }

        /// <summary>
        /// 封装NhibernateDbAccess的Delete
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool Delete(T o)
        {
            NhibernateDbAccess.Delete(o);
            return true;
        }

        /// <summary>
        /// 封装NhibernateDbAccess的DeleteAll
        /// </summary>
        /// <param name="objectList"></param>
        /// <returns></returns>
        public bool DeleteAll(List<T> objectList)
        {
            NhibernateDbAccess.DeleteAll(objectList);
            return true;
        }

        /// <summary>
        /// 封装NhibernateDbAccess的LoadAll
        /// </summary>
        /// <returns></returns>
        public IList<T> GetAll()
        {
            return NhibernateDbAccess.LoadAll(typeof(T)).OfType<T>().ToList<T>();
        }

        /// <summary>
        /// 封装NhibernateDbAccess的Get(typeof(T), ID)
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public T Load(object ID)
        {
            return NhibernateDbAccess.Get(typeof(T), ID) as T;
        }
    }
}
