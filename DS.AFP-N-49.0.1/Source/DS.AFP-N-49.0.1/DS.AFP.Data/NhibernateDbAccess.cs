using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.ServiceModel.Dispatcher;
using System.Text;
using NHibernate;
using Spring.Data.NHibernate;
using Spring.Data.Common;
using Spring.Data.Support;
using System.Data;
using Spring.Util;
using Spring.Context;
using NHibernate.Driver;
using Spring.Objects.Factory.Config;
using NHibernate.Connection;
using NHibernate.Engine;
using DS.AFP.Common.Core;

namespace DS.AFP.Data
{

    /// <summary>
    /// 数据库访问模板类库
    /// </summary>
    public class NhibernateDbAccess : HibernateTemplate, IDbAccess
    {
        public NhibernateDbAccess()
        {
            
        }

        /// <summary>
        /// 封装Session.QueryOver
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public NHibernate.IQueryOver<T, T> QueryOver<T>() where T : class
        {
            return this.Session.QueryOver<T>();
            
        }

        /// <summary>
        /// 封装Session.QueryOver
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        public NHibernate.IQueryOver<T, T> QueryOver<T>(System.Linq.Expressions.Expression<Func<T>> alias) where T : class
        {
            return this.Session.QueryOver<T>(alias);
        }

        /// <summary>
        /// 封装Session.QueryOver
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public NHibernate.IQueryOver<T, T> QueryOver<T>(string entityName) where T : class
        {
            return this.Session.QueryOver<T>(entityName);
        }

