///<summary>
/// Copyright (c) 2011-2015 上海迪爱斯通信设备有限公司
/// 作  者：姜宁
/// 时  间：2013-11-13 11:13:09
/// 描  述：平台异常类
///</summary>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace DS.AFP.Common.Core
{
    /// <summary>
    /// AFP异常
    /// </summary>
    [Serializable]
    public partial class AFPException
    {
        protected AFPException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
           
        }        
    }
}
