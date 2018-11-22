
///<summary>
/// Copyright (c) 2011-2015 �Ϻ��ϰ�˹ͨ���豸���޹�˾
/// ��  �ߣ�����
/// ʱ  �䣺2013-11-13 11:13:09
/// ��  �����̰߳�ȫ��Dictionary��
///</summary>
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// �̰߳�ȫ��Dictionary
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
	[Serializable]
	public class ThreadSafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		private object _syncRoot;

		private readonly IDictionary<TKey, TValue> dictionary;

		public ThreadSafeDictionary(IDictionary<TKey, TValue> dictionary)
		{
			this.dictionary = dictionary;
		}

		public object SyncRoot
		{
			get
			{
				if (_syncRoot == null)
				{
					Interlocked.CompareExchange(ref _syncRoot, new object(), null);
				}

				return _syncRoot;
			}
		}

		#region IDictionary<TKey,TValue> Members

		public bool ContainsKey(TKey key)
		{
			return dictionary.ContainsKey(key);
		}

		public void Add(TKey key, TValue value)
		{
			lock (SyncRoot)
			{
				dictionary.Add(key, value);
			}
		}

		public bool Remove(TKey key)
		{
			lock (SyncRoot)
			{
				return dictionary.Remove(key);
			}
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return dictionary.TryGetValue(key, out value);
		}

		public TValue this[TKey key]
		{
			get { return dictionary[key]; }
			set
			{
				lock (SyncRoot)
				{
					dictionary[key] = value;
				}
			}
		}

		public ICollection<TKey> Keys
		{
			get
			{
				lock (SyncRoot)
				{
					return dictionary.Keys;
				}
			}
		}

		public ICollection<TValue> Values
		{
			get
			{
				lock (SyncRoot)
				{
					return dictionary.Values;
				}
			}
		}

		#endregion

		#region ICollection<KeyValuePair<TKey,TValue>> Members

        /// <summary>
        /// �� IDictionary ���������һ���������ṩ�ļ���ֵ��Ԫ�ء�
        /// </summary>
        /// <param name="item"></param>
		public void Add(KeyValuePair<TKey, TValue> item)
		{
			lock (SyncRoot)
			{
				dictionary.Add(item);
			}
		}

        /// <summary>
        /// �� IDictionary �������Ƴ�����Ԫ�ء�
        /// </summary>
		public void Clear()
		{
			lock (SyncRoot)
			{
				dictionary.Clear();
			}
		}

        /// <summary>
        /// ȷ�� IDictionary �����Ƿ��������ָ������Ԫ�ء�
        /// </summary>
        /// <param name="item">Ҫ�� IDictionary �����ж�λ�Ķ���</param>
        /// <returns>��� IDictionary �������иü���Ԫ�أ���Ϊ true������Ϊ false</returns>
		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return dictionary.Contains(item);
		}

        /// <summary>
        /// ���ض��� Array ��������ʼ���� ICollection ��Ԫ�ظ��Ƶ�һ�� Array �С� 
        /// </summary>
        /// <param name="array">��Ϊ�� ICollection ���Ƶ�Ԫ�ص�Ŀ��λ�õ�һά Array��Array ������д��㿪ʼ������</param>
        /// <param name="arrayIndex">array �д��㿪ʼ���������ڴ˴���ʼ����</param>
		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			lock (SyncRoot)
			{
				dictionary.CopyTo(array, arrayIndex);
			}
		}

        /// <summary>
        /// �� IDictionary �������Ƴ�����ָ������Ԫ��
        /// </summary>
        /// <param name="item">Ҫ�Ƴ���Ԫ�صĶ���</param>
		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			lock (SyncRoot)
			{
				return dictionary.Remove(item);
			}
		}

		public int Count
		{
			get { return dictionary.Count; }
		}

		public bool IsReadOnly
		{
			get { return false; }
		}

		#endregion

		#region IEnumerable<KeyValuePair<TKey,TValue>> Members

		IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
		{
			// In a multi thread environment the dictionary can be changed by another 
			// thread than the actual that asking for a enumerator.
			// The Dictionary<K,V> create a new KeyValuePair for each iteration so....
			// we can create a snapshot for the thread preventing InvalidOperationException when
			// another thread change the version of the dictionary.
			lock (SyncRoot)
			{
				KeyValuePair<TKey, TValue>[] pairArray = new KeyValuePair<TKey, TValue>[dictionary.Count];
				dictionary.CopyTo(pairArray, 0);
				return Array.AsReadOnly(pairArray).GetEnumerator();
			}
		}

		#endregion

		#region IEnumerable Members

		public IEnumerator GetEnumerator()
		{
			return ((IEnumerable<KeyValuePair<TKey, TValue>>) this).GetEnumerator();
		}

		#endregion
	}
}