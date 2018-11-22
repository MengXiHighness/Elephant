/**
供JS调用浏览器接口
*/
export interface Iafpbrowser {
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
    showPrompt(count: number): void;
}


/**
浏览器提示的接口
*/
export interface Iafpplayerbrowser {
    /**
    启动播放器
    */
    startPlayer(parameters: string): string;

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
export interface Iconfig {
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
export interface Iplayerbrowser {

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

