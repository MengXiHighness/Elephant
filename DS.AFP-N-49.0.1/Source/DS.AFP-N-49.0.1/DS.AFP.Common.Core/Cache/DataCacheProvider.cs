using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;

namespace DS.AFP.Common.Core.Cache
{
    public class DataCacheProvider
    {
        private readonly ConcurrentDictionary<string, ObjectCache> _objectCacheLookup = new ConcurrentDictionary<string, ObjectCache>();

        public virtual ObjectCache CreateObjectCache(string objectCacheName = null, CacheItemPolicy cacheItemPolicy = null)
        {
            if (!this._objectCacheLookup.ContainsKey(objectCacheName))
            {
                this._objectCacheLookup[objectCacheName] = (new ObjectCacheElement(cacheItemPolicy) { }).CreateObjectCache();
            }
            return this._objectCacheLookup[objectCacheName];
        }

        public virtual IEnumerable<ObjectCache> GetObjectCaches(string name = null)
        {
            if (name != null)
            {
                ObjectCache iteratorVariable0;
                if (this._objectCacheLookup.TryGetValue(name, out iteratorVariable0))
                {
                    yield return iteratorVariable0;
                }
            }
            else
            {
                IEnumerator<ObjectCache> enumerator = this._objectCacheLookup.Values.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    ObjectCache current = enumerator.Current;
                    yield return current;
                }
            }
        }
    }
}
