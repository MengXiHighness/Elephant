///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2014-2-13 11:13:09
/// 描  述：锁类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DS.AFP.Common.Core.Utility
{
   /// <summary>   
   /// 防止死锁（提供锁的超时处理。并提供自动解锁防止在使用中代码异常导致死锁资源）   
   /// <code>
   /// private readonly static object lock_obj = new object();
   /// using(Lock lk = new Lock(lock_obj,1000))
   /// {
   ///      //Code
   /// }
   /// </code>
   /// </summary>   
   public class Lock : IDisposable   
   {   
       /// <summary>   
       /// 默认超时设置   
       /// </summary>   
       public static int DefaultMillisecondsTimeout = 15000; // 15S   
        private object _obj;   
  
        /// <summary>   
        /// 构造    
        /// </summary>   
        /// <param name="obj">想要锁住的对象</param>   
        public Lock(object obj)   
        {   
            TryGet(obj, DefaultMillisecondsTimeout, true);   
        }   
  
        /// <summary>   
        /// 构造   
        /// </summary>   
        /// <param name="obj">想要锁住的对象</param>   
        /// <param name="millisecondsTimeout">超时设置</param>   
        public Lock(object obj, int millisecondsTimeout)   
        {   
            TryGet(obj, millisecondsTimeout, true);   
        }   
  
        /// <summary>   
        /// 构造   
        /// </summary>   
        /// <param name="obj">想要锁住的对象</param>   
        /// <param name="millisecondsTimeout">超时设置</param>   
        /// <param name="throwTimeoutException">是否抛出超时异常</param>   
        public Lock(object obj, int millisecondsTimeout, bool throwTimeoutException)   
        {   
            TryGet(obj, millisecondsTimeout, throwTimeoutException);   
        }   
  
        private void TryGet(object obj, int millisecondsTimeout, bool throwTimeoutException)   
        {   
            if (Monitor.TryEnter(obj, millisecondsTimeout))   
            {   
                _obj = obj;   
            }   
            else  
            {
                if (throwTimeoutException)
                {
                    throw new TimeoutException();
                }   
            }   
        }   
  
        /// <summary>   
        /// 销毁，并释放锁   
        /// </summary>   
        public void Dispose()   
        {   
            if (_obj != null)   
            {   
                Monitor.Exit(_obj);   
            }   
        }   
  
        /// <summary>   
        /// 获取在获取锁时是否发生等待超时   
        /// </summary>   
        public bool IsTimeout   
        {   
            get  
            {   
                return _obj == null;   
  
            }   
        }   
    }  

}
