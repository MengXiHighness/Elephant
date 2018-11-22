using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// 坐席定义
    /// </summary>
    public class DeskInfo
    {
        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 台号
        /// </summary>
        public int TH { get; set; }

        /// <summary>
        /// 分机号
        /// </summary>
        public string FJH { get; set; }

        /// <summary>
        /// 应急电话号码
        /// </summary>
        public string YJDHHM { get; set; }

        /// <summary>
        /// ds21节点类型
        /// </summary>
        public int JDLX21 { get; set; }

        /// <summary>
        /// acd组
        /// </summary>
        public string DLACD { get; set; }

        /// <summary>
        /// 坐席类型
        /// </summary>
        public string ZXLX { get; set; }

        /// <summary>
        /// 所属单位
        /// </summary>
        public OrganizationInfo SSDW
        {
            get { return this.ssdw; }
            set { this.ssdw = value; }
        }
        private OrganizationInfo ssdw = new OrganizationInfo();
    }
}
