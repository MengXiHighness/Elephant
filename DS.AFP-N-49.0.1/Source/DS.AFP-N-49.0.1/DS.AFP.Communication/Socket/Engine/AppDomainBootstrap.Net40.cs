using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DS.AFP.Communication.SocketBase;
using DS.AFP.Communication.SocketBase.Config;

namespace DS.AFP.Communication.SocketEngine
{
    partial class AppDomainBootstrap : IDynamicBootstrap
    {
        bool IDynamicBootstrap.Add(IServerConfig config)
        {
            var dynamicBootstrap = m_InnerBootstrap as IDynamicBootstrap;
            return dynamicBootstrap.Add(config);
        }

        bool IDynamicBootstrap.AddAndStart(IServerConfig config)
        {
            var dynamicBootstrap = m_InnerBootstrap as IDynamicBootstrap;
            return dynamicBootstrap.AddAndStart(config);
        }

        void IDynamicBootstrap.Remove(string name)
        {
            var dynamicBootstrap = m_InnerBootstrap as IDynamicBootstrap;
            dynamicBootstrap.Remove(name);
        }
    }
}
