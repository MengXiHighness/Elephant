using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Web;
using Spring.Data;
using Spring.Dao;
using Spring.Data.Core;
using Spring.Data.Common;
using Spring.Transaction.Support;
using Spring.Transaction;
using System.Data.Common;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using DS.AFP.Common.Core;
using System.Collections.Generic;
using Spring.Data.Support;

namespace DS.AFP.Data
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    [Obsolete("已过期，不建议使用，请使用DbAccess")]
    public class AdoDbAccess : ITransAdoDbAccess, IConnection
    {
        IDbProvider _idbProvider;
        AdoPlatformTransactionManager _AdoPlatformTransactionManager;
        ITransactionStatus _ITransactionStatus;
        AdoTemplate _AdoTemplate;
        DsExceptionHandler dsExceptionHandler = new DsExceptionHandler();
        public IDbProvider IDbProvider
        {
            get
            {
                if (_idbProvider == null)
                    _idbProvider = Spring.Data.Common.DbProviderFactory.GetDbProvider(provider);
                if (_idbProvider.ConnectionString == null)
                    _idbProvider.ConnectionString = this.ConnnectString;
                return _idbProvider;
            }
        }
        string provider;
        public string ConnnectString
        {
            get;
            private set;
        }
        public string DataBaseName
        {
            get
            {
                return this.DbConnection.Database;
            }
        }
        //public string ServerName
        //{
        //    get
        //    {
        //        return this.DbConnection.DataSource;
        //    }
        //}
        public IDbConnection DbConnection
        {
            get;
            private set;
        }
        public bool IsInTrans
        {
            get;
            private set;
        }

        /// <summary>
        /// 初始化DbAccess
        /// </summary>
        /// <param name="key">在配置文件中Connections节点的key</param>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public AdoDbAccess(string key)
        {
            ConnectionElement ConnectionElement = new DS.AFP.Common.Core.ConfigurationNameSpace.DsConfigurationManager().DsRootConfigurationSection.Connections[key];
            this.provider = ConnectionElement.Provider;
            this.ConnnectString = ConnectionElement.ConnectionString;
            IDbProvider.ConnectionString = ConnectionElement.ConnectionString;
            this.DbConnection = ConnectionUtils.GetConnection(IDbProvider); 
            //this.DbConnection = IDbProvider.CreateConnection() as DbConnection;
            _AdoTemplate = new AdoTemplate(IDbProvider);            
        }

        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public AdoDbAccess(string provider,string connStr)
        {
            this.provider = provider;
            this.ConnnectString = connStr;
            IDbProvider.ConnectionString = connStr;
            this.DbConnection = IDbProvider.CreateConnection() as DbConnection;
            _AdoTemplate = new AdoTemplate(IDbProvider);
        }

        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        private void ReInit()
        {
            if (this.DbConnection!=null )
            {
                //this.DbConnection.Close();
                //this.DbConnection.Open();
            }
            
        }
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        private void ReConnection(Exception ex)
        {
            if (!ex.InnerException.Message.Contains("ORA-12571"))
                return;
            ReInit();
        }


        /// <summary>
        /// 开始事务
        /// </summary>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public void BeginTransaction()
        {
            if (_AdoPlatformTransactionManager != null)
            {
                CommitTransaction();//当已经开启过事务，默认提交该事务。
            }
            IDbProvider.ConnectionString = this.ConnnectString;
            _AdoPlatformTransactionManager = new AdoPlatformTransactionManager(IDbProvider);
            TransactionTemplate tt = new TransactionTemplate(_AdoPlatformTransactionManager);
            _AdoPlatformTransactionManager.TransactionSynchronization = TransactionSynchronizationState.Never;
            tt.TransactionIsolationLevel = IsolationLevel.ReadCommitted;
            DefaultTransactionDefinition df = new DefaultTransactionDefinition();
            df.PropagationBehavior = TransactionPropagation.Required;
            _ITransactionStatus = _AdoPlatformTransactionManager.GetTransaction(df);
            IsInTrans = true;
        }

        /// <summary>
        /// 开始事务
        /// </summary>
        /// <param name="_IsolationLevel">指定连接的事务锁定行为</param>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public void BeginTransaction(IsolationLevel _IsolationLevel)
        {
            if (_AdoPlatformTransactionManager != null)
            {
                CommitTransaction();//当已经开启过事务，默认提交该事务。
            }
            //初始化连接串。
            IDbProvider.ConnectionString = this.ConnnectString;
            _AdoPlatformTransactionManager = new AdoPlatformTransactionManager(IDbProvider);
            _AdoPlatformTransactionManager.TransactionSynchronization = TransactionSynchronizationState.Never;
            TransactionTemplate tt = new TransactionTemplate(_AdoPlatformTransactionManager);
            //事务隔离级别
            tt.TransactionIsolationLevel = _IsolationLevel;
            DefaultTransactionDefinition df = new DefaultTransactionDefinition();
            df.PropagationBehavior = TransactionPropagation.Required;
            _ITransactionStatus = _AdoPlatformTransactionManager.GetTransaction(df);
            IsInTrans = true;
        }

        /// <summary>
        /// 提交事务
        /// </summary>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public void CommitTransaction()
        {
            if (_AdoPlatformTransactionManager == null)
            {
                throw new Exception("Currently there is no open transactions, cannot be submitted");
            }
            _AdoPlatformTransactionManager.Commit(_ITransactionStatus);
            _AdoPlatformTransactionManager = null;
            IsInTrans = false;
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public void RollbackTransaction()
        {
            if (_AdoPlatformTransactionManager == null)
            {
                throw new Exception("Currently there is no open transactions, cannot be rolled back");
            }
            _AdoPlatformTransactionManager.Rollback(_ITransactionStatus);
            _AdoPlatformTransactionManager = null;
            IsInTrans = false;
        }

        /// <summary>
        /// 可以使用 ExecuteNonQuery 执行编录操作（例如查询数据库的结构或创建诸如表等的数据库对象），或通过执行 UPDATE、INSERT 或 DELETE 语句更改数据库中的数据
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>受影响的行数</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public int ExecuteNonQuery(string cmdText)
        {
            int effectCount = -1;
            try
            {
                effectCount = _AdoTemplate.ExecuteNonQuery(CommandType.Text, cmdText);
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;
            }
            finally { 
            
            }


            return effectCount;
        }

        /// <summary>
        /// 可以使用 ExecuteNonQuery 执行编录操作（例如查询数据库的结构或创建诸如表等的数据库对象），或通过执行 UPDATE、INSERT 或 DELETE 语句更改数据库中的数据
        /// </summary>
        /// <param name="commandType">指定如何解释命令字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <returns>受影响的行数</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public int ExecuteNonQuery(CommandType commandType, string cmdText)
        {
            int effectCount = -1;
            try
            {
                effectCount = _AdoTemplate.ExecuteNonQuery(commandType, cmdText);
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;
            }
            return effectCount;
        }

        /// <summary>
        /// 可以使用 ExecuteNonQuery 执行编录操作（例如查询数据库的结构或创建诸如表等的数据库对象），或通过执行 UPDATE、INSERT 或 DELETE 语句更改数据库中的数据
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="parameters">DbCommand 的参数</param>
        /// <returns>受影响的行数</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public int ExecuteNonQuery(string cmdText, IDbParameters parameters)
        {
            int effectCount = -1;
            try
            {
                effectCount = _AdoTemplate.ExecuteNonQuery(CommandType.Text, cmdText, parameters);
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
            }
            return effectCount;
        }

        /// <summary>
        /// 可以使用 ExecuteNonQuery 执行编录操作（例如查询数据库的结构或创建诸如表等的数据库对象），或通过执行 UPDATE、INSERT 或 DELETE 语句更改数据库中的数据
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="commandType">指定如何解释命令字符串</param>
        /// <param name="parameters">DbCommand 的参数</param>
        /// <returns>受影响的行数</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public int ExecuteNonQuery(CommandType commandType, string cmdText, IDbParameters parameters)
        {

            if (commandType == CommandType.StoredProcedure)
            {
                cmdText = cmdText.Split(' ')[0];
            }
            int effectCount = -1;
            try
            {
                effectCount = _AdoTemplate.ExecuteNonQuery(commandType, cmdText, parameters);
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
            }
            return effectCount;
        }

        /// <summary>
        /// 执行多句sql语句
        /// </summary>
        /// <param name="cmdText">sql语句数组</param>
        /// <returns>受影响的总行数</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public int ExecuteNonQuery(string[] cmdText)
        {
            int effectCount = 0;
            BeginTransaction();
            try
            {
                for (int i = 0; i < cmdText.Length; i++)
                {
                    effectCount += ExecuteNonQuery(cmdText[i]);
                }
                CommitTransaction();
            }
            catch (Exception e)
            {
                RollbackTransaction();
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace, "SQL语句:" + string.Join(",", cmdText));
            }
            return effectCount;
        }

        /// <summary>
        /// 执行多句sql语句
        /// </summary>
        /// <param name="cmdText">sql语句数组</param>
        /// <param name="isIntransaction">是否将这些sql语句作为事务</param>
        /// <returns>受影响的总行数</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public int ExecuteNonQuery(string[] cmdText, bool isIntransaction)
        {
            int effectCount = 0;
            if (isIntransaction)
                BeginTransaction();
            try
            {
                for (int i = 0; i < cmdText.Length; i++)
                {
                    effectCount += ExecuteNonQuery(cmdText[i]);
                }
                if (isIntransaction)
                    CommitTransaction();
            }
            catch (Exception e)
            {
                if (isIntransaction)
                    RollbackTransaction();
                dsExceptionHandler.HandleException(e);
                ReConnection(e);

                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace, "SQL语句:" + string.Join(",", cmdText));
            }
            return effectCount;
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。所有其他的列和行将被忽略。 
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>结果集中第一行的第一列</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public object ExecuteScalar(string cmdText)
        {
            try
            {
                return _AdoTemplate.ExecuteScalar(CommandType.Text, cmdText);
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。所有其他的列和行将被忽略。 
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="commandType">指定如何解释命令字符串</param>
        /// <returns>结果集中第一行的第一列</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public object ExecuteScalar(CommandType commandType, string cmdText)
        {
            try
            {
                return _AdoTemplate.ExecuteScalar(commandType, cmdText);

            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);

                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。所有其他的列和行将被忽略。 
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="commandType">指定如何解释命令字符串</param>
        /// <param name="parameters">DbCommand 的参数</param>
        /// <returns>结果集中第一行的第一列</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public object ExecuteScalar(CommandType commandType, string cmdText, IDbParameters parameters)
        {
            if (commandType == CommandType.StoredProcedure)
            {
                cmdText = cmdText.Split(' ')[0];
            }
            try
            {               
                return _AdoTemplate.ExecuteScalar(commandType, cmdText, parameters);
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);

                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行的第一列。所有其他的列和行将被忽略。 
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="parameters">DbCommand 的参数</param>
        /// <returns>结果集中第一行的第一列</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public object ExecuteScalar(string cmdText, IDbParameters parameters)
        {
            try
            {
                return _AdoTemplate.ExecuteScalar(CommandType.Text, cmdText, parameters);
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);

                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
            }
        }

        /// <summary>
        /// 创建DbCommandBuilder
        /// </summary>
        /// <returns></returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public DbCommandBuilder CreateCommandBuilder()
        {
            return IDbProvider.CreateCommandBuilder() as DbCommandBuilder;
        }
        /// <summary>
        /// 创建DbDataAdapter
        /// </summary>
        /// <returns></returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public DbDataAdapter CreateDbDataAdapter()
        {
            return IDbProvider.CreateDataAdapter() as DbDataAdapter;
        }
        /// <summary>
        /// 创建DbCommand
        /// </summary>
        /// <returns></returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public DbCommand CreateDbCommand()
        {
            return IDbProvider.CreateCommand() as DbCommand;
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataTable
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>结果集</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public DataTable QueryDataTable(string cmdText)
        {
            try
            {
                return _AdoTemplate.DataTableCreate(CommandType.Text, cmdText);
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
            }
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataTable
        /// </summary>
        /// <param name="sqlCommand">SqlCommand对象</param>
        /// <returns>结果集</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public DataTable QueryDataTableWithSqlDependency(SqlCommand sqlCommand)
        {
            try
            {

                //SqlDataAdapter sda = new SqlDataAdapter(sqlCommand);
                //DataTable dt = new DataTable();
                //if (string.IsNullOrEmpty(sqlCommand.Connection.ConnectionString))
                //{
                //    sqlCommand.Connection.ConnectionString = this.ConnnectString;
                //}
                //if (sqlCommand.Connection.State != ConnectionState.Open)
                //{
                //    sqlCommand.Connection.Open();
                //}
                return this.QueryDataTable(sqlCommand.CommandText);
                //sda.Fill(dt);
                //return dt;
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", sqlCommand.CommandText);
            }
            finally
            {
                sqlCommand.Connection.Close();
                sqlCommand.Connection.Dispose();
                sqlCommand.Dispose();
            }
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataTable
        /// </summary>
        /// <param name="commandType">指定如何解释命令字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <returns>结果集</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public DataTable QueryDataTable(CommandType commandType, string cmdText)
        {
            try
            {
                return _AdoTemplate.DataTableCreate(commandType, cmdText);
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
            }
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataTable
        /// </summary>
        /// <param name="commandType">指定如何解释命令字符串</param>
        /// <param name="cmdText">sql语句</param>
        /// <param name="parameters">DbCommand 的参数</param>
        /// <returns>结果集</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public DataTable QueryDataTable(CommandType commandType, string cmdText, IDbParameters parameters)
        {
            if (commandType == CommandType.StoredProcedure)
            {
                cmdText = cmdText.Split(' ')[0];
            }
            try
            {
                return _AdoTemplate.DataTableCreateWithParams(commandType, cmdText, parameters);
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
            }
        }

        /// <summary>
        /// 执行查询，并返回查询所返回的结果集中第一行
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>结果集中第一行</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public DataRow QueryDataRow(string cmdText)
        {
            try
            {
                DataTable dt = _AdoTemplate.DataTableCreate(CommandType.Text, cmdText);
                if (dt.Rows.Count == 0)
                    return null;
                else
                    return dt.Rows[0];
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
            }
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataSet
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="tblName">查询结果在DataSet里保存的表名</param>
        /// <returns>结果集</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public DataSet QueryDataSet(string cmdText, string tblName)
        {
            try
            {
                DataSet ds = new DataSet();
                if (tblName != string.Empty)
                    ds = _AdoTemplate.DataSetCreate(CommandType.Text, cmdText, new string[] { tblName });
                else
                    ds = _AdoTemplate.DataSetCreate(CommandType.Text, cmdText);
                return ds;
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
            }
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataSet
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="tableMappingName">查询结果在DataSet里保存的表名数组</param>
        /// <returns>结果集</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public DataSet QueryDataSet(string cmdText, string[] tableMappingName)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = _AdoTemplate.DataSetCreate(CommandType.Text, cmdText);
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    try
                    {

                        string name = tableMappingName[i];
                        ds.Tables[i].TableName = name;
                    }
                    catch { continue; }
                }
                return ds;
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
            }
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DbDataReader
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <returns>结果集</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public System.Data.Common.DbDataReader QueryDataReader(string cmdText)
        {
            try
            {
                
                DbCommand command = IDbProvider.CreateCommand() as DbCommand;
                command.Connection = IDbProvider.CreateConnection() as DbConnection;
                command.Connection.ConnectionString = this.ConnnectString;
                command.CommandText = cmdText;
                command.Connection.Open();
                return command.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception e)
            {
                dsExceptionHandler.HandleException(e);
                ReConnection(e);
                throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
            }
           
        }

        /// <summary>
        /// 执行查询，将结果集返回到一个DataTable
        /// </summary>
        /// <param name="cmdText">sql语句</param>
        /// <param name="startRecord">从其开始的从零开始的记录号</param>
        /// <param name="maxRecord">要检索的最大记录数</param>
        /// <returns>结果集</returns>
        [Obsolete("已过期，不建议使用，请使用DbAccess")]
        public DataTable QueryDataTable(string cmdText, int startRecord, int maxRecord)
        {
            return  _AdoTemplate.Execute(
                new DataAdapterExecutor(cmdText, startRecord, maxRecord,IDbProvider)
                ) as DataTable;
        }

        private class DataAdapterExecutor:IDataAdapterCallback
        {
            private string cmdText;
            private int startRecord;
            private int maxRecord;
            private IDbProvider dbProvider;
            public DataAdapterExecutor(string cmdText, int startRecord, int maxRecord,IDbProvider dbProvider) {
                this.cmdText = cmdText;
                this.startRecord = startRecord;
                this.maxRecord = maxRecord;
                this.dbProvider = dbProvider;
            }

            public object DoInDataAdapter(IDbDataAdapter dataAdapter)
            {
                try
                {
                    string sql = "";
                    if (dbProvider.DbMetadata.ProductName.ToLower().IndexOf("oracle") != -1)
                    {
                        sql = "SELECT * FROM (SELECT DST1.*, ROWNUM RN FROM ({0}) DST1 WHERE ROWNUM <= {2})WHERE RN >= {1}".FormatString(cmdText, startRecord, startRecord + maxRecord);
                    }
                    else if (dbProvider.DbMetadata.ProductName.ToLower().IndexOf("sqlserver") != -1)
                    {

                    }
                    DbDataAdapter sqlDA = dataAdapter as DbDataAdapter;
                    DbCommand commandToUse = (dataAdapter.SelectCommand as DbCommand);
                    commandToUse.CommandText = sql;
                    DataTable dt = new DataTable();
                    sqlDA.Fill(dt);
                    return dt;

                }
                catch(Exception ex)
                {
                   
                    throw new Exception("IDataAdapterCallback DoInDataAdapter function of internal errors", ex);
                   
                } 
                return null;
            }
        }

        ///// <summary>
        ///// 执行查询，将结果集返回到一个DataTable
        ///// </summary>
        ///// <param name="cmdText">sql语句</param>
        ///// <param name="sqlDA">DbDataAdapter对象</param>
        ///// <returns>结果集</returns>
        //public DataTable QueryDataTable(string cmdText, out DbDataAdapter sqlDA)
        //{
        //    if (this.DbConnection == null)
        //    {
        //        this.DbConnection = IDbProvider.CreateConnection() as DbConnection;
        //        this.DbConnection.ConnectionString = ConnnectString;
        //    }
        //    this.DbConnection.Open();
        //    try
        //    {
        //        DbDataAdapter _DbDataAdapter = IDbProvider.CreateDataAdapter() as DbDataAdapter;
        //        _DbDataAdapter.SelectCommand = IDbProvider.CreateCommand() as DbCommand;
        //        _DbDataAdapter.SelectCommand.CommandText = cmdText;
        //        _DbDataAdapter.SelectCommand.Connection = this.DbConnection;
        //        sqlDA = _DbDataAdapter;
        //        DataTable dt = new DataTable();

        //        sqlDA.Fill(dt);
        //        return dt;
        //    }
        //    catch (Exception e)
        //    {
        //        dsExceptionHandler.HandleException(e);

        //        sqlDA = null;
        //        throw e;// new Goodway.CommonModel.Exception.DbException(DbResult.SqlError, e.Message ?? "" + Environment.NewLine + e.StackTrace ?? "", cmdText);
        //    }
        //    finally
        //    {
        //        this.DbConnection.Dispose();
        //    }
        //}
     
        void PrepareIDbParametersByWhereString(out IDbParameters cmdParms, string whereString, params object[] parms)
        {
            cmdParms = new DbParameters(IDbProvider);
            System.Text.RegularExpressions.Regex r = null;
            if (IDbProvider.DbMetadata.ProductName.ToLower().IndexOf("oracle") != -1)
            {
                r = new System.Text.RegularExpressions.Regex(":([^ ]*)", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);
                System.Text.RegularExpressions.MatchCollection matches = r.Matches(whereString);
                IDataParameter param;
                for (int i = 0; i < matches.Count; i++)
                {
                    cmdParms.Add(":" + matches[i].Groups[1].Value, DbType.String);
                    param = cmdParms[":" + matches[i].Groups[1].Value];
                    param.Direction = ParameterDirection.Input;
                    if (parms == null || parms.Length < (i + 1)) throw new Exception("对不起， @" + matches[i].Groups[1].Value + " 参数的值未提供！");
                    param.Value = parms[i];
                    //cmdParms.AddParameter(param);
                }
            }
            else
            {
                r = new System.Text.RegularExpressions.Regex("@([^ ]*)", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Compiled);
                System.Text.RegularExpressions.MatchCollection matches = r.Matches(whereString);
                IDataParameter param;
                for (int i = 0; i < matches.Count; i++)
                {
                    cmdParms.Add("@" + matches[i].Groups[1].Value, DbType.String);
                    param = cmdParms["@" + matches[i].Groups[1].Value];
                    param.Direction = ParameterDirection.Input;
                    if (parms == null || parms.Length < (i + 1)) throw new Exception("对不起， @" + matches[i].Groups[1].Value + " 参数的值未提供！");
                    param.Value = parms[i];
                    //cmdParms.AddParameter(param);
                }

            }
            
           
        }

        void InitIDbParameters(out IDbParameters cmdParms, string sql, IList<KeyValuePair<string, object>> Parameters)
        {
            cmdParms = new DbParameters(IDbProvider);
           
            IDataParameter param;
            foreach (var parameter in Parameters)
            {
                cmdParms.Add( parameter.Key, DbType.String);
                param = cmdParms[parameter.Key];
                param.Direction = ParameterDirection.Input;
                param.Value = parameter.Value;
            }
        }
    }
}
