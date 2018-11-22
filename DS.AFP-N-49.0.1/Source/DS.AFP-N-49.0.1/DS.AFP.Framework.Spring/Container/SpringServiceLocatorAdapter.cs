using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using Spring.Context;
using Spring.Context.Support;

namespace DS.AFP.Framework
{
    /// <summary>
    /// Spring服务位置适配器
    /// </summary>
    public class SpringServiceLocatorAdapter : ServiceLocatorImplBase
    {
        private IApplicationContext _springContext;

        /// <summary>
        /// <code>
        /// new SpringServiceLocatorAdapter(this.Container)
        /// </code>
        /// </summary>
        /// <param name="context"></param>
        public SpringServiceLocatorAdapter(IApplicationContext context)
        {
            _springContext = context;
        }

        #region IContainerFacade Members

        protected override object DoGetInstance(Type serviceType, string key)
        {
            return TryResolve(serviceType, key);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            return TryResolve(serviceType);
        }

        private object TryResolve(Type type, string key)
        {
            if (_springContext == null)
                _springContext = ContextRegistry.GetContext();
            string[] possibleAliases;
            if (key != "" && key != null)
                possibleAliases = new string[] { key, type.Name, type.Namespace + "." + type.Name };
            else
                possibleAliases = new string[] { type.Name, type.Namespace + "." + type.Name };

            object foundObjectInstance = null;

            foreach (string possibleAlias in possibleAliases)
            {
                if (_springContext.ContainsObjectDefinition(possibleAlias))
                {
                    foundObjectInstance = _springContext.GetObject(possibleAlias);
                    break;
                }
            }
            if (foundObjectInstance == null)
            {

            }

            return foundObjectInstance;
        }

        private IEnumerable<object> TryResolve(Type type)
        {
            IList<object> ojbs = new List<object>();
            if (_springContext == null)
                _springContext = ContextRegistry.GetContext();

            string[] possibleAliases = new string[] { type.Name, type.Namespace + "." + type.Name };
            object foundObjectInstance = null;

            foreach (string possibleAlias in possibleAliases)
            {
                if (_springContext.ContainsObjectDefinition(possibleAlias))
                {
                    foundObjectInstance = _springContext.GetObject(possibleAlias);
                    ojbs.Add(foundObjectInstance);
                }
            }

            return ojbs.AsEnumerable();
        }

        #endregion
    }
}
