using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Communication.SocketNameSpace;
using DS.AFP.Communication.SocketBase.Config;
using DS.AFP.Communication.SocketBase;
using DS.AFP.Communication.SocketEngine;
using DS.AFP.Communication.SocketBase.Logging;

namespace DS.AFP.Framework.SocketNameSpace
{
    /// <summary>
    /// Socket服务容器
    /// </summary>
    public class SocketServiceContainer
    {
        /// <summary>
        /// 初始化Socket服务
        /// <code>
        /// SocketServiceContainer.Create(CurrentAddinConfiguration);
        /// </code>
        /// </summary>
        /// <param name="currentAddinConfig"></param>
        public static void Create(System.Configuration.Configuration currentAddinConfig)
        {
            IConfigurationSource configSource = currentAddinConfig.GetSection(SocketSection.SectionName) as IConfigurationSource;
            if (configSource == null)
                return;
            IBootstrap bootstrap = BootstrapFactory.CreateBootstrap(configSource);
            bootstrap.Initialize(new Log4NetLogFactory() );
            StartResult result = bootstrap.Start();

        }
    }
}
