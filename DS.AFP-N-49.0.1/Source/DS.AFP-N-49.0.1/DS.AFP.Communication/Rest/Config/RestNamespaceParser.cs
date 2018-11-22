
using Spring.Objects.Factory.Xml;

namespace DS.AFP.Communication.Rest.Config
{
    /// <summary>
    /// Rest客户端配置
    /// </summary>
    /// <author>jiangning</author>
    [
        NamespaceParser(
            Namespace = "http://www.dscomm.com/rest",
            SchemaLocationAssemblyHint = typeof(RestNamespaceParser),
            SchemaLocation = "/DS.AFP.Communication.Rest.Config/DS.AFP.Communication.rest.xsd")
    ]
    public sealed class RestNamespaceParser : NamespaceParserSupport
    {
        private const string RestFactoryElement = "restFactory";
        private const string ServiceHostElement = "hostBaseUri";

        /// <summary>
        /// 注册Restclient实例
        /// </summary>
        public override void Init()
        {
            RegisterObjectDefinitionParser(RestFactoryElement, new RestFactoryObjectDefinitionParser());
        }
    }
    
}
