/**
供JS调用浏览器接口
*/
interface Iafpbrowser {
    /**
    *启动本地应用程序
    */
    startProcess(processPath: string, parameter: string): void;

    /**
    *最小化浏览器
    */
    minimize(): void;

    /**
    *最大化浏览器
    */
    maximize(): void;

    /**
    *声光提示
    */
    alertCount(count: number,type: string): void;

    /**
    发送消息 js -> exe -> js
    */
    sendMessageToBrowser(message: string): boolean;

    /**
    发送消息js -> exe
    */
    sendMessageToClient(message: string): boolean;

    /**
    发送消息websocket client ->websocket client
    */
    sendWebMessage(message: string): boolean;

    /**
    发送基本信息到浏览器
    */
    setClientData(clientData: string): void;

    /**
    打开新窗口
    */
    openWindow(uri: string, screenNumber: number): void;

    /**
    退出系统
    */
    exit(): void;

    /**
    获取当前窗体句柄
    */
    getWinHandleID(): string;

    call(callInfo: callInfo): boolean;

    sendsms(messageInfo:string): boolean;
}


/**
浏览器提示的接口
*/
interface Iafpplayerbrowser {
    /**
    启动播放器
    */
    startPlayer(path: string, parameters: string): string;

    /**
    退出播放器
    */
    signOut(playerID: string): boolean;

    /**
    发送消息到播放器
    */
    sendMessage(playerID: string, parameters: string): boolean;



}


/**
页面JS配置接口
*/
interface Iconfig {
    /**
    播放器启动路径
    */
    PlayerPath: string;

    /**
    播放器启动成功时的回调
    */
    StartupComplete(playerID: string): void;

    /**
    回到消息的回调
    */
    ReceiveMessage(playerID: string, message: string): void;

    /**
    播放器退出时的回调
    */
    SignOut(playerID: string): boolean;

    /**
    出现错误时的回调
    */
    Error(playerID: string, message: string): void;


}



//import { Iconfig } from './ds.Iconfig';

/**
页面JS调用接口
*/
interface Iplayerbrowser {

    config: Iconfig;

    /**
    启动播放器
    */
    StartPlayer(parameters: string): boolean;

    /**
    退出播放器
    */
    SignOut(): boolean;

    /**
    向播放器传递参数
    */
    SetPlayerParameters(parameters: string): boolean;


}

interface IPhone {
    /*
    单呼、组呼
    */
    call(callInfo: callInfo): boolean;

    /*
    短信
    */
    sendsms(messageInfo: messageInfo): boolean;

}

interface callInfo {
    "businessid": string,
    "systemcode": string,
    "userid": string,
    "actiontype":string,
    "phoneMembers": string[]    
}

interface messageInfo {
    "businessid": string,
    "phoneMembers": string[],
    "content": string,
    "systemcode": string,
    "userid": string
}


//clientData
interface orgInfo {
    "orgID": string, //机构ID
    "orgCode": string, //机构代码
    "orgName": string, //机构名称
    "orgType": string //机构类型
}

interface role {
    "roleId": string, //系统角色ID
    "roleKey": string, //系统角色Key
    "roleCode": string, //系统角色编码
    "roleName": string, //系统角色名称
    "belongSystem": string, //所属系统
    "roleType": string //角色类型     0：系统角色；1：组织角色（业务单位）；2：业务角色
}

class clientData {
    public UserStore: { //当前登录用户相关信息
        "userID": string, //登陆用户ID
        "userName": string, //登陆用户名称
        "personID": string, //登陆用户对应人员ID
        "personName": string, //登陆用户对应人员名称
        "orgInfo": orgInfo,  //行政单位信息
        "businessOrgInfo": orgInfo, //业务单位信息
        "systemName": string, //登陆用户账号
        "userKey": string, //登陆用户key
        "currentSkin": string, //当前皮肤
        "roles": role[] //登陆用户对应系统角色集合
    }
    public configure: { //其它配置
        "logOutUri": string //退出系统地址
    }
}
//clientData

interface RegisterInfo {
    /**
    回到消息的回调
    */
    ReceiveMessage(message: { head: { code: string, from: string, tos: Array<string> }, body: string }): void;
}


namespace ds.afp {
    export class common {
        /**
 * 返回系统时间
 */
        getNowFormatDate(): string {
            let date: Date = new Date();
            let seperator1: string = "-";
            let seperator2: string = ":";
            let month: number = date.getMonth() + 1;
            let strDate: number = date.getDate();

            let temp_month: string = month.toString();
            if (month >= 1 && month <= 9) {
                temp_month = "0" + month.toString();
            }
            let temp_strDate: string = strDate.toString();
            if (strDate >= 0 && strDate <= 9) {
                temp_strDate = "0" + strDate.toString();
            }

            var currentdate: string = date.getFullYear().toString() + seperator1 + temp_month + seperator1 + temp_strDate
                + " " + date.getHours().toString() + seperator2 + date.getMinutes().toString()
                + seperator2 + date.getSeconds().toString();
            return currentdate;
        }

