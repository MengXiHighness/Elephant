
using Spring.Objects.Factory.Xml;

namespace DS.AFP.Communication.WCF.Config
{
    /// <summary>
    /// Namespace parser for the WCF namespace.
    /// </summary>
    /// <author>Bruno Baia</author>
    [
        NamespaceParser(
            Namespace = "http://www.dscomm.com/ds/afp/wcf",
            SchemaLocationAssemblyHint = typeof(WcfNamespaceParser),
            SchemaLocation = "/DS.AFP.Communication.WCF.Config/DS.AFP.Communication.wcf.xsd")
    ]
    public sealed class WcfNamespaceParser : NamespaceParserSupport
    {
        private const string ChannelFactoryElement = "channelFactory";
        private const string ServiceHostElement = "serviceHost";
        private const string ServiceExporterElement = "serviceExporter";

        /// <summary>
        /// Register the “see cref="IObjectDefinitionParser"” for the WCF tags.
        /// </summary>
        public override void Init()
        {
            RegisterObjectDefinitionParser(ChannelFactoryElement, new ChannelFactoryObjectDefinitionParser());
        }
    }

   
    [
        NamespaceParser(
            Namespace = "http://www.dscomm.com/ds/afp/wcf",
            SchemaLocationAssemblyHint = typeof(WcfDuplexClientNamespaceParser),
            SchemaLocation = "/DS.AFP.Communication.WCF.Config/DS.AFP.Communication.wcf2.xsd")
    ]
    public sealed class WcfDuplexClientNamespaceParser : NamespaceParserSupport
    {
        private const string ChannelFactoryElement = "duplexChannelFactory";
        private const string ServiceHostElement = "serviceHost";
        private const string ServiceExporterElement = "serviceExporter";
        private const string ServiceCallBackElement = "callBack";

        /// <summary>
        /// Register the “see cref="IObjectDefinitionParser"” for the WCF tags.
        /// </summary>
        public override void Init()
        {
            RegisterObjectDefinitionParser(ChannelFactoryElement, new DuplexChannelFactoryObjectDefinitionParser());
        }
    }
}
