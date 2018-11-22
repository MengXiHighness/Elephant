#region License

/*
 * Copyright 2002-2004 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

#endregion

using System.Collections;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// 线程安全的dEnumerator
    /// <code>
    /// return new SynchronizedEnumerator(SyncRoot, ((IEnumerable)_table).GetEnumerator());
    /// </code>
    /// </summary>
    internal class SynchronizedEnumerator : IEnumerator
    {
        protected object syncRoot;
        protected IEnumerator enumerator;

        public SynchronizedEnumerator(object syncRoot, IEnumerator enumerator)
        {
            this.syncRoot = syncRoot;
            this.enumerator = enumerator;
        }

        /// <summary>
        /// 将枚举数推进到集合的下一个元素
        /// </summary>
        /// <returns>如果枚举数成功地推进到下一个元素，则为 true；如果枚举数越过集合的结尾，则为 false。</returns>
        public bool MoveNext()
        {
            lock (syncRoot)
            {
                return enumerator.MoveNext();
            }
        }

        /// <summary>
        /// 将枚举数设置为其初始位置，该位置位于集合中第一个元素之前。
        /// </summary>
        public void Reset()
        {
            lock (syncRoot)
            {
                enumerator.Reset();
            }
        }

        /// <summary>
        /// 获取集合中的当前元素
        /// </summary>
        public object Current
        {
            get
            {
                lock (syncRoot)
                {
                    return enumerator.Current;
                }
            }
        }
    }
}