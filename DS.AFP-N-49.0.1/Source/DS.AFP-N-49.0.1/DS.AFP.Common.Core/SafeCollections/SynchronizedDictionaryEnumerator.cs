
using System.Collections;
using Spring.Collections;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 线程安全的字典DictionaryEnumerator
    /// <code>
    /// return new SynchronizedDictionaryEnumerator(SyncRoot, _table.GetEnumerator());
    /// </code>
    /// </summary>
    internal class SynchronizedDictionaryEnumerator : SynchronizedEnumerator, IDictionaryEnumerator
    {
        public SynchronizedDictionaryEnumerator(object syncRoot, IDictionaryEnumerator enumerator)
            : base(syncRoot, enumerator)
        {
        }

        protected IDictionaryEnumerator Enumerator
        {
            get { return (IDictionaryEnumerator) enumerator; }
        }

        public object Key
        {
            get
            {
                lock (syncRoot)
                {
                    return Enumerator.Key;
                }
            }
        }

        public object Value
        {
            get
            {
                lock (syncRoot)
                {
                    return Enumerator.Value;
                }
            }
        }

        public DictionaryEntry Entry
        {
            get
            {
                lock (syncRoot)
                {
                    return Enumerator.Entry;
                }
            }
        }
    }
}