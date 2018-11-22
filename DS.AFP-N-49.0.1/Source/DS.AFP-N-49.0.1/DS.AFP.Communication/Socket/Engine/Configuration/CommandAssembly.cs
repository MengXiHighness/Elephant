﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using DS.AFP.Communication.Common;
using DS.AFP.Communication.SocketBase.Config;

namespace DS.AFP.Communication.SocketEngine.Configuration
{
    /// <summary>
    /// Command assembly configuration element
    /// </summary>
    public class CommandAssembly : ConfigurationElement, ICommandAssemblyConfig
    {
        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        /// <value>
        /// The assembly.
        /// </value>
        [ConfigurationProperty("assembly", IsRequired = false)]
        public string Assembly
        {
            get { return this["assembly"] as string; }
        }
    }

    /// <summary>
    /// Command assembly configuation collection
    /// </summary>
    [ConfigurationCollection(typeof(CommandAssembly))]
    public class CommandAssemblyCollection : GenericConfigurationElementCollectionBase<CommandAssembly, ICommandAssemblyConfig>
    {

    }
}
