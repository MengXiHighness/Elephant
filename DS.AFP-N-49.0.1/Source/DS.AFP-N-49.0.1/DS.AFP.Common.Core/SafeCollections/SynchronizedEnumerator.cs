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
    /// �̰߳�ȫ��dEnumerator
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
        /// ��ö�����ƽ������ϵ���һ��Ԫ��
        /// </summary>
        /// <returns>���ö�����ɹ����ƽ�����һ��Ԫ�أ���Ϊ true�����ö����Խ�����ϵĽ�β����Ϊ false��</returns>
        public bool MoveNext()
        {
            lock (syncRoot)
            {
                return enumerator.MoveNext();
            }
        }

        /// <summary>
        /// ��ö��������Ϊ���ʼλ�ã���λ��λ�ڼ����е�һ��Ԫ��֮ǰ��
        /// </summary>
        public void Reset()
        {
            lock (syncRoot)
            {
                enumerator.Reset();
            }
        }

        /// <summary>
        /// ��ȡ�����еĵ�ǰԪ��
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