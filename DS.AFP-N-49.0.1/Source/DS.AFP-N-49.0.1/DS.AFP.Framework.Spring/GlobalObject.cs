using Spring.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.Spring
{
    public static class GlobalObject
    {
        public static IApplicationContext Container { get; set; }

        private static System.Collections.Concurrent.ConcurrentDictionary<string, IApplicationContext> addindata = new System.Collections.Concurrent.ConcurrentDictionary<string, IApplicationContext>();

        public static IApplicationContext Addin(string addinName)
        {
            return addindata[addinName];
        }

        internal   static void SetAddinContanier(string addinName, IApplicationContext container)
        {
            if (!addindata.ContainsKey(addinName))
            {
                addindata[addinName] = container;
            }
        }

       

    }
}
