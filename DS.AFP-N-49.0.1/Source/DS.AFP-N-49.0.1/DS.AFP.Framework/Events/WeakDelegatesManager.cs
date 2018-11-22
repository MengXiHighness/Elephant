//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
using System;
using System.Collections.Generic;
using System.Linq;

namespace DS.AFP.Framework.Events
{
    /// <summary>
    /// 委托管理器
    /// </summary>
    public class WeakDelegatesManager
    {
        private readonly List<DelegateReference> listeners = new List<DelegateReference>();

        /// <summary>
        /// 为管理队列添加一个委托
        /// </summary>
        /// <param name="listener">委托对象</param>
        public void AddListener(Delegate listener)
        {
            this.listeners.Add(new DelegateReference(listener, false));
        }

        /// <summary>
        /// 为管理队列移出一个委托
        /// </summary>
        /// <param name="listener">委托对象</param>
        public void RemoveListener(Delegate listener)
        {
            this.listeners.RemoveAll(reference =>
            {
                //Remove the listener, and prune collected listeners
                Delegate target = reference.Target;
                return listener.Equals(target) || target == null;
            });
        }

        /// <summary>
        /// 移出全部Target为null的委托后，动态调用余下全部委托
        /// <code>
        /// this.contentRegisteredListeners.Raise(this, e);//ViewRegisteredEventArgs e
        /// </code>
        /// </summary>
        /// <param name="args"></param>
        public void Raise(params object[] args)
        {
            this.listeners.RemoveAll(listener => listener.Target == null);

            foreach (Delegate handler in this.listeners.ToList().Select(listener => listener.Target).Where(listener => listener != null))
            {
                handler.DynamicInvoke(args);
            }
        }
    }
}
