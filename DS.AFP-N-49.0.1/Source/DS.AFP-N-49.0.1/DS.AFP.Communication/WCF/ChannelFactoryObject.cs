using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Common.Logging;
using Spring.Objects.Factory;

namespace DS.AFP.Communication.WCF
{
    /// <summary>
    /// 通道工厂
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ChannelFactoryObject<T> : ChannelFactory<T>, IFactoryObject
    {
        #region Logging

        private static readonly ILog Log = LogManager.GetLogger(typeof(ChannelFactoryObject<>));

        #endregion

        private bool _isSingleton = true;

        public ChannelFactoryObject(ServiceEndpoint serviceEndpoint)
            : base(serviceEndpoint)
        {
        }

        /// <summary>
        /// 获取通道对象
        /// </summary>
        /// <returns></returns>
        public object GetObject()
        {
            return this.CreateChannel();
        }

        /// <summary>
        /// 获取通道类型
        /// </summary>
        public Type ObjectType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// 是否单工
        /// </summary>
        public bool IsSingleton
        {
            get { return this._isSingleton; }
            set { this._isSingleton = value; }
        }
    }
}

