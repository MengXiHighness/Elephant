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
    /// 插件异常扩展
    /// </summary>
    [Serializable]
    public partial class AddinException
    {
        protected AddinException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            this.AddinName = info.GetValue("插件", typeof(string)) as string;
        }

        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("插件", this.AddinName);
        }
    }
}
