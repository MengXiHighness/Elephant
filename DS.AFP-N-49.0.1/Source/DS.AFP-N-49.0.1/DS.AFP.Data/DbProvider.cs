using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Data.Common;
using Spring.Reflection.Dynamic;
using System.Data;
using Spring.Expressions;
using Spring.Util;
using Spring.Context;
using Spring.Context.Support;
using Spring.Core.IO;
using DS.AFP.Common.Core.ConfigurationNameSpace;

namespace DS.AFP.Data
{
    /// <summary>
    /// 提供创建提供程序对数据源类的实现的实例
    /// </summary>
    public class DbProvider : IDbProvider
    {
        private IDbProvider dbprovider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">平台配置对象中的id</param>
        public DbProvider(string id)
        {
            DsConfigurationManager dm = new DsConfigurationManager();
            var c = dm.DsRootConfigurationSection.Connections.OfType<ConnectionElement>().Where(o => o.ID == id).FirstOrDefault();
            dbprovider = DbProviderFactory.GetDbProvider(c.Provider);
            dbprovider.ConnectionString = c.ConnectionString;
        }

        /// <summary>
        /// 创建IDbCommand
        /// </summary>
        /// <returns></returns>
        public IDbCommand CreateCommand()
        {
            return dbprovider.CreateCommand();
        }

        /// <summary>
        /// 创建CommandBuilder
        /// </summary>
        /// <returns></returns>
        public object CreateCommandBuilder()
        {
            return dbprovider.CreateCommandBuilder();
        }

        /// <summary>
        /// 创建Connection
        /// </summary>
        /// <returns></returns>
        public IDbConnection CreateConnection()
        {
            return dbprovider.CreateConnection();
        }

        /// <summary>
        /// 创建DataAdapter
        /// </summary>
        /// <returns></returns>
        public IDbDataAdapter CreateDataAdapter()
        {
            return dbprovider.CreateDataAdapter();
        }

        /// <summary>
        /// 创建Parameter
        /// </summary>
        /// <returns></returns>
        public IDbDataParameter CreateParameter()
        {
            return dbprovider.CreateParameter();
        }

        /// <summary>
        /// 创建ParameterName
        /// </summary>
        /// <returns></returns>
        public string CreateParameterName(string name)
        {
            return dbprovider.CreateParameterName(name);
        }

        /// <summary>
        /// 创建CreateParameterNameForCollection
        /// </summary>
        /// <returns></returns>
        public string CreateParameterNameForCollection(string name)
        {
            return dbprovider.CreateParameterNameForCollection(name);
        }

        public IDbMetadata DbMetadata
        {
            get { return dbprovider.DbMetadata; }
        }

        public string ConnectionString
        {
            get { return dbprovider.ConnectionString; }
            set { dbprovider.ConnectionString = value; }
        }

        /// <summary>
        /// 将Exception信息转换为string信息
        /// </summary>
        /// <param name="e">待转换的Exception信息</param>
        /// <returns>转换后的string信息</returns>
        public string ExtractError(Exception e)
        {
            return dbprovider.ExtractError(e);
        }

        /// <summary>
        /// 判断异常是否是数据库异常
        /// </summary>
        /// <param name="e">待判断的异常</param>
        /// <returns>是数据库异常为ture，否则为false</returns>
        public bool IsDataAccessException(Exception e)
        {
            return dbprovider.IsDataAccessException(e);
        }
    }

}
