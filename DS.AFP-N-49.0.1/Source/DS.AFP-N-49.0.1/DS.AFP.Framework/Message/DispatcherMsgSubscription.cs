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
    /// ����UI�߳�ִ��
    ///</summary>
    /// <typeparam name="TMessage"></typeparam>
    public class DispatcherMsgSubscription<TMessage> : MsgSubscription<TMessage>
    {
        private readonly IDispatcherFacade dispatcher;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionReference">��������</param>
        /// <param name="filterReference">����������</param>
        /// <param name="dispatcher">�ṩ���ڹ����̹߳�������еķ���</param>
        public DispatcherMsgSubscription(IDelegateReference actionReference, IDelegateReference filterReference, IDispatcherFacade dispatcher)
            : base(actionReference, filterReference)
        {
            this.dispatcher = dispatcher;
        }

        /// <summary>
        /// ��UI�߳��ϻص�����action
        /// </summary>
        /// <param name="action">����action</param>
        /// <param name="argument1">MessageSender</param>
        /// <param name="argument2">TMessage</param>
        public override void InvokeAction(Action<MessageSender,TMessage> action,MessageSender argument1,TMessage argument2)
        {
            dispatcher.BeginInvoke(action, argument1,argument2);
        }
    }
}