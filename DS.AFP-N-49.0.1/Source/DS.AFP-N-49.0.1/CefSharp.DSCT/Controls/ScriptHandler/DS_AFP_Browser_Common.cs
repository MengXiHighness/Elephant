using CefSharp.Wpf;
using DS.AFP.Common.Core;
using DS.AFP.Framework.Events;
using DS.AFP.Framework.Spring;
using DS.AFP.Framework.WPF;
using DS.AFP.Framework.WPF.Browser.EventSignal;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace CefSharp.DSCT.Browser
{
    public class DS_AFP_Browser_Common
    {

        Action<string, string> StartProcessEvent;
        Action MinimizeEvent;
        Action MaximizeEvent;
        Action<int,string> AlertCountEvent;
        Func<string, string, string> StartPlayerEvent;

        public ILoggerFacade Logger
        {
            get
            {
                return GlobalObject.Container.GetObject("ILoggerFacade") as ILoggerFacade;
            }
        }

        public IEventAggregator EventAggregator
        {
            get
            {
                return DS.AFP.Framework.Spring.GlobalObject.Container.GetObject("IEventAggregator") as IEventAggregator;
            }
        }
        ChromiumWebBrowser chromiumWebBrowser;

        private string _winHandleID { get; set; }

        public DS_AFP_Browser_Common(ChromiumWebBrowser chromiumWebBrowser, Action<string, string> _StartProcessEvent, Action _MinimizeEvent, Action _MaximizeEvent, Action<int,string> _ShowPromptEvent,
             Func<string, string, string> _StartPlayerEvent)
        {
            this.StartProcessEvent = _StartProcessEvent;
            this.MinimizeEvent = _MinimizeEvent;
            this.MaximizeEvent = _MaximizeEvent;
            this.AlertCountEvent = _ShowPromptEvent;
            this.StartPlayerEvent = _StartPlayerEvent;
            this.chromiumWebBrowser = chromiumWebBrowser;
           
        }

        //public string GetWinHandleID()
        //{
        //    if (this.chromiumWebBrowser.HwndSource != null)
        //        this._winHandleID = chromiumWebBrowser.HwndSource.Handle.ToString();
        //    return this._winHandleID;
        //}

        public void StartProcess(string processPath, string parameter)
        {
            Logger.Info("StartProcess:processPath=" + processPath + ";parameter=" + parameter);
            this.StartProcessEvent(processPath, parameter);
        }

        public void Minimize()
        {
            Logger.Info("Minimize");
            this.MinimizeEvent();
        }

        public void Maximize()
        {
            Logger.Info("Maximize");
            this.MaximizeEvent();
        }

        public void AlertCount(int count,string type)
        {
            Logger.Info("ShowPrompt");
            this.AlertCountEvent(count,type);
            if (count > 0)
            {
                this.MaximizeEvent();
            }
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        public void Exit()
        {
            Logger.Info("Exit");
            //BrowserExitEvent
            EventAggregator.GetEvent<BrowserExitEventSignal>().Publish(null);
            //Environment.Exit(Environment.ExitCode);
        }


        #region 视频相关
        /// <summary>
        /// 启动播放器
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string StartPlayer(string path, string parameters)
        {
            Logger.Info("StartPlayer");
            string res = this.StartPlayerEvent(path, parameters);
            return res;
        }


        /// <summary>
        /// 退出播放器
        /// </summary>
        public bool SignOut(string handle)
        {
            Logger.Info("SignOut");
            return false;
        }

        /// <summary>
        /// 向播放器传递参数
        /// </summary>
        /// <param name="handle"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public bool SetPlayerParameters(string handle, string parameters)
        {
            Logger.Info("SetPlayerParameters");
            SendMessage(handle, parameters);
            return false;
        }

        /// <summary>
        /// 发送数据给播放器
        /// </summary>
        /// <param name="playerid"></param>
        /// <param name="parameters"></param>
        public void SendMessage(string playerid, string parameters)
        {
            Logger.Info("SendMessage");
            try
            {
                IntPtr intPtr = new IntPtr(int.Parse(playerid));

                Win32API.My_lParam lp = new Win32API.My_lParam();
                lp.playerid = playerid;
                lp.json = parameters;


                //new Thread(new System.Threading.ThreadStart(() =>
                //{
                try
                {
                    Win32API.SendMessage(intPtr, 100, 3, ref lp);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }

                //})).Start();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public void SendMessageToBrowser(string message)
        {
            Logger.Info("SendMessageToBrowser");
            try
            {
                EventAggregator.GetEvent<ReceiveWebBrowserMessageEventSignal>().Publish(message);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public void SendMessageToClient(string message)
        {
            Logger.Info("SendMessageToDSCT");
            try
            {
                EventAggregator.GetEvent<ReceiveWebBrowserMessageToClientEventSignal>().Publish(message);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public void SendWebMessage(string message)
        {
            Logger.Info("SendMessageToWebSocketClient");
            try
            {
                EventAggregator.GetEvent<ReceiveWebMessageToWebSocketClientEventSignal>().Publish(message);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// 门户发送的配置信息
        /// </summary>
        /// <param name="clientData"></param>
        public void SetClientData(string clientData)
        {
            Logger.Info("SetClientData");
            try
            {
                (DS.AFP.Framework.Spring.GlobalObject.Container.GetObject("IDsEnvironment") as DS.AFP.Common.Core.IDsEnvironment).ShareData["ClientData"] = clientData;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uri"></param>
        /// <param name="screenNumber"></param>
        public void OpenWindow(string uri, int screenNumber)
        {
            Logger.Info("OpenWindow");
            try
            {
                EventAggregator.GetEvent<OpenWindowEventSignal>().Publish(new OpenWindowInfo
                {
                    ScreenNumber = screenNumber,
                    Uri = uri
                });
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }


        #endregion

        #region 话务相关

        public bool call(string callInfo)
        {
            try
            {
                callInfo ci = callInfo.DeserializeFromJson<callInfo>();
                Console.WriteLine("调用Call");
            }catch(Exception ex)
            {
                Console.WriteLine("序列化异常");
            }
            return true;
        }
        
        public bool sendsms(string messageInfo)
        {
            try
            {
                messageInfo ci = messageInfo.DeserializeFromJson<messageInfo>();
                Console.WriteLine("调用SendSMS");
            }catch(Exception ex)
            {
                Console.WriteLine("序列化异常");
            }
            return true;
        }
        #endregion


    }

    public class BrowserInfo
    {
        public class head
        {
            public string code { get; set; }
            public string from { get; set; }
            public List<string> tos { get; set; }
        }
        public string body { get; set; }
    }

    public class callInfo
    {
        public string businessid { get; set; }
        public string systemcode { get; set; }
        public string userid { get; set; }
        public string actiontype { get; set; }
        public string[] phoneMembers { get; set; }
    }

    public class messageInfo
    {
        public string businessid { get; set; }
        public string content { get; set; }
        public string systemcode { get; set; }
        public string userid { get; set; }
        public string[] phoneMembers { get; set; }
    }





}
