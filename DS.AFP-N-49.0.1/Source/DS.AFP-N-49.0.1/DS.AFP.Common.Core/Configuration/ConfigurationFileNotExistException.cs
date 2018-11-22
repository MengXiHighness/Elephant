using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core;

namespace DS.AFP.Common.Core.ConfigurationNameSpace
{
    /// <summary>
    /// 配置文件不存在异常
    /// <code>
    /// !(File.Exists(ConfigurationFilePath))
    ///     throw new ConfigurationFileNotExistException();
    /// </code>
    /// </summary>
    public class ConfigurationFileNotExistException:AFPException
    {
        public ConfigurationFileNotExistException() : base("ConfigurationFileNotExistException", "配置文件不存在") { }
    }
}
