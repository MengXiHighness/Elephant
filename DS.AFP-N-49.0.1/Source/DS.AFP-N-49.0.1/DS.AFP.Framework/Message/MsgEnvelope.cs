using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.Message
{
    /// <summary>
    /// 消息信封类，是用来消息订阅和发布的类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Msgenvelope<T> : MessageEnvelope<T> where T : class 
    {
    }
}
