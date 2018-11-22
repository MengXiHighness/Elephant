using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.DS21
{
    /************************************
     * 这里就是程序现在使用到的一些枚举项，
     * 
     * **********************************/

    /// <summary>
    /// 消息发送模式
    /// </summary>
    public enum SendMode
    {
        [Description("点对点")]
        P2P = 0,
        [Description("广播(EXCLUDE)")]
        BROCAST_EXCLUDE = 1,
        [Description("广播(INCLUDE)")]
        BROCAST_INCLUDE = 2,
        [Description("未知")]
        UNKNOWN = 3
    }

    /// <summary>
    /// 节点类型
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// 通信服务器
        /// </summary>
        [Description("通信服务器")]
        SVR_COMM = 0x8000,										// 通信服务器
        /// <summary>
        /// 通信服务器Trace
        /// </summary>
        [Description("通信服务器Trace")]
        NODE_COMM_TRACE = 0x8F00,   		                    //通信服务器Trace

        #region 81**

        /// <summary>
        /// ACD/CTI Server
        /// </summary>
        [Description("ACD/CTI Server")]
        SVR_ACD = 0x8100,										// ACD/CTI Server	
        /// <summary>
        /// ACD/CTI Linker
        /// </summary>
        [Description("ACD/CTI Linker")]
        NODE_CTI_LINK = 0x8110,									// ACD/CTI Linker
        /// <summary>
        /// DS PBX
        /// </summary>
        [Description("DS PBX")]
        NODE_DS_PBX = 0x8111,									// DS PBX
        /// <summary>
        /// DS LINK_TRACE
        /// </summary>
        [Description("DS LINK_TRACE")]
        NODE_DS_LINK_TRACE = 0x8112,							// DS LINK_TRACE
        /// <summary>
        ///  Log Read-Write
        /// </summary>
        [Description("Log Read-Write")]
        NODE_CTI_LOGWR = 0x8120,								// Log Read-Write
        /// <summary>
        /// System Manager
        /// </summary>
        [Description("System Manager")]
        NODE_CTI_SYSMGR = 0x8130,								// System Manager
        /// <summary>
        /// Service Net
        /// </summary>
        [Description("Service Net")]
        NODE_CTI_SRVNET = 0x8140,								// Service Net
        //CTI Trace
        [Description("CTI Trace")]
        NODE_CTI_TRACE = 0x8150,								// CTI Trace
        /// <summary>
        /// CTI Control
        /// </summary>
        [Description("CTI Control")]
        NODE_CTI_CTRL = 0x8160,                                 //CTI Control
        /// <summary>
        /// CTI Conference
        /// </summary>
        [Description("CTI Conference")]
        NODE_CTI_CONF = 0x8170,                                 //CTI Conference
        /// <summary>
        /// CTI Agent Proxy
        /// </summary>
        [Description("CTI Agent Proxy")]
        NODE_CTI_PROXY = 0x8180,	                            //CTI Agent Proxy

        #endregion

        #region 82** AgentServer 相关节点 ，以下节点类型注册到AgentServer

        /// <summary>
        /// Agent Server	
        /// </summary>
        [Description("Agent Server")]
        SVR_AGENT = 0x8200,										// Agent Server	
        /// <summary>
        /// 中心接警坐席
        /// </summary>
        [Description("中心接警坐席")]
        NODE_RCPT = 0x8210,										// 中心接警坐席
        /// <summary>
        /// 中心调度坐席
        /// </summary>
        [Description("中心调度坐席")]
        NODE_DSP_KB = 0x8220,									// 中心调度坐席	
        /// <summary>
        /// 地图坐席
        /// </summary>
        [Description("地图坐席")]
        NODE_GIS = 0x8230,										// 地图坐席
        /// <summary>
        /// 综合处警坐席
        /// </summary>
        [Description("综合处警坐席")]
        NODE_POLICE_DISP = 0x8240,								// 综合处警坐席
        /// <summary>
        /// 交通处警坐席
        /// </summary>
        [Description("交通处警坐席")]
        NODE_TRAFFIC_DISP = 0x8250,								// 交通处警坐席
        /// <summary>
        /// 消防处警坐席
        /// </summary>
        [Description("消防处警坐席")]
        NODE_FIRE_DISP = 0x8260,								// 消防处警坐席
        /// <summary>
        /// 联动处警坐席
        /// </summary>
        [Description("联动处警坐席")]
        NODE_COALITION_DISP = 0x8270,							// 联动处警坐席
        /// <summary>
        /// 班长坐席
        /// </summary>
        [Description("班长坐席")]
        NODE_RCPT_CHIEF = 0x8280,								// 班长坐席
        /// <summary>
        /// 预案坐席
        /// </summary>
        [Description("预案坐席")]
        NODE_PLAN_DISP = 0x8290,								// 预案坐席
        /// <summary>
        /// 无线调度
        /// </summary>
        [Description("无线调度")]
        NODE_WIRELESS_DISP = 0x82A0,							// 无线调度
        /// <summary>
        /// LED控制台 大屏显示
        /// </summary>
        [Description("LED控制台 大屏显示")]
        NODE_LED_MONITOR = 0x82B0,								// LED控制台
        /// <summary>
        /// Agent跟踪程序
        /// </summary>
        [Description("Agent跟踪程序")]
        NODE_AGENT_TRACE = 0x82E0,								// Agent跟踪程序
        /// <summary>
        /// IVR客户端
        /// </summary>
        [Description("IVR客户端")]
        NODE_IVR_AGENT_AGENT = 0x82F0,							// IVR客户端

        #endregion


        /// <summary>
        /// DSInServer 0x8300
        /// </summary>
        [Description("DSInServer")]
        SVR_INSERVER = 0x8300,									// DSInServer
        /// <summary>
        /// 数据库代理服务器
        /// </summary>
        [Description("数据库代理服务器")]
        SVR_DB_PROXY = 0x8400,	                                // 数据库代理服务器
        /// <summary>
        /// 数据库代理TRACE
        /// </summary>
        [Description("数据库代理TRACE")]
        SVR_DB_PROXY_TRACE = 0x8410,	                        // 数据库代理TRACE

        /// <summary>
        /// 无线服务器
        /// </summary>
        [Description("无线服务器")]
        SVR_WIRELESS = 0x8500,									// 无线服务器	
        /// <summary>
        /// IVR服务器
        /// </summary>
        [Description("IVR服务器")]
        SVR_IVR = 0x8600,										// IVR服务器
        /// <summary>
        /// IVR客户端的CTI代理
        /// </summary>
        [Description("IVR客户端的CTI代理")]
        NODE_IVR_LINK_CTI = 0x8610,								// IVR客户端的CTI代理

        /// <summary>
        /// 监控服务器
        /// </summary>
        [Description("监控服务器")]
        SVR_MONITOR = 0x8700,									// 监控服务器
        /// <summary>
        /// 录音服务器
        /// </summary>
        [Description("录音服务器")]
        SVR_RECORD = 0x8800,									// 录音服务器
        /// <summary>
        /// 技防服务器
        /// </summary>
        [Description("技防服务器")]
        SVR_CK = 0x8900,										// 技防服务器
        /// <summary>
        /// 应用服务器
        /// </summary>
        [Description("应用服务器")]
        SVR_APPLICATION = 0x9000,                               //应用服务器

        /// <summary>
        /// 上级接入服务器
        /// </summary>
        [Description("上级接入服务器")]
        SVR_HIGHER_IN = 0x9100,		        //上级接入服务器
        /// <summary>
        /// 下级(二级)路由服务器
        /// </summary>
        [Description("下级(二级)路由服务器")]
        SVR_LOWER_ROUTE = 0x9200,		    //下级(二级)路由服务器
        /// <summary>
        /// 消防分配服务器
        /// </summary>
        [Description("消防分配服务器")]
        SVR_FIREALLOC = 0x9300,		        //消防分配服务器
        /// <summary>
        /// 消防(暂定)
        /// </summary>
        [Description("消防(暂定)")]
        SVR_FIREDATA = 0x9400,		        //消防(暂定)

        /// <summary>
        /// 综合管理服务器
        /// </summary>
        [Description("综合管理服务器")]
        SVR_INTEGRATED_MANAGER = 0x9500,						// 综合管理服务器
        /// <summary>
        /// 基础数据管理程序
        /// </summary>
        [Description("基础数据管理程序")]
        NODE_MANAGER_BASIC_DATA = 0x9510,						// 基础数据管理程序
        /// <summary>
        /// 查询统计程序
        /// </summary>
        [Description("查询统计程序")]
        NODE_MANAGER_STATISTIC = 0x9520,						// 查询统计程序
        /// <summary>
        /// 接处警考评程序
        /// </summary>
        [Description("接处警考评程序")]
        NODE_MANAGER_REMARK = 0x9530,							// 接处警考评程序

        /// <summary>
        /// 调度服务器
        /// </summary>
        [Description("调度服务器")]
        SVR_DISPATCH = 0x9600,			                        // 调度服务器

        /// <summary>
        /// 查号台
        /// </summary>
        [Description("查号台")]
        SVR_MOBILE = 0x9700,

        /// <summary>
        /// IPPHONE服务器
        /// </summary>
        [Description("IPPHONE服务器")]
        SVR_IPPHONE = 0x9800,			    // IPPHONE服务器
        /// <summary>
        /// WEBSERVER
        /// </summary>
        [Description("WEBSERVER")]
        SVR_WEB = 0x9900,		             // WEBSERVER
        /// <summary>
        /// GPS/CK报警接入服务器
        /// </summary>
        [Description("GPS/CK报警接入服务器")]
        SVR_GPSCK = 0x9A00,			        //GPS/CK报警接入服务器
        /// <summary>
        /// DS11接入服务器
        /// </summary>
        [Description("DS11接入服务器")]
        SVR_DS11_LINK = 0x9B00,			    //DS11接入服务器
        /// <summary>
        /// 系统控制服务器
        /// </summary>
        [Description("系统控制服务器")]
        SVR_SYSCTRL = 0x9C00,			    //系统控制服务器

        /// <summary>
        /// 传真服务器
        /// </summary>
        [Description("传真服务器")]
        SVR_FAX = 0x9D00,

        [Description("中心分配服务器(连接)")]
        SVR_DSRAS_CNCT = 0x8A00,								// 中心分配服务器(连接)
        [Description("中心分配服务器(本地1)")]
        SVR_DSRAS_COMM = 0x8A10,								// 中心分配服务器(本地1)
        [Description("中心分配服务器(本地2)")]
        SVR_DSRAS_COMM2 = 0x8A20,								// 中心分配服务器(本地2)
        [Description("中心分配服务器(本地3)")]
        SVR_DSRAS_COMM3 = 0x8A30,								// 中心分配服务器(本地3)
        [Description("中心分配服务器(本地4)")]
        SVR_DSRAS_COMM4 = 0x8A40,								// 中心分配服务器(本地4)
        [Description("接处警信息WEB网关")]
        SVR_BIZ_WEB_GATEWAY = 0x8A50,							// 接处警信息WEB网关
        [Description("二级接入服务器(连接）")]
        SVR_2RCPT_CNCT = 0x8A11,								// 二级接入服务器(连接）
        [Description("二级接入服务器(本地1)")]
        SVR_2RCPT_COMM = 0x4000,								// 二级接入服务器(本地1)
        [Description("二级接入服务器(本地2)")]
        SVR_2RCPT_COMM2 = 0x4100,								// 二级接入服务器(本地2)
        [Description("二级接入服务器(本地3)")]
        SVR_2RCPT_COMM3 = 0x4200,								// 二级接入服务器(本地3)
        [Description("二级接入服务器(本地4)")]
        SVR_2RCPT_COMM4 = 0x4300,								// 二级接入服务器(本地4)

        [Description("二级处警坐席")]
        NODE_2RCPT = 0x4400,									// 二级处警坐席
        [Description("二级地图坐席")]
        NODE_2GIS = 0x4500,										// 二级地图坐席
        [Description("二级班长坐席")]
        NODE_2RCPT_CHIEF = 0x4600,								// 二级班长坐席
        [Description("二级预案坐席")]
        NODE_2PLAN = 0x4700,									// 二级预案坐席
        [Description("二级交通坐席")]
        NODE_2TRAFFIC = 0x4800,									// 二级交通坐席
        [Description("二级综合处警坐席")]
        NODE_2RCPT_DISP = 0x4900,								// 二级综合处警坐席
        [Description("三级分配服务器")]
        SVR_2TRANSFER = 0x4A00,									// 三级分配服务器
        [Description("三级接警坐席")]
        NODE_3RCPT = 0x4A11,									// 三级接警坐席
        [Description("二级维护程序")]
        NODE_MAINTAIN = 0x4B00,									// 二级维护程序
        [Description("二级技防接入网关")]
        SVR_CK_GATEWAY = 0x4C00,								// 二级技防接入网关
        [Description("二级NC接入网关")]
        SVR_NC_GATEWAY = 0x4D00,								// 二级NC接入网关 
        [Description("二级GIS接入网关")]
        SVR_GIS_GATEWAY = 0x4E00,								// 二级GIS接入网关
        [Description("初始化节点类型")]
        NODE_UNKNOWN = 0x0000,									// 初始化节点类型
        [Description("本地通道对端")]
        NODE_LOCALCHANNEL_ENDPOINT = 0x0001,				// 本地通道对端

        /// <summary>
        /// 消息路由服务器
        /// </summary>
        [Description("消息路由服务器")]
        DSMSGROUTESVR = 0X9B00,

        /// <summary>
        /// 三台合一与移动终端接入服务器
        /// </summary>
        [Description("三台合一与移动终端接入服务器")]
        PoliceTerminalAccessServer = 0xBD00
    }


    /// <summary>
    /// 信息系统消息定义
    /// </summary>
    public enum InfoSysMessageID
    {
        /// <summary> 
        /// 通用消息 0x8000
        /// </summary>
        [Description("通用消息")]
        SystemGeneral = 0x8000,                            //通用消息
        /// <summary>
        /// 通用xml消息  0xaa00
        /// </summary>
        [Description("通用xml消息")]
        SystemXml = 0xaa00,                                //通用xml消息
        /// <summary>
        /// 通用应用内部消息 0x8a00
        /// </summary>
        [Description("通用应用内部消息")]
        SystemInternal = 0x8a00,                           //通用应用内部消息

        /// <summary>
        /// 服务器状态主用消息
        /// </summary>
        [Description("服务器状态主用消息")]
        SystemStatusPrimary = 0xc001,

        /// <summary>
        /// 服务器状态备用消息
        /// </summary>
        [Description("服务器状态备用消息")]
        SystemStatusBackup = 0xc002,

        /// <summary>
        /// 科所队消息 0xA301
        /// </summary>
        [Description("科所队消息")]
        SystemSquadron = 0xA301,                           //科所队消息


        /// <summary>
        /// 科所队消息通知确认 0xA303
        /// </summary>
        [Description("科所队消息通知确认")]
        SystemSquadronInformRequest = 0xA303,

        /// <summary>
        /// 科所队消息接警消息 0xA307
        /// </summary>
        [Description("科所队接警消息")]
        SystemSquadronAccept = 0xA307,

        /// <summary>
        /// 科所队收到通知消息 0xA304
        /// </summary>
        [Description("科所队收到通知消息")]
        SystemSquadronInform = 0xA304,                     // 科所队收到通知消息

        /// <summary>
        /// 科所队申请退单消息 0xA308
        /// </summary>
        [Description("科所队申请退单消息")]
        SystemSquadronRequestBack = 0xA308,                // 科所队申请退单消息

        [Description("科所队更新车辆状态")]
        SystemSquadronUpdateVehicle = 0xA309, //科所队更新车辆状态

        [Description("Web查询申请虚实警转换消息")]
        QueryForCaseInfoConvert = 0xA401,                            //查询申请虚实警转换消息

        [Description("Web查询申请联系报警人")]
        QueryForRequestContact = 0xA402, //查询申请联系报警人

        /// <summary>
        /// 时钟同步消息 0X6A8
        /// </summary>
        [Description("时钟同步消息")]
        SystemTimeSync = 0X6A8,								// 时钟同步消息

        /// <summary>
        /// 向地图发警情信息 0x3910
        /// </summary>
        [Description("警情信息")]
        GISIncident = 0x3910,
        /// <summary>
        /// 一次定位 0x3901
        /// </summary>
        [Description("一次定位")]
        GISLocate = 0x3901,
        /// <summary>
        /// 二次定位  0x3902
        /// </summary>
        [Description("二次定位")]
        GISLocateSecond = 0x3902,
        /// <summary>
        /// 手机定位  0x3904
        /// </summary>
        [Description("手机定位")]
        GISPhoneLocate = 0x3904,
        /// <summary>
        /// 灯杆定位  0x3938
        /// </summary>
        [Description("灯杆定位")]
        GISLocateLightPole = 0x3938,
        /// <summary>
        /// 历史警情定位  0x3910
        /// </summary>
        [Description("历史警情定位")]
        GISLocateHistory = 0x3910,
        /// <summary>
        /// 9301 一次定位成功地图台返回辖区信息
        /// </summary>
        [Description("一次定位返回辖区信息")]
        GISLocateAnswer = 0x9301,
        /// <summary>
        /// 9302 二次定位成功地图台返回辖区信息
        /// </summary>
        [Description("二次定位辖区信息")]
        GISLocateSecondAnswer = 0x9302,
        /// <summary>
        /// 9304 手机定位辖区信息
        /// </summary>
        [Description("手机定位辖区信息")]
        GISLocateMobileAnswer = 0x9304,
        /// <summary>
        /// 9330 定位返回中队信息
        /// </summary>
        [Description("定位返回中队信息")]
        GISLocateDetachmentAnswer = 0x9330,

        /// <summary>
        /// 9331 定位返回周边单位信息
        /// </summary>
        [Description("定位返回周边单位信息")]
        GISLocateUnitAnswer = 0x9331,

        /// <summary>
        /// 9332 定位返回周边水源信息
        /// </summary>
        [Description("定位返回周边水源信息")]
        GISLocateWaterAnswer = 0x9332,
        /// <summary>
        /// 9309 定位失败返回消息
        /// </summary>
        [Description("定位失败")]
        GISLocateFailure = 0x9309,
        /// <summary>
        /// 9333 定位成功后返回定位点的坐标
        /// </summary>
        [Description("定位点的坐标")]
        GISLocateXYAnswer = 0x9333,


        /// <summary>
        /// 查询录音号 0x3701
        /// </summary>
        [Description("查询录音号")]
        RecordAsk = 0x3701,

        /// <summary>
        /// 录音号查询应答 0x7301
        /// </summary>
        [Description("录音号查询应答")]
        RecordAnswer = 0x7301,

        /// <summary>
        /// 中继录音按台号查询录音号 0x3706
        /// </summary>
        [Description("查询中继录音号")]
        RecordRelayAsk = 0x3706,

        /// <summary>
        /// 中继录音应答录音号 0x7306
        /// </summary>
        [Description("中继录音应答录音号")]
        RecordRelayAnswer = 0x7306,
        /// <summary>
        /// 中继录音按台号查询录音号 0x2801
        /// </summary>
        [Description("接警台直接听录音")]
        RecordOneKeyListen = 0x2801,

        /// <summary>
        /// 中继录音应答录音号 0x2802
        /// </summary>
        [Description("录音台主动放音")]
        RecordPlay = 0x2802,
        /// <summary>
        /// 保护录音 0x3702
        /// </summary>
        [Description("保护录音")]
        RecordProtect = 0x3702,
        /// <summary>
        /// 录音号自动应答  0x0900
        /// </summary>
        [Description("录音号自动应答")]
        RecordAnswerAuto = 0x0900,

        /// <summary>
        /// CK报警消息  0xe361 
        /// </summary>
        [Description("CK报警")]
        CKAlarm = 0xe361,

        /// <summary>
        /// CK报警应答消息  0x3e61 
        /// </summary>
        [Description("CK报警应答")]
        CKAlarmAnswer = 0x3e61,

        /// <summary>
        /// GPS报警消息  0xe381 
        /// </summary>
        [Description("GPS报警")]
        GPSAlarm = 0xe381,

        /// <summary>
        /// GPS报警应答消息  0x3e81 
        /// </summary>
        [Description("GPS报警应答")]
        GPSAlarmAnswer = 0x3e81,
        /// <summary>
        /// 接警台忙闲状态控制消息  0x3e90 
        /// </summary>
        [Description("接警台忙闲控制状态")]
        AgentStateChange = 0x3e90,

        /// <summary>
        /// 调度台心跳询问  0x3401 
        /// </summary>
        [Description("调度台心跳询问")]
        DispatcherHeartAsk = 0x3401,
        /// <summary>
        /// 调度台心跳询问应答  0x4301 
        /// </summary>
        [Description("调度台心跳询问应答")]
        DispatcherHeartAskAnswer = 0x4301,

        /// <summary>
        /// 调度台最小化  0x0904 
        /// </summary>
        [Description("调度台最小化")]
        DispatcherMin = 0x0904,

        [Description("警情分配服务器心跳询问")]
        CaseDistributeHeartAsk = 0xf320,
        [Description("警情分配服务器心跳询问应答")]
        CaseDistributeHeartAskAnswer = 0x3f20,
        [Description("警情分配")]
        DistributeCaseMessage = 0x3f21,
        [Description("处警台接收分配警情")]
        DistributeCaseReceiveMessage = 0xf322,
        [Description("处警台发送接收警情确认消息")]
        ConfirmReceiveCaseMessage = 0x3f22,
        [Description("收到警情分配服务器分配处理确认消息")]
        DistributeDealConfirmMessage = 0xf324,
        [Description("处警台处警后释放消息")]
        ReleaseMessage = 0x3f25,
        //省厅12110相关消息

        #region 移动手机定位消息定义 
        /// <summary>
        /// 向查号台发送三字段查询消息	0x3d01
        /// </summary>
        [Description("向查号台发送三字段查询消息")]
        MSG_CELLPHONE_WHOSE = 0x3d01,
        /// <summary>
        /// 向查号台发送手机定位查询消息(省厅12110标准)
        /// </summary>
        [Description("向查号台发送手机定位查询消息(省厅12110标准)")]
        MSG_CELLPHONE_WHERE_N = 0x3d12,
        /// <summary>
        /// 向查号台发送手机定位查询消息	0x3d02
        /// </summary>
        [Description("向查号台发送手机定位查询消息")]
        MSG_CELLPHONE_WHERE = 0x3d02,
        /// <summary>
        /// 查号台返回的三字段查询结果与3d01消息对应	0x3d01
        /// </summary>
        [Description("查号台返回的三字段查询结果")]
        MSG_CELLPHONE_WHOSE_ANSWER = 0xd301,
        /// <summary>
        /// 查号台返回的手机定位查询结果 （与3d02消息对应）	0x3d02
        /// </summary>
        [Description("查号台返回的手机定位查询结果")]
        MSG_CELLPHONE_WHERE_ANSWER = 0xd302,
        #endregion

        #region 短信调度消息定义
        /// <summary>
        /// 短信报警消息  0xe304 
        /// </summary>
        [Description("短信报警")]
        SmsAlarm = 0xe304,

        /// <summary>
        /// 短信接收应答消息  0x3e04 
        /// </summary>
        [Description("短信接收应答")]
        SmsAlarmAnswer = 0x3e04,

        /// <summary>
        /// 发送短信给用户消息  0x3e02 
        /// </summary>
        [Description("发送短信给用户")]
        SmsSend = 0x3e02,

        /// <summary>
        /// 短信发送应答消息  0xe301 
        /// </summary>
        [Description("短信发送应答")]
        SmsSendAnswer = 0xe301,

        /// <summary>
        /// 短信发送结果  0xe302 
        /// </summary>
        [Description("短信发送结果")]
        SmsSendResult = 0xe302,

        [Description("发送短信给用户(新)")]
        SmsSendN = 0x3e52,
        [Description("错位接警接入应答")]
        SmsExchangeAlarmAnswer = 0x3e53,
        [Description("错位接警流转发起")]
        SmsSendExchangeAlarmMessage = 0x3e56,
        [Description("短信受理状态通知")]
        SmsAcceptStatusReport = 0x3801,
        [Description("短信受理结果通知")]
        SmsAcceptResultReport = 0x3802,
        [Description("错位警情流转发起通知")]
        SmsSendExchangeInfoMessage = 0x3804,
        [Description("短信接收应答")]
        SmsAlarmAnswerN = 0x3e54,
        #endregion

        SVR_2RCPT_TRACE = 0x8A1F,   // 二级分配Trace
        NODE_2TRACE = 0x4F00,		// 二级接入Trace


        MSG_IS_FIRE_INCALL = 0x8014,
        MSG_IS_FIRE_RECEIVE_INFO = 0x8013
    }
}
