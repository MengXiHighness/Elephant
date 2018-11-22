using Spring.Data.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace DS.AFP.Data
{
    public interface IAdoDbAccess
    {
        /// <summary>
        /// 数据库引擎
        /// </summary>
        IDbProvider IDbProvider { get; }

       

        object ExecuteScalar(CommandType cmdType, string cmdText);
        object ExecuteScalar(CommandType cmdType, string cmdText, IDbParameters parameters);
        object ExecuteScalar(string cmdText);
        object ExecuteScalar(string cmdText, IDbParameters parameters);

        System.Data.Common.DbDataReader QueryDataReader(string cmdText);
        System.Data.DataRow QueryDataRow(string cmdText);
        System.Data.DataSet QueryDataSet(string cmdText, string tblName);
        System.Data.DataSet QueryDataSet(string cmdText, string[] tableMappingName);
        System.Data.DataTable QueryDataTable(CommandType commandType, string cmdText);
        System.Data.DataTable QueryDataTable(string cmdText);
        System.Data.DataTable QueryDataTable(string cmdText, int startRecord, int maxRecord);
        //System.Data.DataTable QueryDataTable(string cmdText, out DbDataAdapter sqlDA);
        System.Data.DataTable QueryDataTable(CommandType commandType, string cmdText, IDbParameters parameters);

        int ExecuteNonQuery(CommandType cmdType, string cmdText);
        int ExecuteNonQuery(CommandType cmdType, string cmdText, IDbParameters parameters);

        int ExecuteNonQuery(string cmdText);
        int ExecuteNonQuery(string cmdText, IDbParameters Parameters);
        int ExecuteNonQuery(string[] cmdText);

      
    }
}
