"use strict";
var ds;
(function (ds) {
    var afp;
    (function (afp) {
        /**
         * 视频操作相关
         */
        var videoplayer = (function () {
            function videoplayer(config) {
                /**
                基本配置
                */
                this.config = null;
                /**
                播放器ID，exe句柄
                */
                this.playerid = null;
                this.config = config;
                window.dsExt.cacheVideoPlayer(new GUID().toString(), this);
            }
            /**
             * 启动播放器 js -> exe
             * @param parameters
             */
            videoplayer.prototype.StartPlayer = function (parameters) {
                try {
                    var afpbrowser = window.afpbrowser;
                    var res = afpbrowser.startPlayer(parameters);
                    if (res != null && res != '') {
                        this.playerid = res;
                        if (this.config.StartupComplete != null && this.config.StartupComplete != undefined) {
                            this.config.StartupComplete(this.playerid); //加启动回完进行回调
                        }
                        return true;
                    }
                }
                catch (ex) { }
                return false;
            };
            /**
            退出播放器 js -> exe
            */
            videoplayer.prototype.SignOut = function () {
                try {
                    var afpbrowser = window.afpbrowser;
                    var res = afpbrowser.signOut(this.playerid);
                    if (res) {
                        this.playerid = '';
                        if (this.config.StartupComplete != null && this.config.StartupComplete != undefined) {
                            this.config.StartupComplete(this.playerid); //加启动回完进行回调
                        }
                        return true;
                    }
                }
                catch (ex) { }
                return false;
            };
            /**
            向播放器传递参数 js -> exe
            */
            videoplayer.prototype.SetPlayerParameters = function (parameters) {
                try {
                    var afpbrowser = window.afpbrowser;
                    var res = afpbrowser.sendMessage(this.playerid, parameters);
                    return res;
                }
                catch (ex) { }
                return false;
            };
            return videoplayer;
        }());
        afp.videoplayer = videoplayer;
        var GUID = (function () {
            function GUID(str) {
                this.str = str || GUID.getNewGUIDString();
            }
            GUID.prototype.toString = function () {
                return this.str;
            };
            GUID.getNewGUIDString = function () {
                // your favourite guid generation function could go here
                // ex: http://stackoverflow.com/a/8809472/188246
                var d = new Date().getTime();
                if (window.performance && typeof window.performance.now === "function") {
                    d += performance.now(); //use high-precision timer if available
                }
                return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = (d + Math.random() * 16) % 16 | 0;
                    d = Math.floor(d / 16);
                    return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
                });
            };
            return GUID;
        }());
        afp.GUID = GUID;
    })(afp = ds.afp || (ds.afp = {}));
})(ds || (ds = {}));
//# sourceMappingURL=ds.videoplayer.js.map