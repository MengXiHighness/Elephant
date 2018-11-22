using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.WPF
{
    //系统角色
    public class Role
    {
        /// <summary>
        /// 系统角色ID
        /// </summary>
        public string roleId { get; set; }

        /// <summary>
        /// 系统角色Key
        /// </summary>
        public string roleKey { get; set; }

        /// <summary>
        /// 系统角色编码
        /// </summary>
        public string roleCode { get; set; }

        /// <summary>
        /// 系统角色名称
        /// </summary>
        public string roleName { get; set; }

        /// <summary>
        /// 所属系统
        /// </summary>
        public string belongSystem { get; set; }

        /// <summary>
        /// 角色类型
        /// "0":系统角色, "1":业务单位, "2":业务角色,"3":业务单位下系统角色
        /// </summary>
        public string roleType { get; set; }
    }
}
