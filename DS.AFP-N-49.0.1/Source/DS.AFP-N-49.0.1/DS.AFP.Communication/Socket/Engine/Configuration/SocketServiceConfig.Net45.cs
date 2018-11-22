using DS.AFP.Communication.SocketBase.Config;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.SocketEngine.Configuration

{
    /// <summary>
    /// SocketServiceConfig, the part which is compatible with .Net 4.5 or higher
    /// </summary>
    public partial class SocketServiceConfig : IConfigurationSource
    {
        /// <summary>
        /// Gets/sets the default culture for all server instances.
        /// </summary>
        /// <value>
        /// The default culture.
        /// </value>
        [ConfigurationProperty("defaultCulture", IsRequired = false)]
        public string DefaultCulture
        {
            get
            {
                return (string)this["defaultCulture"];
            }
        }
    }
}
