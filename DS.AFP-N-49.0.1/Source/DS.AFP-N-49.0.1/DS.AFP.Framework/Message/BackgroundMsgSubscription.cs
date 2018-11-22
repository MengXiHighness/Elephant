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
using System.Threading;
using System;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// 后台线程订阅执行
    /// </summary>
    /// <typeparam name="TMessage"></typeparam>
    public class BackgroundMsgSubscription<TMessage> : MsgSubscription<TMessage>
    {
       
        public BackgroundMsgSubscription(IDelegateReference actionReference, IDelegateReference filterReference)
            : base(actionReference, filterReference)
        {
        }

        /// <summary>
        /// 用线程池执行action
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="argument1">MessageSender</param>
        /// <param name="argument2">TMessage</param>
        public override void InvokeAction(Action<MessageSender,TMessage> action,MessageSender argument1, TMessage argument2)
        {
            ThreadPool.QueueUserWorkItem( (o) => action(argument1,argument2) );
        }
    }
}