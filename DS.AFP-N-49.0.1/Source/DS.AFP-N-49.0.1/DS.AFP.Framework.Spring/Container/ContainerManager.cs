using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Common.Core.ConfigurationNameSpace;
using Spring.Context;
using Spring.Context.Support;

namespace DS.AFP.Framework.Container
{
    public class ContainerManager
    {
        public void Attach(IApplicationContext container,IDsConfigurationSection config)
        {
            XmlApplicationContext chaild = new XmlApplicationContext(container,"assembly://DS.WorkflowManager.Services/DS.WorkflowManager.Services.Objects/Objects.xml");
            
        }
    }
}