        /**
         * 记录日志
         * @param logcontent
         */
        info(logcontent: string): void {
            if (console) {
                try {
                    let datetime: string = this.getNowFormatDate();
                    console.log("log info:" + datetime + "  " + logcontent)
                } catch (ex) {
                }
            }
        }

        /**
         * 记录日志
         * @param logcontent
         */
        error(logcontent: string): void {
            if (console) {
                try {
                    let datetime: string = this.getNowFormatDate();
                    console.log("log error:" + datetime + "  " + logcontent)
                } catch (ex) {
                }
            }
        }

        /**
         * 返回一个guid
         */
        public static getGuid(): string {
            return new GUID().toString();
        }
    };

    export class GUID {
        private str: string;

        constructor(str?: string) {
            this.str = str || GUID.getNewGUIDString();
        }

        toString() {
            return this.str;
        }

        private static getNewGUIDString() {
            // your favourite guid generation function could go here
            // ex: http://stackoverflow.com/a/8809472/188246
            let d = new Date().getTime();
            if (window.performance && typeof window.performance.now === "function") {
                d += performance.now(); //use high-precision timer if available
            }
            return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, (c) => {
                let r = (d + Math.random() * 16) % 16 | 0;
                d = Math.floor(d / 16);
                return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
            });
        }
    }
}



namespace ds.afp {
    export class browser extends ds.afp.common {
        public Registers: Array<RegisterInfo> = [];
        public LogoutFun: any = null;

        constructor() {
            //this.info("ds.afp.browser.common loading...")
            super();
        }


        /**
        登出回调
        */
        LogoutRegister(info: { logoutFun: any }): boolean {
            console.log("LogoutRegister")
            this.LogoutFun = info.logoutFun;
            //console.log(this.LogoutFun)
            return true;
        }


        /**
         * 注册
         * @param info
         */
        MessageRegister(info: RegisterInfo): boolean {
            this.Registers.push(info);
            return false;
        }

        /**
         * 启动本地程序 js -> exe
         * @param processPath
         * @param parameter
         */
        StartProcess(processPath: string, parameter: string = ''): void {
            this.info("StartProcess:start processPath=" + processPath + ";parameter=" + parameter.toString());
            try {
                let afpbrowser: Iafpbrowser = (<any>window).afpbrowser;
                afpbrowser.startProcess(processPath, parameter);
            } catch (ex) {
                this.error("StartProcess error:" + ex.name)
            }
            this.info("StartProcess:end");
        }

        /**
         * 最小化浏览器 js -> exe
         */
        Minimize(): void {
            this.info("Minimize:start");
            try {
                let afpbrowser: Iafpbrowser = (<any>window).afpbrowser;
                afpbrowser.minimize();
            } catch (ex) {
                this.error("Minimize error:" + ex.name)
            }
            this.info("Minimize:end");
        }

        /**
         * 最大化窗口 js -> exe
         */
        Maximize(): void {
            this.info("Maximize:start");
            try {
                let afpbrowser: Iafpbrowser = (<any>window).afpbrowser;
                afpbrowser.maximize();
            } catch (ex) {
                this.error("Maximize error:" + ex.name)
            }
            this.info("Maximize:end");
        }

        /**
         * 显示提示 js -> exe
         */
        AlertCount(count: number,type: string): void {
            this.info("ShowPrompt:start");
            try {
                let afpbrowser: Iafpbrowser = (<any>window).afpbrowser;
                afpbrowser.alertCount(count, type);
            } catch (ex) {
                this.error("ShowPrompt error:" + ex.name)
            }
            this.info("ShowPrompt:end");
        }

