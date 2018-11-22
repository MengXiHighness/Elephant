import { Iafpbrowser, Iconfig, Iplayerbrowser, Iafpplayerbrowser } from './model/model_videoplayer';



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
                let res = afpbrowser.startPlayer(parameters);
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
