using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DS.AFP.Communication.DS21
{
    public class GlobalParams
    {
    }

    /// <summary>
    /// 消息代码
    /// </summary>
    public enum DSMessageCode
    {
        [Description("接警台忙闲控制状态")]
        AgentStateChange = 0x3e90,
        [Description("警情分配服务器心跳询问")]
        CaseDistributeHeartAsk = 0xf320,
        [Description("警情分配服务器心跳询问应答")]
        CaseDistributeHeartAskAnswer = 0x3f20,
        [Description("CK报警")]
        CKAlarm = 0xe361,
        [Description("CK报警应答")]
        CKAlarmAnswer = 0x3e61,
        [Description("处警台发送接收警情确认消息")]
        ConfirmReceiveCaseMessage = 0x3f22,
        [Description("调度台心跳询问")]
        DispatcherHeartAsk = 0x3401,
        [Description("调度台心跳询问应答")]
        DispatcherHeartAskAnswer = 0x4301,
        [Description("调度台最小化")]
        DispatcherMin = 0x904,
        [Description("警情分配")]
        DistributeCaseMessage = 0x3f21,
        [Description("处警台接收分配警情")]
        DistributeCaseReceiveMessage = 0xf322,
        [Description("收到警情分配服务器分配处理确认消息")]
        DistributeDealConfirmMessage = 0xf324,
        [Description("一次定位")]
        GISLocate = 0x3901,
        [Description("一次定位返回辖区信息")]
        GISLocateAnswer = 0x9301,
        [Description("定位返回中队信息")]
        GISLocateDetachmentAnswer = 0x9330,
        [Description("定位失败")]
        GISLocateFailure = 0x9309,
        [Description("灯杆定位")]
        GISLocateLightPole = 0x3938,
        [Description("手机定位辖区信息")]
        GISLocateMobileAnswer = 0x9304,
        [Description("二次定位")]
        GISLocateSecond = 0x3902,
        [Description("二次定位辖区信息")]
        GISLocateSecondAnswer = 0x9302,
        [Description("定位返回周边单位信息")]
        GISLocateUnitAnswer = 0x9331,
        [Description("定位返回周边水源信息")]
        GISLocateWaterAnswer = 0x9332,
        [Description("定位点的坐标")]
        GISLocateXYAnswer = 0x9333,
        [Description("手机定位")]
        GISPhoneLocate = 0x3904,
        [Description("GPS报警")]
        GPSAlarm = 0xe381,
        [Description("GPS报警应答")]
        GPSAlarmAnswer = 0x3e81,
        [Description("向查号台发送手机定位查询消息")]
        MSG_CELLPHONE_WHERE = 0x3d02,
        [Description("查号台返回的手机定位查询结果")]
        MSG_CELLPHONE_WHERE_ANSWER = 0xd302,
        [Description("向查号台发送三字段查询消息")]
        MSG_CELLPHONE_WHOSE = 0x3d01,
        [Description("查号台返回的三字段查询结果")]
        MSG_CELLPHONE_WHOSE_ANSWER = 0xd301,
        [Description("Web查询申请虚实警转换消息")]
        QueryForCaseInfoConvert = 0xa401,
        [Description("Web查询申请联系报警人")]
        QueryForRequestContact = 0xa402,
        [Description("录音号查询应答")]
        RecordAnswer = 0x7301,
        [Description("录音号自动应答")]
        RecordAnswerAuto = 0x900,
        [Description("查询录音号")]
        RecordAsk = 0x3701,
        [Description("接警台直接听录音")]
        RecordOneKeyListen = 0x2801,
        [Description("录音台主动放音")]
        RecordPlay = 0x2802,
        [Description("保护录音")]
        RecordProtect = 0x3702,
        [Description("中继录音应答录音号")]
        RecordRelayAnswer = 0x7306,
        [Description("查询中继录音号")]
        RecordRelayAsk = 0x3706,
        [Description("处警台处警后释放消息")]
        ReleaseMessage = 0x3f25,
        [Description("短信报警")]
        SmsAlarm = 0xe304,
        [Description("短信接收应答")]
        SmsAlarmAnswer = 0x3e04,
        [Description("发送短信给用户")]
        SmsSend = 0x3e02,
        [Description("短信发送应答")]
        SmsSendAnswer = 0xe301,
        [Description("短信发送结果")]
        SmsSendResult = 0xe302,
        [Description("通用消息")]
        SystemGeneral = 0x8000,
        [Description("通用应用内部消息")]
        SystemInternal = 0x8a00,
        [Description("科所队消息")]
        SystemSquadron = 0xa301,
        [Description("科所队接警消息")]
        SystemSquadronAccept = 0xa307,
        [Description("科所队收到通知消息")]
        SystemSquadronInform = 0xa304,
        [Description("科所队消息通知确认")]
        SystemSquadronInformRequest = 0xa303,
        [Description("科所队申请退单消息")]
        SystemSquadronRequestBack = 0xa308,
        [Description("科所队更新车辆状态")]
        SystemSquadronUpdateVehicle = 0xa309,
        [Description("服务器状态备用消息")]
        SystemStatusBackup = 0xc002,
        [Description("服务器状态主用消息")]
        SystemStatusPrimary = 0xc001,
        [Description("时钟同步消息")]
        SystemTimeSync = 0x6a8,
        [Description("通用xml消息")]
        SystemXml = 0xaa00
    }
}
