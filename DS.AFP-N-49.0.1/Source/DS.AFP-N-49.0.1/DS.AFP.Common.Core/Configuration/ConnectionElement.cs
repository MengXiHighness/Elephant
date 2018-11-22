///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-8-13 11:13:09
/// 描  述：数据库配置类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using DS.AFP.Common.Core.Utility;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 数据库连接配置项（用于数据库连接配置）
    /// <code>
    /// ConnectionElement ConnectionElement = new DS.AFP.Common.Core.ConfigurationNameSpace.DsConfigurationManager().DsRootConfigurationSection.Connections[key];
    /// this.provider = ConnectionElement.Provider;
    /// </code>
    /// </summary>
    public class ConnectionElement : ConfigurationElement
    {

        [ConfigurationProperty("connectionstring")]
        public string ConnectionString
        {
            get
            {
                string connectString = (String) this["connectionstring"];
                if (connectString.IndexOf('$') != -1)
                {
                    string[] conArr = connectString.Split('$');

                    string user = conArr[1];
                    string pwd = conArr[3];
                    string user1 = AESHelper.AESDecrypt(user);
                    string pwd1 = AESHelper.AESDecrypt(pwd);
                    connectString = connectString.Replace("${0}$".FormatString(user), user1);
                    connectString = connectString.Replace("${0}$".FormatString(pwd), pwd1);
                    return connectString;

                }
                else
                {
                    return (String) this["connectionstring"];
                }
            }
            set
            { this["connectionstring"] = value; }
        }

        [ConfigurationProperty("id")]
        public string ID
        {
            get
            { return (String)this["id"]; }
            set
            { this["id"] = value; }
        }

        [ConfigurationProperty("provider", DefaultValue = "Oracle.DataAccess.Client")]
        public string Provider
        {
            get
            { return (String)this["provider"]; }
            set
            { this["provider"] = value; }
        }

        [ConfigurationProperty("metadata", DefaultValue = "res://*/")]
        public string Metadata
        {
            get
            { return (String)this["metadata"]; }
            set
            { this["metadata"] = value; }
        }
    }
}
