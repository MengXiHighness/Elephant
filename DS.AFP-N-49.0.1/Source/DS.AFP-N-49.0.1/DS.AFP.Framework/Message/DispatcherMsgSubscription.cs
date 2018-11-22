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
using System.Windows.Threading;

namespace DS.AFP.Framework.Message
{
    ///<summary>
    /// 订阅UI线程执行
    ///</summary>
    /// <typeparam name="TMessage"></typeparam>
    public class DispatcherMsgSubscription<TMessage> : MsgSubscription<TMessage>
    {
        private readonly IDispatcherFacade dispatcher;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionReference">动作引用</param>
        /// <param name="filterReference">过滤器引用</param>
        /// <param name="dispatcher">提供用于管理线程工作项队列的服务</param>
        public DispatcherMsgSubscription(IDelegateReference actionReference, IDelegateReference filterReference, IDispatcherFacade dispatcher)
            : base(actionReference, filterReference)
        {
            this.dispatcher = dispatcher;
        }

        /// <summary>
        /// 在UI线程上回调动作action
        /// </summary>
        /// <param name="action">动作action</param>
        /// <param name="argument1">MessageSender</param>
        /// <param name="argument2">TMessage</param>
        public override void InvokeAction(Action<MessageSender,TMessage> action,MessageSender argument1,TMessage argument2)
        {
            dispatcher.BeginInvoke(action, argument1,argument2);
        }
    }
}