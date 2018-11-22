using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using Microsoft.Xrm.Client.Caching;
using Microsoft.Xrm.Client.Configuration;

namespace DS.AFP.Common.Core.Cache
{
    public static class ObjectCacheManager  : ObjectCache,  IDisposable
    {
        private static Lazy<DataCacheProvider> _provider = new Lazy<DataCacheProvider>(new Func<DataCacheProvider>(ObjectCacheManager.CreateProvider));
       
        private static DataCacheProvider CreateProvider()
        {
            return new DataCacheProvider();
        }

        public static ObjectCache GetInstance(string objectCacheName,CacheItemPolicy cacheItemPolicy=null)
        {
            if(cacheItemPolicy==null)
            {
                cacheItemPolicy = GetCacheItemPolicy();
            }
            return _provider.Value.CreateObjectCache(objectCacheName,cacheItemPolicy);
        }

        public static CacheItemPolicy GetCacheItemPolicy()
        {
            return  new CacheItemPolicy { AbsoluteExpiration =  (DateTimeOffset.UtcNow + new TimeSpan(0,30,0)), SlidingExpiration = new TimeSpan(0,30,0), Priority = CacheItemPriority.Default};
                    
        }
        
        //public static void Insert(this ObjectCache cache, string cacheKey, object value, CacheItemPolicy policy = null, string regionName = null)
        //{
        //    _provider.Value.Insert(cache, cacheKey, value, policy, regionName);
        //}

        public static T Get<T>(string objectCacheName, string cacheKey, Func<ObjectCache, T> load, Func<CacheItemPolicy> getPolicy = null, string regionName = null)
        {
            ObjectCache instance = GetInstance(objectCacheName);
            Func<CacheItemPolicy> func = getPolicy ?? GetCacheItemPolicy;
            return instance.Get<T>(cacheKey, load, func, regionName);
        }
       
        public static T Get<T>(this ObjectCache cache, string cacheKey, Func<ObjectCache, T> load, Func<CacheItemPolicy> getPolicy = null, string regionName = null)
        {
            Action<ObjectCache, T> insert = delegate (ObjectCache c, T obj) {
                CacheItemPolicy policy = (getPolicy != null) ? getPolicy() : GetCacheItemPolicy();
                c.Insert(cacheKey, obj, policy, regionName);
            };
            return cache.Get<T>(cacheKey, load, insert, null);
        }
    }

    public class T
    {
        
        Microsoft.Xrm.Client.Caching.ObjectCacheManager 
            ObjectCacheManager
    }
}
