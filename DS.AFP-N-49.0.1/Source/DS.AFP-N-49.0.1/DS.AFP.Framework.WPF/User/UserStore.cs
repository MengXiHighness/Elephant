using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.WPF
{
    public class UserStore
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public string userID { get; set; }

        /// <summary>
        /// 用户账户（系统名称）
        /// </summary>
        public string systemName { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string userName { get; set; }

        /// <summary>
        /// 用户key（代码）
        /// </summary>
        public string userKey { get; set; }

        /// <summary>
        /// 用户对应人员ID
        /// </summary>
        public string personID { get; set; }

        /// <summary>
        /// 用户对应人员名称
        /// </summary>
        public string personName { get; set; }

        /// <summary>
        /// 用户对应人员工号
        /// </summary>
        public string employeeNumber { get; set; }

        /// <summary>
        /// 本地客户端IP列表
        /// </summary>
        public string clientIPs { get; set; }

        /// <summary>
        /// 当前皮肤
        /// </summary>
        public string currentSkin { get; set; }

        /// <summary>
        /// 用户拥有的系统角色集合
        /// </summary>
        public List<Role> roles
        {
            get { return this._roles; }
            set { this._roles = value; }
        }
        private List<Role> _roles = new List<Role>();

        /// <summary>
        /// 行政单位
        /// </summary>
        public OrganizationInfo orgInfo
        {
            get { return this._orginfo; }
            set { this._orginfo = value; }
        }
        private OrganizationInfo _orginfo;

        /// <summary>
        /// COC（合成指挥系统）的业务单位
        /// 该数据也包含在businessOrgs中：businessOrgs["COC"]
        /// </summary>
        public OrganizationInfo businessOrgInfo
        {
            get { return this._businessorginfo; }
            set { this._businessorginfo = value; }
        }
        private OrganizationInfo _businessorginfo;

        /// <summary>
        /// 各系统的业务单位集合
        /// </summary>
        public Dictionary<String, List<OrganizationInfo>> businessOrgs
        {
            get { return this._businessorgs; }
            set { this._businessorgs = value; }
        }
        private Dictionary<String, List<OrganizationInfo>> _businessorgs = new Dictionary<string, List<OrganizationInfo>>();


        /// <summary>
        /// 登录密码加密方式，目前只支持SHA1和SHA256
        /// </summary>
        public string encryption { get; set; }

        /// <summary>
        /// 加密后密码
        /// </summary>
        public string password { get; set; }


    }
}
