using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;


namespace DS.AFP.Common.Core.Cache
{
    public sealed class ObjectCacheElement 
    {
        private const string _defaultObjectCacheTypeName = "System.Runtime.Caching.MemoryCache, System.Runtime.Caching, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a";
        public const string DefaultObjectCacheName = "DS.AFP.Cache";

        private CacheItemPolicy CacheItemPolicy
        {
            get;
            set;
        }
        public ObjectCacheElement(CacheItemPolicy cacheItemPolicy)
        {
            this.CacheItemPolicy = cacheItemPolicy;
        }

       

        public ObjectCache CreateObjectCache(string objectCacheName = null)
        {
            string name = objectCacheName ?? (DefaultObjectCacheName);
            return new MemoryCache(name);
        }

      
      
        

       
    }
}