        /**
         * 发送消息 js -> 其他浏览器窗口 -> 广播到所有页面
         * @param message
         */
        SendMessageToBrowser(message: { head: { code: string, from: string, tos: Array<string> }, body: string }): boolean {
            let msg = JSON.stringify(message);
            this.info("SendMessageToBrowser:start");
            try {
                let afpbrowser: Iafpbrowser = (<any>window).afpbrowser;
                afpbrowser.sendMessageToBrowser(msg);
                return true;
            } catch (ex) {
                this.error("SendMessageToBrowser error:" + ex.name)
            }
            this.info("SendMessageToBrowser:end");
            return false;
        }
         /**
         * 发送消息 js -> exe -> 广播到所有页面js
         * @param message
         */
        SendMessageToClient(message: { head: { code: string, from: string, tos: Array<string> }, body: string }): boolean {
            let msg = JSON.stringify(message);
            this.info("SendMessageToClient:start");
            try {
                let afpbrowser: Iafpbrowser = (<any>window).afpbrowser;
                afpbrowser.sendMessageToClient(msg);
                return true;
            } catch (ex) {
                this.error("SendMessageToClient error:" + ex.name)
            }
            this.info("SendMessageToClient:end");
            return false;
        }
         /**
         * 发送websocket消息
         * @param message
         */
        SendWebMessage(message: { head: { code: string, from: string, tos: Array<string> }, body: string }): boolean {
            let msg = JSON.stringify(message);
            this.info("SendWebMessage:start");
            try {
                let afpbrowser: Iafpbrowser = (<any>window).afpbrowser;
                afpbrowser.sendWebMessage(msg);
                return true;
            } catch (ex) {
                this.error("SendWebMessage error:" + ex.name)
            }
            this.info("SendWebMessage:end");
            return false;
        }

        /**
         * 生成基础配置
         * @param clientdata
         */
        SetClientData(clientdata: clientData): boolean {
            let msg = JSON.stringify(clientdata);
            this.info("SetClientData:start");
            try {
                let afpbrowser: Iafpbrowser = (<any>window).afpbrowser;
                afpbrowser.setClientData(msg);
                return true;
            } catch (ex) {
                this.error("SetClientData error:" + ex.name)
            }
            this.info("SetClientData:end");
            return false;
        }

        OpenWindow(uri: string, screenNumber: number): boolean {
            this.info("OpenWindow:start");
            try {
                let afpbrowser: Iafpbrowser = (<any>window).afpbrowser;
                afpbrowser.openWindow(uri, screenNumber);
                return true;
            } catch (ex) {
                this.error("OpenWindow error:" + ex.name)
            }
            this.info("OpenWindow:end");
            return false;
        }

        LogOut(): void {
            this.info("LogOut:start");
            try {
                if (this.LogoutFun != null) {
                    this.LogoutFun();
                }
            } catch (ex) {
                this.error("LogOut error:" + ex.name)
            }
            this.info("LogOut:end");
        }

        /**
         * 退出系统
         */
        Exit(): void {
            this.info("Exit:start");
            try {
                let afpbrowser: Iafpbrowser = (<any>window).afpbrowser;
                afpbrowser.exit();
            } catch (ex) {
                this.error("OpenWindow error:" + ex.name)
            }
            this.info("Exit:end");
        }
        /**
        * 测试数据
        */
        GetWinHandleID(): string {
            this.info("GetWinHandleID:start");
            try {
                let afpbrowser: Iafpbrowser = (<any>window).afpbrowser;
                return afpbrowser.getWinHandleID();
            } catch (ex) {
                this.error("GetWinHandleID error:" + ex.name)
                return '';
            }            
        }

        /**
         * 测试数据
         */
        getTestData(): string {
            return "testdata";
        }
    };

}

namespace ds.afp {
    /**
     * 视频操作相关
     */
    export class videoplayer implements Iplayerbrowser {
        /**
        基本配置
        */
        public config: Iconfig = null;

        /**
        播放器ID，exe句柄
        */
        public playerid: string = null;

        public constructor(config: Iconfig) {
            this.config = config;
        }

        /**
         * 启动播放器 js -> exe
         * @param parameters
         */
        public StartPlayer(parameters: string): boolean {
            try {
                let afpbrowser: Iafpplayerbrowser = (<any>window).afpbrowser;
                let res = afpbrowser.startPlayer(this.config.PlayerPath, parameters);

                if (res != null && res != '') {
                    this.playerid = res;

                    (<any>window).dsExt.cacheVideoPlayer(this.playerid, this);//将启动视频的对象放缓存里

                    if (this.config.StartupComplete != null && this.config.StartupComplete != undefined) {
                        this.config.StartupComplete(this.playerid);//加启动回完进行回调
                    }
                    return true;
                }
            } catch (ex) { }
            return false;
        }

        /**
        退出播放器 js -> exe
        */
        public SignOut(): boolean {
            try {
                let afpbrowser: Iafpplayerbrowser = (<any>window).afpbrowser;
                let res = afpbrowser.signOut(this.playerid);
                if (res) {
                    this.playerid = '';
                    if (this.config.SignOut != null && this.config.SignOut != undefined) {
                        this.config.SignOut(this.playerid);//退出回调
                    }
                    return true;
                }
            } catch (ex) { }
            return false;
        }

