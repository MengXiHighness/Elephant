using Common.Logging;
using DS.AFP.Common.Core;
using Spring.Dao;
using Spring.Data;
using Spring.Data.Common;
using Spring.Data.Core;
using Spring.Data.Support;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DS.AFP.Data
{
    public class DbAccess : AdoTemplate, IAdoDbAccess
    {
        private ILog logger = LogManager.GetLogger("DbAccess"); 

        public Spring.Data.Common.IDbProvider IDbProvider
        {
            get {
                return base.DbProvider;
            }
        }

        public object ExecuteScalar(string cmdText)
        {
            try
            {
                return base.ExecuteScalar(CommandType.Text, cmdText);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public object ExecuteScalar(string cmdText, Spring.Data.Common.IDbParameters parameters)
        {
            try
            {
                return base.ExecuteScalar(CommandType.Text, cmdText, parameters);
            }
            catch (Exception e)
            {
               
                throw e;
            }
        }

        public System.Data.Common.DbDataReader QueryDataReader(string cmdText)
        {
            ConnectionTxPair connectionTxPairToUse = GetConnectionTxPair(DbProvider);
            IDbCommand command = null;
            System.Data.Common.DbDataReader reader = null;
            try
            {

                command = DbProvider.CreateCommand();
                command.Connection = connectionTxPairToUse.Connection;
                command.Transaction = connectionTxPairToUse.Transaction;

                ApplyCommandSettings(command);

                DbCommand commandToUse = command as DbCommand;
                if (commandToUse != null)
                {
                    command.CommandText = cmdText;
                    reader = base.CreateDataReaderWrapper(command.ExecuteReader()) as System.Data.Common.DbDataReader;
                    return reader;
                }else{
                    throw new InvalidDataAccessApiUsageException("IDbCommand 通过DbProvider创建的command不能转换为DbCommand");
                }  
            }
            catch (Exception e)
            {
                DisposeCommand(command);
                command = null;
                DisposeConnection(connectionTxPairToUse.Connection, DbProvider);
                connectionTxPairToUse.Connection = null;
               
                throw;
            }
            finally
            {
                DisposeCommand(command);
                DisposeConnection(connectionTxPairToUse.Connection, DbProvider);
                Spring.Data.Support.AdoUtils.CloseReader(reader);
            }
        }

        public System.Data.DataRow QueryDataRow(string cmdText)
        {
            try
            {
                DataTable dt = base.DataTableCreate(CommandType.Text, cmdText);
                if (dt.Rows.Count == 0)
                    return null;
                else
                    return dt.Rows[0];
            }
            catch (Exception e)
            {
              
                throw e;
            }
        }

        public System.Data.DataSet QueryDataSet(string cmdText, string tblName)
        {
            try
            {
                DataSet ds = new DataSet();
                if (tblName != string.Empty)
                    ds = base.DataSetCreate(CommandType.Text, cmdText, new string[] { tblName });
                else
                    ds = base.DataSetCreate(CommandType.Text, cmdText);
                return ds;
            }
            catch (Exception e)
            {
              
                throw e;
            }
        }

        public System.Data.DataSet QueryDataSet(string cmdText, string[] tableMappingName)
        {
            try
            {
                DataSet ds = new DataSet();
                ds = base.DataSetCreate(CommandType.Text, cmdText);
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
               
                throw e;
            }
        }

        public System.Data.DataTable QueryDataTable(System.Data.CommandType commandType, string cmdText)
        {
            try
            {
                return base.DataTableCreate(commandType, cmdText);
            }
            catch (Exception e)
            {
             
                throw e;
            }
        }

        public System.Data.DataTable QueryDataTable(string cmdText)
        {
            return this.QueryDataTable(CommandType.Text, cmdText);
        }

        public System.Data.DataTable QueryDataTable(string cmdText, int startRecord, int maxRecord)
        {
            return base.Execute(
                new DataAdapterExecutor(cmdText, startRecord, maxRecord, IDbProvider)
                ) as DataTable;
        }

        private class DataAdapterExecutor : IDataAdapterCallback
        {
            private string cmdText;
            private int startRecord;
            private int maxRecord;
            private IDbProvider dbProvider;
            public DataAdapterExecutor(string cmdText, int startRecord, int maxRecord, IDbProvider dbProvider)
            {
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
                catch (Exception ex)
                {

                    throw new Exception("IDataAdapterCallback DoInDataAdapter function of internal errors", ex);

                }
                return null;
            }
        }

        public System.Data.DataTable QueryDataTable(System.Data.CommandType commandType, string cmdText, Spring.Data.Common.IDbParameters parameters)
        {
            if (commandType == CommandType.StoredProcedure)
            {
                cmdText = cmdText.Split(' ')[0];
            }
            try
            {
                return base.DataTableCreateWithParams(commandType, cmdText, parameters);
            }
            catch (Exception e)
            {
              
                throw e;
            }
        }

        public int ExecuteNonQuery(string cmdText)
        {
            int effectCount = -1;
            try
            {
                effectCount = base.ExecuteNonQuery(CommandType.Text, cmdText);
            }
            catch (Exception e)
            {
               
                throw e;
            }
            finally
            {

            }


            return effectCount;
        }

        public int ExecuteNonQuery(string cmdText, Spring.Data.Common.IDbParameters parameters)
        {
            int effectCount = -1;
            try
            {
                effectCount = base.ExecuteNonQuery(CommandType.Text, cmdText, parameters);
            }
            catch (Exception e)
            {
                throw e;
            }
            return effectCount;
        }

        public int ExecuteNonQuery(string[] cmdText)
        {
            int effectCount = 0;
           
            try
            {
                for (int i = 0; i < cmdText.Length; i++)
                {
                    effectCount += ExecuteNonQuery(cmdText[i]);
                }
               
            }
            catch (Exception e)
            {
             
                throw e;
            }
            return effectCount;
        }
    }
}
