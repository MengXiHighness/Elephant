using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using DS.AFP.Common.Core;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// 传参对象
    /// </summary>
    public class ViewParameters : IEnumerable<KeyValuePair<string, object>>
    {
        private readonly List<KeyValuePair<string, object>> entries = new List<KeyValuePair<string, object>>();

        public Func<ViewParameters, ViewParameters> FuncBack { get; set; }
        public object this[string key]
        {
            get
            {
                foreach (var kvp in this.entries)
                {
                    if (string.Compare(kvp.Key, key, StringComparison.Ordinal) == 0)
                    {
                        return kvp.Value;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 返回循环访问本对象的枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return this.entries.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void Add(string key, object value)
        {
            this.entries.Add(new KeyValuePair<string, object>(key, value));
        }

        Dictionary<string, string> o = new Dictionary<string, string>();
        

        public bool ContainsKey(string key)
        {
            foreach (var kvp in this.entries)
            {
                if (string.Compare(kvp.Key, key, StringComparison.Ordinal) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool Remove(string key)
        {
            KeyValuePair<string, object> item = new KeyValuePair<string,object>();
            foreach (var kvp in this.entries)
            {
                if (string.Compare(kvp.Key, key, StringComparison.Ordinal) == 0)
                {
                    item = kvp;
                }
            }
            if (!item.Key.IsNullOrEmpty())
            {
                this.entries.Remove(item);
                return true;
            }
            else
            {
                return false;
            }

        }

    }
}
