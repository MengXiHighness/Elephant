///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-7-10 11:13:09
/// 描  述：通信配置元素类
///</summary>
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;

namespace DS.AFP.Common.Core.Utility
{
    /// <summary>
    /// 配置文件工具类（提供平台配置的路径的获取，插件配置文件的获取）
    /// </summary>
    public class ConfigurationFileHelper
    {
        /// <summary>
        /// 获取平台配置路径
        /// <code>
        /// ConfigurationFileHelper.GetRootConfigurationFilePath()
        /// </code>
        /// </summary>
        /// <returns></returns>
        public static string GetRootConfigurationFilePath()
        {
            IDsEnvironment et = new DsEnvironment();
            switch (et.EnvironmentType)
            {
                case EnvironmentType.WPF:
                case EnvironmentType.WindowsService:
                    {
                        return Path.Combine(PathHelper.GetRootPath(), et.HostName+".exe.config");
                    }
                case EnvironmentType.WebMvc:
                case EnvironmentType.WebForm:
                    {
                        return Path.Combine(PathHelper.GetRootPath(), "web.config");
                    } 
                default:
                    throw new ArgumentNullException("DsEnvironment EnvironmentType attribute of the object does not exist");
            }
        }

        /// <summary>
        /// 插件配置路径，需要带后缀
        /// </summary>
        /// <param name="relativePath">相对路径</param>
        /// <returns></returns>
        public static string GetAddinConfigurationFilePath(string relativePath)
        {
            return PathHelper.GetFullPath(relativePath) + (relativePath.ToLower().Contains(".config") ? "" : ".config");
        }
    }

  
}
