﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.AFP.Communication.SocketBase.Config
{
    public partial class ServerConfig : IServerConfig
    {
        /// <summary>
        /// Gets or sets the default culture.
        /// </summary>
        /// <value>
        /// The default culture.
        /// </value>
        public string DefaultCulture { get; set; }
    }
}
