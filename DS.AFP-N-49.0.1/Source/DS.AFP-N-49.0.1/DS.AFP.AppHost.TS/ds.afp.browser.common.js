var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    }
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
var clientData = (function () {
    function clientData() {
    }
    return clientData;
}());
var ds;
(function (ds) {
    var afp;
    (function (afp) {
        var common = (function () {
            function common() {
            }
            common.prototype.getNowFormatDate = function () {
                var date = new Date();
                var seperator1 = "-";
                var seperator2 = ":";
                var month = date.getMonth() + 1;
                var strDate = date.getDate();
                var temp_month = month.toString();
                if (month >= 1 && month <= 9) {
                    temp_month = "0" + month.toString();
                }
                var temp_strDate = strDate.toString();
                if (strDate >= 0 && strDate <= 9) {
                    temp_strDate = "0" + strDate.toString();
                }
                var currentdate = date.getFullYear().toString() + seperator1 + temp_month + seperator1 + temp_strDate
                    + " " + date.getHours().toString() + seperator2 + date.getMinutes().toString()
                    + seperator2 + date.getSeconds().toString();
                return currentdate;
            };
            common.prototype.info = function (logcontent) {
                if (console) {
                    try {
                        var datetime = this.getNowFormatDate();
                        console.log("log info:" + datetime + "  " + logcontent);
                    }
                    catch (ex) {
                    }
                }
            };
            common.prototype.error = function (logcontent) {
                if (console) {
                    try {
                        var datetime = this.getNowFormatDate();
                        console.log("log error:" + datetime + "  " + logcontent);
                    }
                    catch (ex) {
                    }
                }
            };
            common.getGuid = function () {
                return new GUID().toString();
            };
            return common;
        }());
        afp.common = common;
        ;
        var GUID = (function () {
            function GUID(str) {
                this.str = str || GUID.getNewGUIDString();
            }
            GUID.prototype.toString = function () {
                return this.str;
            };
            GUID.getNewGUIDString = function () {
                var d = new Date().getTime();
                if (window.performance && typeof window.performance.now === "function") {
                    d += performance.now();
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
(function (ds) {
    var afp;
    (function (afp) {
        var browser = (function (_super) {
            __extends(browser, _super);
            function browser() {
                var _this = _super.call(this) || this;
                _this.Registers = [];
                _this.LogoutFun = null;
                return _this;
            }
            browser.prototype.LogoutRegister = function (info) {
                console.log("LogoutRegister");
                this.LogoutFun = info.logoutFun;
                return true;
            };
            browser.prototype.MessageRegister = function (info) {
                this.Registers.push(info);
                return false;
            };
            browser.prototype.StartProcess = function (processPath, parameter) {
                if (parameter === void 0) { parameter = ''; }
                this.info("StartProcess:start processPath=" + processPath + ";parameter=" + parameter.toString());
                try {
                    var afpbrowser = window.afpbrowser;
                    afpbrowser.startProcess(processPath, parameter);
                }
                catch (ex) {
                    this.error("StartProcess error:" + ex.name);
                }
                this.info("StartProcess:end");
            };
            browser.prototype.Minimize = function () {
                this.info("Minimize:start");
                try {
                    var afpbrowser = window.afpbrowser;
                    afpbrowser.minimize();
                }
                catch (ex) {
                    this.error("Minimize error:" + ex.name);
                }
                this.info("Minimize:end");
            };
            browser.prototype.Maximize = function () {
                this.info("Maximize:start");
                try {
                    var afpbrowser = window.afpbrowser;
                    afpbrowser.maximize();
                }
                catch (ex) {
                    this.error("Maximize error:" + ex.name);
                }
                this.info("Maximize:end");
            };
            browser.prototype.AlertCount = function (count, type) {
                this.info("ShowPrompt:start");
                try {
                    var afpbrowser = window.afpbrowser;
                    afpbrowser.alertCount(count, type);
                }
                catch (ex) {
                    this.error("ShowPrompt error:" + ex.name);
                }
                this.info("ShowPrompt:end");
            };
            browser.prototype.SendMessageToBrowser = function (message) {
                var msg = JSON.stringify(message);
                this.info("SendMessageToBrowser:start");
                try {
                    var afpbrowser = window.afpbrowser;
                    afpbrowser.sendMessageToBrowser(msg);
                    return true;
                }
                catch (ex) {
                    this.error("SendMessageToBrowser error:" + ex.name);
                }
                this.info("SendMessageToBrowser:end");
                return false;
            };
            browser.prototype.SendMessageToClient = function (message) {
                var msg = JSON.stringify(message);
                this.info("SendMessageToClient:start");
                try {
                    var afpbrowser = window.afpbrowser;
                    afpbrowser.sendMessageToClient(msg);
                    return true;
                }
                catch (ex) {
                    this.error("SendMessageToClient error:" + ex.name);
                }
                this.info("SendMessageToClient:end");
                return false;
            };
            browser.prototype.SendWebMessage = function (message) {
                var msg = JSON.stringify(message);
                this.info("SendWebMessage:start");
                try {
                    var afpbrowser = window.afpbrowser;
                    afpbrowser.sendWebMessage(msg);
                    return true;
                }
                catch (ex) {
                    this.error("SendWebMessage error:" + ex.name);
                }
                this.info("SendWebMessage:end");
                return false;
            };
            browser.prototype.SetClientData = function (clientdata) {
                var msg = JSON.stringify(clientdata);
                this.info("SetClientData:start");
                try {
                    var afpbrowser = window.afpbrowser;
                    afpbrowser.setClientData(msg);
                    return true;
                }
                catch (ex) {
                    this.error("SetClientData error:" + ex.name);
                }
                this.info("SetClientData:end");
                return false;
            };
            browser.prototype.OpenWindow = function (uri, screenNumber) {
                this.info("OpenWindow:start");
                try {
                    var afpbrowser = window.afpbrowser;
                    afpbrowser.openWindow(uri, screenNumber);
                    return true;
                }
                catch (ex) {
                    this.error("OpenWindow error:" + ex.name);
                }
                this.info("OpenWindow:end");
                return false;
            };
            browser.prototype.LogOut = function () {
                this.info("LogOut:start");
                try {
                    if (this.LogoutFun != null) {
                        this.LogoutFun();
                    }
                }
                catch (ex) {
                    this.error("LogOut error:" + ex.name);
                }
                this.info("LogOut:end");
            };
            browser.prototype.Exit = function () {
                this.info("Exit:start");
                try {
                    var afpbrowser = window.afpbrowser;
                    afpbrowser.exit();
                }
                catch (ex) {
                    this.error("OpenWindow error:" + ex.name);
                }
                this.info("Exit:end");
            };
            browser.prototype.GetWinHandleID = function () {
                this.info("GetWinHandleID:start");
                try {
                    var afpbrowser = window.afpbrowser;
                    return afpbrowser.getWinHandleID();
                }
                catch (ex) {
                    this.error("GetWinHandleID error:" + ex.name);
                    return '';
                }
            };
            browser.prototype.getTestData = function () {
                return "testdata";
            };
            return browser;
        }(ds.afp.common));
        afp.browser = browser;
        ;
    })(afp = ds.afp || (ds.afp = {}));
})(ds || (ds = {}));
(function (ds) {
    var afp;
    (function (afp) {
        var videoplayer = (function () {
            function videoplayer(config) {
                this.config = null;
                this.playerid = null;
                this.config = config;
            }
            videoplayer.prototype.StartPlayer = function (parameters) {
                try {
                    var afpbrowser = window.afpbrowser;
                    var res = afpbrowser.startPlayer(this.config.PlayerPath, parameters);
                    if (res != null && res != '') {
                        this.playerid = res;
                        window.dsExt.cacheVideoPlayer(this.playerid, this);
                        if (this.config.StartupComplete != null && this.config.StartupComplete != undefined) {
                            this.config.StartupComplete(this.playerid);
                        }
                        return true;
                    }
                }
                catch (ex) { }
                return false;
            };
            videoplayer.prototype.SignOut = function () {
                try {
                    var afpbrowser = window.afpbrowser;
                    var res = afpbrowser.signOut(this.playerid);
                    if (res) {
                        this.playerid = '';
                        if (this.config.SignOut != null && this.config.SignOut != undefined) {
                            this.config.SignOut(this.playerid);
                        }
                        return true;
                    }
                }
                catch (ex) { }
                return false;
            };
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
    })(afp = ds.afp || (ds.afp = {}));
})(ds || (ds = {}));
;
(function (ds) {
    var afp;
    (function (afp) {
        var phone = (function () {
            function phone() {
            }
            phone.prototype.call = function (callInfo) {
                var afpbrowser = window.afpbrowser;
                var msg = JSON.stringify(callInfo);
                afpbrowser.call(callInfo);
                console.log("invoke:Call");
                return true;
            };
            phone.prototype.sendsms = function (messageInfo) {
                var afpbrowser = window.afpbrowser;
                var msg = JSON.stringify(messageInfo);
                afpbrowser.sendsms(msg);
                console.log("invoke:SendSMS");
                return true;
            };
            return phone;
        }());
        afp.phone = phone;
    })(afp = ds.afp || (ds.afp = {}));
})(ds || (ds = {}));
(function (ds) {
    var afp;
    (function (afp) {
        var office = (function () {
            function office() {
            }
            office.prototype.open = function (uri) {
            };
            office.prototype.save = function (uri) {
            };
            return office;
        }());
        afp.office = office;
    })(afp = ds.afp || (ds.afp = {}));
})(ds || (ds = {}));
var dsExtHelper = (function () {
    function dsExtHelper() {
        this.count = 0;
        this.ds_afp_browser_map = {};
        this.ds_afp_videoplayer_map = {};
        if (this.ds_afp_browser_map != null) {
            this.ds_afp_browser_map["ds.browser"] = new ds.afp.browser();
            this.ds_afp_browser_map["ds.phone"] = new ds.afp.phone();
        }
    }
    dsExtHelper.prototype.getObject = function (key) {
        return this.ds_afp_browser_map[key];
    };
    dsExtHelper.prototype.ReceiveMessage = function (message) {
        try {
            var msg_1 = JSON.parse(message);
            if (typeof (this.interObj) == 'object')
                console.log('innerObj不为空');
            this.ds_afp_browser_map["ds.browser"].Registers.forEach(function (v, i, ar) {
                v.ReceiveMessage(msg_1);
            });
        }
        catch (e) {
            console.log('发送的消息格式必须为标准json格式,如{"name":"ds"}');
        }
    };
    dsExtHelper.prototype.getGuid = function () {
        return ds.afp.common.getGuid();
    };
    dsExtHelper.prototype.setClientData = function (clientdata) {
        return this.ds_afp_browser_map["ds.browser"].SetClientData(clientdata);
    };
    dsExtHelper.prototype.createVideoPlayer = function (config) {
        this.count = this.count + 1;
        return new ds.afp.videoplayer(config);
    };
    dsExtHelper.prototype.cacheVideoPlayer = function (key, videoplayer) {
        this.ds_afp_videoplayer_map[key] = videoplayer;
    };
    dsExtHelper.prototype.getAllVideoPlayer = function () {
        this.ds_afp_videoplayer_map;
    };
    dsExtHelper.prototype.createPhone = function () {
    };
    dsExtHelper.prototype.AlertCount = function (count, type) {
        this.ds_afp_browser_map["ds.browser"].AlertCount(count, type);
    };
    dsExtHelper.prototype.LogOut = function () {
        console.log("dsExt.LogOut");
        this.ds_afp_browser_map["ds.browser"].LogOut();
    };
    return dsExtHelper;
}());
var dsExt = new dsExtHelper();
