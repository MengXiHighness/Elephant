using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DS.AFP.Framework.WPF
{
    public class OrganizationInfo
    {
        /// <summary>
        /// 单位（组织机构）ID
        /// </summary>
        public string orgID { get; set; }

        /// <summary>
        /// 单位（组织机构）代码
        /// </summary>
        public string orgCode { get; set; }

        /// <summary>
        /// 单位（组织机构）名称
        /// </summary>
        public string orgName { get; set; }

        /// <summary>
        /// 单位（组织机构）类型
        /// </summary>
        public string orgType { get; set; }

        /// <summary>
        /// 所属行政区划编码
        /// </summary>
        public string regionCode { get; set; }

        /// <summary>
        /// 所属行政区划名称
        /// </summary>
        public string regionName { get; set; }
    }
}