        /// <summary>
        /// 封装Session.QueryOver
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entityName"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        public NHibernate.IQueryOver<T, T> QueryOver<T>(string entityName, System.Linq.Expressions.Expression<Func<T>> alias) where T : class
        {
            return this.Session.QueryOver<T>(entityName, alias);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。所有其他的列和行将被忽略。 
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="commandType">指定如何解释命令字符串</param>
        /// <returns>结果集中第一行的第一列</returns>
        public object ExecuteScalar(System.Data.CommandType cmdType, string cmdText)
        {
            return this.ExecuteScalar(cmdType, cmdText, null);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。所有其他的列和行将被忽略。 
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="commandType">指定如何解释命令字符串</param>
        /// <param name="parameters">DbCommand 的参数</param>
        /// <returns>结果集中第一行的第一列</returns>
        public object ExecuteScalar(System.Data.CommandType cmdType, string cmdText, IDbParameters parameters )
        {
            object result = null;
            try
            {
                DbConnection connection = (DbConnection)this.Session.Connection;
                ITransaction trans = this.Session.Transaction;
                DbCommand command = connection.CreateCommand();
                command.CommandText = cmdText;
                command.CommandType = cmdType;
                if (null != parameters)
                {
                    ParameterUtils.CopyParameters(command, parameters);                    
                }
                trans.Enlist(command);
                result = command.ExecuteScalar();
            }
            catch (DbException e)
            {
                ExceptionHandler.AsynchronousThreadExceptionHandler = new DbExceptionHandler();
                ExceptionHandler.AsynchronousThreadExceptionHandler.HandleException(e);

            }
            return result;
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。所有其他的列和行将被忽略。 
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>结果集中第一行的第一列</returns>
        public object ExecuteScalar(string cmdText)
        {
            return this.ExecuteScalar(System.Data.CommandType.Text, cmdText);
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。所有其他的列和行将被忽略。 
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="parameters">DbCommand 的参数</param>
        /// <returns>结果集中第一行的第一列</returns>
        public object ExecuteScalar(string cmdText, IDbParameters parameters)
        {
            return this.ExecuteScalar(System.Data.CommandType.Text, cmdText, parameters);
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DbDataReader
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>结果集</returns>
        public System.Data.Common.DbDataReader QueryDataReader(string cmdText)
        {
            System.Data.Common.DbDataReader dr = null;
            try
            {
                DbConnection connection = (DbConnection)this.Session.Connection;
                ITransaction trans = this.Session.Transaction;
                DbCommand command = connection.CreateCommand();
                command.CommandText = cmdText;
                command.CommandType = System.Data.CommandType.Text;
                trans.Enlist(command);
                dr = command.ExecuteReader();
            }
            catch (DbException e)
            {
                ExceptionHandler.AsynchronousThreadExceptionHandler = new DbExceptionHandler();
                ExceptionHandler.AsynchronousThreadExceptionHandler.HandleException(e);
            }
            return dr;
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>结果集中第一行</returns>
        public System.Data.DataRow QueryDataRow(string cmdText)
        {
            System.Data.DataRow result = null;
            System.Data.DataTable dt = this.QueryDataTable(System.Data.CommandType.Text, cmdText, null);
            if (dt != null && dt.Rows.Count > 0)
            {
                result = dt.Rows[0];
            }
            return result;
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataSet
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="tblName">查询结果在DataSet里保存的表名</param>
        /// <returns>结果集</returns>
        public System.Data.DataSet QueryDataSet(string cmdText, string tblName)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.DataTable dt = this.QueryDataTable(System.Data.CommandType.Text, cmdText, null);
            dt.TableName = tblName;
            if (dt != null)
            {
                ds.Tables.Add(dt);
            }
            return ds;
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataSet
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="tableMappingName">查询结果在DataSet里保存的表名数组</param>
        /// <returns>结果集</returns>
        public System.Data.DataSet QueryDataSet(string cmdText, string[] tableMappingName)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            try
            {
                DbConnection connection = (DbConnection)this.Session.Connection;
                ITransaction trans = this.Session.Transaction;
                DbCommand command = connection.CreateCommand();
                command.CommandText = cmdText;
                command.CommandType = System.Data.CommandType.Text;
               // LocalSessionFactoryObject sessionfactory = GetDbProviderthis.SessionFactory as Spring.Data.NHibernate.LocalSessionFactoryObject;
                DbDataAdapter adapter = IDbProvider.CreateDataAdapter() as DbDataAdapter;
                adapter.SelectCommand = command;
                ITableMappingCollection mappingCollection = DoCreateMappingCollection(tableMappingName);
                foreach (DataTableMapping dataTableMapping in mappingCollection)
                {
                    adapter.TableMappings.Add(((ICloneable)dataTableMapping).Clone());
                }

                trans.Enlist(command);
                adapter.Fill(ds);
            }
            catch (DbException e)
            {
                ExceptionHandler.AsynchronousThreadExceptionHandler = new DbExceptionHandler();
                ExceptionHandler.AsynchronousThreadExceptionHandler.HandleException(e);
            }

            return ds;
        }

        private ITableMappingCollection DoCreateMappingCollection(string[] dataSetTableNames)
        {
            DataTableMappingCollection mappingCollection;

            if (dataSetTableNames == null)
            {
                dataSetTableNames = new string[] { "Table" };
            }
            foreach (string tableName in dataSetTableNames)
            {
                if (StringUtils.IsNullOrEmpty(tableName))
                {
                    throw new ArgumentException("TableName for DataTable mapping can not be null or empty");
                }
            }
            mappingCollection = new DataTableMappingCollection();
            int counter = 0;
            bool isFirstTable = true;
            foreach (string dataSetTableName in dataSetTableNames)
            {
                string sourceTableName;
                if (isFirstTable)
                {
                    sourceTableName = "Table";
                    isFirstTable = false;
                }
                else
                {
                    sourceTableName = "Table" + ++counter;
                }
                mappingCollection.Add(sourceTableName, dataSetTableName);
            }
            return mappingCollection;
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataTable
        /// </summary>
        /// <param name="commandType">指定如何解释命令字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <returns>结果集</returns>
        public System.Data.DataTable QueryDataTable(System.Data.CommandType commandType, string cmdText)
        {
            return this.QueryDataTable(commandType, cmdText, null);
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataTable
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>结果集</returns>
        public System.Data.DataTable QueryDataTable(string cmdText)
        {
            return this.QueryDataTable(System.Data.CommandType.Text, cmdText, null);
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataTable
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="startRecord">从其开始的从零开始的记录号</param>
        /// <param name="maxRecord">要检索的最大记录数</param>
        /// <returns>结果集</returns>
        public System.Data.DataTable QueryDataTable(string cmdText, int startRecord, int maxRecord)
        {
            System.Data.DataTable result = null;
            try
            {
                return  base.Execute(delegate(ISession session)
                {
                    string sql = "";
                    if (IDbProvider.DbMetadata.ProductName.ToLower().IndexOf("oracle") != -1)
                    {
                        sql = "SELECT * FROM (SELECT DST1.*, ROWNUM RN FROM ({0}) DST1 WHERE ROWNUM <= {2})WHERE RN >= {1}".FormatString(cmdText, startRecord, startRecord + maxRecord);
                    }
                    DbConnection connection = (DbConnection)session.Connection;
                    ITransaction trans = session.Transaction;
                    DbCommand command = connection.CreateCommand();
                    command.CommandText = sql;
                    command.CommandType = System.Data.CommandType.Text;                   
                
                    trans.Enlist(command);

                    result = new System.Data.DataTable();
                    System.Data.DataRow dt_dr = null;

                    using (DbDataReader dr = command.ExecuteReader())
                    {
                        int index = 0;
                        while (dr.Read())
                        {
                            if (index == 0)
                            {
                                for (int i = 0; i < dr.FieldCount; i++)
                                {
                                    result.Columns.Add(dr.GetName(i), dr.GetFieldType(i));
                                }
                            }
                            dt_dr = result.NewRow();
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                dt_dr[i] = dr[i];
                            }
                            result.Rows.Add(dt_dr);
                            index++;
                        }
                    }


                    return result;
                }) as DataTable;
               
            }
            catch (DbException e)
            {
                ExceptionHandler.AsynchronousThreadExceptionHandler = new DbExceptionHandler();
                ExceptionHandler.AsynchronousThreadExceptionHandler.HandleException(e);
            }
            return result;
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataTable
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="startRecord">从其开始的从零开始的记录号</param>
        /// <param name="maxRecord">要检索的最大记录数</param>
        /// <returns>结果集</returns>
        public IList<T> QueryDataTable<T>(string cmdText, int startRecord, int maxRecord) where T:class
        {
            try
            {
                return base.Execute(delegate(ISession session)
                {
                    string sql = "";
                    if (IDbProvider.DbMetadata.ProductName.ToLower().IndexOf("oracle") != -1)
                    {
                        sql = "SELECT * FROM (SELECT DST1.*, ROWNUM RN FROM ({0}) DST1 WHERE ROWNUM <= {2})WHERE RN >= {1}".FormatString(cmdText, startRecord, startRecord + maxRecord - 1);
                        
                    }
                    ISQLQuery query = session.CreateSQLQuery(sql).AddEntity(typeof(T));
                    return query.List<T>();

                }) as List<T>;

            }
            catch (DbException e)
            {
                ExceptionHandler.AsynchronousThreadExceptionHandler = new DbExceptionHandler();
                ExceptionHandler.AsynchronousThreadExceptionHandler.HandleException(e);
            }
            return null;
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataTable
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="sqlDA">DbDataAdapter对象</param>
        /// <returns>结果集</returns>
        public System.Data.DataTable QueryDataTable(string cmdText, out System.Data.Common.DbDataAdapter sqlDA)
        {
            System.Data.DataSet ds = new System.Data.DataSet();
            System.Data.DataTable dt = null;
            DbDataAdapter adapter = null;
            try
            {
                DbConnection connection = (DbConnection)this.Session.Connection;
                ITransaction trans = this.Session.Transaction;
                DbCommand command = connection.CreateCommand();
                command.CommandText = cmdText;
                command.CommandType = System.Data.CommandType.Text;
               // DSSessionFactoryObject sessionfactory = this.SessionFactory as Spring.Data.NHibernate.DSSessionFactoryObject;
                adapter = IDbProvider.CreateDataAdapter() as DbDataAdapter;
                adapter.SelectCommand = command;
                trans.Enlist(command);
                adapter.Fill(ds);
            }
            catch (DbException e)
            {
                ExceptionHandler.AsynchronousThreadExceptionHandler = new DbExceptionHandler();
                ExceptionHandler.AsynchronousThreadExceptionHandler.HandleException(e);
            }
            if (ds != null && ds.Tables.Count > 0)
            {
                dt = ds.Tables[0];
            }
            sqlDA = adapter;
            return dt;
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataTable
        /// </summary>
        /// <param name="commandType">指定如何解释命令字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="parameters">DbCommand 的参数</param>
        /// <returns>结果集</returns>
        public System.Data.DataTable QueryDataTable(System.Data.CommandType commandType, string cmdText, IDbParameters parameters)
        {
            System.Data.DataTable result = null;
            try
            {
                DbConnection connection = (DbConnection)this.Session.Connection;
                ITransaction trans = this.Session.Transaction;

                DbCommand command = connection.CreateCommand();
                command.CommandText = cmdText;
                command.CommandType = commandType;
                if (null != parameters)
                {
                    ParameterUtils.CopyParameters(command,parameters);

                }
                trans.Enlist(command);
                result = new System.Data.DataTable();
                System.Data.DataRow dt_dr = null;
                using (DbDataReader dr = command.ExecuteReader())
                {
                    int index = 0;
                    while (dr.Read())
                    {
                        if (index == 0)
                        {
                            for (int i = 0; i < dr.FieldCount; i++)
                            {
                                result.Columns.Add(dr.GetName(i), dr.GetFieldType(i));
                            }
                        }
                        dt_dr = result.NewRow();
                        for (int i = 0; i < dr.FieldCount; i++)
                        {
                            dt_dr[i] = dr[i];
                        }
                        result.Rows.Add(dt_dr);
                        index++;
                    }
                }
            }
            catch (DbException e)
            {
                ExceptionHandler.AsynchronousThreadExceptionHandler = new DbExceptionHandler();
                ExceptionHandler.AsynchronousThreadExceptionHandler.HandleException(e);
            }
            return result;
        }

        /// <summary>
        /// 可以使用 ExecuteNonQuery 执行编录操作（例如查询数据库的结构或创建诸如表等的数据库对象），或通过执行 UPDATE、INSERT 或 DELETE 语句更改数据库中的数据
        /// </summary>
        /// <param name="commandType">指定如何解释命令字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(System.Data.CommandType cmdType, string cmdText)
        {
            return this.ExecuteNonQuery(cmdType, cmdText, null);
        }

        /// <summary>
        /// 可以使用 ExecuteNonQuery 执行编录操作（例如查询数据库的结构或创建诸如表等的数据库对象），或通过执行 UPDATE、INSERT 或 DELETE 语句更改数据库中的数据
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="commandType">指定如何解释命令字符串</param>
        /// <param name="parameters">DbCommand 的参数</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(System.Data.CommandType cmdType, string cmdText, IDbParameters parameters)
        {
            int result = 0;
            try
            {
                DbConnection connection = (DbConnection)this.Session.Connection;
                ITransaction trans = this.Session.Transaction;

                DbCommand command = connection.CreateCommand();
                command.CommandText = cmdText;
                command.CommandType = cmdType;
                if (null != parameters)
                {
                    ParameterUtils.CopyParameters(command,parameters );

                }
                trans.Enlist(command);
                result = command.ExecuteNonQuery();
            }
            catch (DbException e)
            {
                ExceptionHandler.AsynchronousThreadExceptionHandler = new DbExceptionHandler();
                ExceptionHandler.AsynchronousThreadExceptionHandler.HandleException(e);
            }
            return result;
        }

        /// <summary>
        /// 可以使用 ExecuteNonQuery 执行编录操作（例如查询数据库的结构或创建诸如表等的数据库对象），或通过执行 UPDATE、INSERT 或 DELETE 语句更改数据库中的数据
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string cmdText)
        {
            return this.ExecuteNonQuery(cmdText, null);
        }

        /// <summary>
        /// 可以使用 ExecuteNonQuery 执行编录操作（例如查询数据库的结构或创建诸如表等的数据库对象），或通过执行 UPDATE、INSERT 或 DELETE 语句更改数据库中的数据
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="parameters">DbCommand 的参数</param>
        /// <returns>受影响的行数</returns>
        public int ExecuteNonQuery(string cmdText, IDbParameters parameters)
        {
            return this.ExecuteNonQuery(System.Data.CommandType.Text, cmdText, parameters);
        }

        /// <summary>
        /// 执行多句sql语句
        /// </summary>
        /// <param name="cmdText">sql语句数组</param>
        /// <returns>受影响的总行数</returns>
        public int ExecuteNonQuery(string[] cmdText)
        {
            StringBuilder sqllist = new StringBuilder();
            for (int i = 0; i < cmdText.Length; i++)
            {
                if (sqllist.Length > 0)
                {
                    sqllist.Append("\n");
                }
                sqllist.Append(cmdText[i]);
            }
            return this.ExecuteNonQuery(System.Data.CommandType.Text, sqllist.ToString(), null);
        }

        public IDbProvider IDbProvider
        {
            get {
                return GetDbProvider(base.SessionFactory);
            }
        }

        public static IDbProvider GetDbProvider(ISessionFactory sessionFactory)
        {
            //ISessionFactoryImplementor sfi = sessionFactory as ISessionFactoryImplementor;
            //if (sfi != null)
            //{
            //    Spring.Data.NHibernate.LocalSessionFactoryObject.DbProviderWrapper p = sfi.ConnectionProvider as Spring.Data.NHibernate.LocalSessionFactoryObject.DbProviderWrapper;
            //    if (p != null)
            //        return p.DbProvider;

            //}
            //return null;
            //修改获取DbProvider的方法
            IDbProvider provider =  SessionFactoryUtils.GetDbProvider(sessionFactory);
            if(provider==null)
            {
                ISessionFactoryImplementor sfi = sessionFactory as ISessionFactoryImplementor;
                if (sfi != null)
                {
                    Spring.Data.NHibernate.LocalSessionFactoryObject.DbProviderWrapper p = sfi.ConnectionProvider as Spring.Data.NHibernate.LocalSessionFactoryObject.DbProviderWrapper;
                    if (p != null)
                        return p.DbProvider;

                }
            }
            return provider;
        }
    }
}