        /**
        向播放器传递参数 js -> exe
        */
        public SetPlayerParameters(parameters: string): boolean {
            try {
                let afpbrowser: Iafpplayerbrowser = (<any>window).afpbrowser;
                let res = afpbrowser.sendMessage(this.playerid, parameters);
                return res;

            } catch (ex) { }
            return false;
        }



    }

};


namespace ds.afp {
    /**
    /*话务操作
    */
    export class phone implements IPhone
    {
        /*
        单呼、组呼
        */
        call(callInfo: callInfo): boolean {
            let afpbrowser: Iafpbrowser = (<any>window).afpbrowser;
            let msg = JSON.stringify(callInfo);
            afpbrowser.call(callInfo);
            console.log("invoke:Call");
            return true;
        }

        /*
        短信
        */
        sendsms(messageInfo: messageInfo): boolean {
            let afpbrowser: Iafpbrowser = (<any>window).afpbrowser;
            let msg = JSON.stringify(messageInfo);
            afpbrowser.sendsms(msg);
            console.log("invoke:SendSMS");
            return true;
        }
    }
}

namespace ds.afp {
    export class office {
        /*
        打开office 文件
        下载
        */
        open(uri:String):void {

        }
        /*
        保存office文件上传到服务器
        */
        save(uri:String): void {

        }

    }
}



/**
 * ui调用类
 */
class dsExtHelper {
    public count: number = 0;
    ds_afp_browser_map: { [key: string]: any; } = {};
    ds_afp_videoplayer_map: { [key: string]: ds.afp.videoplayer; } = {};
    public interObj: any;
    public clientData: clientData;
    
    constructor() {
        if (this.ds_afp_browser_map != null) {
            this.ds_afp_browser_map["ds.browser"] = new ds.afp.browser();
            this.ds_afp_browser_map["ds.phone"] = new ds.afp.phone();
        }
    }

    getObject(key: string) {
        return this.ds_afp_browser_map[key];
    }

    /**
     * 收到消息 exe -> js -> js
     * @param message
     */
    ReceiveMessage(message: string): void {
        try {
            let msg = JSON.parse(message);
            if (typeof (this.interObj) == 'object')
                console.log('innerObj不为空');
            this.ds_afp_browser_map["ds.browser"].Registers.forEach((v, i, ar) => {
                v.ReceiveMessage(msg);//向所有JS发
            });
        } catch (e) {
            console.log('发送的消息格式必须为标准json格式,如{"name":"ds"}');
        }
    }
    
    getGuid(): string {
        return ds.afp.common.getGuid();
    }

    /**
     * 生成基础配置
     * @param clientdata
     */
    setClientData(clientdata: clientData): boolean {
        return this.ds_afp_browser_map["ds.browser"].SetClientData(clientdata);
    }

    /**
     * 创建一个视频控件
     * @param config
     */
    createVideoPlayer(config: Iconfig) {
        this.count = this.count + 1;
        return new ds.afp.videoplayer(config);
    }

    /**
     * 缓存一个视频控件
     * @param key
     * @param videoplayer
     */
    cacheVideoPlayer(key: string, videoplayer: any) {
        this.ds_afp_videoplayer_map[key] = videoplayer;
    }

    /**
     * 返回所有的视频控件
     */
    getAllVideoPlayer() {
        this.ds_afp_videoplayer_map;
    }

    createPhone() {

    }
    

    ///**
    // * 当有视频控件退出时触发
    // * @param playerid
    // */
    //signOut(playerid: string): void {
    //    for (let videoplayerObjKey in this.ds_afp_videoplayer_map) {
    //        let videoplayerObj = this.ds_afp_videoplayer_map[videoplayerObjKey];
    //        try {
    //            videoplayerObj.config.SignOut(videoplayerObj.playerid);//回调
    //        } catch (ex) {
    //            alert(ex)
    //        }
    //    }
    //}

    ///**
    // * 收到消息
    // * @param playerid
    // */
    //receiveMessage(playerid: string, message: string): void {
    //    for (let videoplayerObjKey in this.ds_afp_videoplayer_map) {
    //        let videoplayerObj = this.ds_afp_videoplayer_map[videoplayerObjKey];
    //        try {
    //            videoplayerObj.config.ReceiveMessage(videoplayerObj.playerid, message);//回调
    //        } catch (ex) {
    //            alert(ex)
    //        }
    //    }
    //}


    AlertCount(count: number,type: string ): void {
        this.ds_afp_browser_map["ds.browser"].AlertCount(count,type);
    }

    LogOut(): void {
        console.log("dsExt.LogOut");
        this.ds_afp_browser_map["ds.browser"].LogOut();
    }

}
let dsExt = new dsExtHelper();

