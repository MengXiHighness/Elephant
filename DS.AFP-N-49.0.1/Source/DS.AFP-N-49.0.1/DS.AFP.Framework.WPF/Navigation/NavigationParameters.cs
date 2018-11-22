using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
//jn
namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// 导航传参对象
    /// </summary>
    public class NavigationParameters:IEnumerable<KeyValuePair<string,object>>
    {
        private readonly List<KeyValuePair<string, object>> entries = new List<KeyValuePair<string, object>>();

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

    }
}
