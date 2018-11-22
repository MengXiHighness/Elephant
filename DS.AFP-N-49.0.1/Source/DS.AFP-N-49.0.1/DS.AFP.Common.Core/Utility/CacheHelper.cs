using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Caching;
using System.Text;

namespace DS.AFP.Common.Core.Utility
{
    public class CacheHelper
    {
        public T Get<T>(string key) where T:class ,new()
        {
           ObjectCache configurationCache = MemoryCache.Default;
           T temp = configurationCache[key] as T;
           return temp;            
        }

        public bool Register<T>(string key,string filePath, Func<byte[],T> adapter)
        {
            ObjectCache configurationCache = MemoryCache.Default;
            CacheItemPolicy policy = new CacheItemPolicy();
            HostFileChangeMonitor hfcm = new HostFileChangeMonitor(new List<string> { filePath });
            policy.ChangeMonitors.Add(hfcm);
            if (configurationCache.Contains(key))
                return false;
            if (File.Exists(filePath))
            {
                using (FileStream sFile = new FileStream(filePath, FileMode.Open))
                {
                    byte[] data = new byte[sFile.Length];
                    sFile.Read(data, 0, data.Length);
                    configurationCache.Set(key, adapter(data), policy);
                }
                return true;
            }
            else {
                throw new Exception("Register file does not exist!");
            };
        }

    }
}
