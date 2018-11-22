using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.WPF.App
{
    public class ClientData
    {
        public UserStore UserStore { get; set; }

        public configure configure { get; set; } //其它配置

    }

    public class orgInfo
    {
        public string orgID { get; set; } //机构ID
        public string orgCode { get; set; } //机构代码
        public string orgName { get; set; } //机构名称
        public string orgType { get; set; } //机构类型
    }

    public class roleInfo
    {
        public string roleId { get; set; } //系统角色ID
        public string roleKey { get; set; } //系统角色Key
        public string roleCode { get; set; } //系统角色编码
        public string roleName { get; set; } //系统角色名称
        public string belongSystem { get; set; } //所属系统
        public string roleType { get; set; } //角色类型     0：系统角色；1：组织角色（业务单位）；2：业务角色
    }

    public class configure
    {
        public string logOutUri { get; set; } //退出系统地址
    }

    public class UserStore
    { //当前登录用户相关信息
        public string userID { get; set; } //登陆用户ID
        public string userName { get; set; } //登陆用户名称
        public string personID { get; set; } //登陆用户对应人员ID
        public string personName { get; set; } //登陆用户对应人员名称
        public orgInfo orgInfo { get; set; }  //行政单位信息
        public orgInfo businessOrgInfo { get; set; } //业务单位信息
        public string systemName { get; set; } //登陆用户账号
        public string userKey { get; set; } //登陆用户key
        public string currentSkin { get; set; } //当前皮肤
        public roleInfo[] roles { get; set; } //登陆用户对应系统角色集合
    }


}
