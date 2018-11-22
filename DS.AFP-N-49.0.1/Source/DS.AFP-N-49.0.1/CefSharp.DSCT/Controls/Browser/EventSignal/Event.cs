using DS.AFP.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.WPF.Browser.EventSignal
{

    /// <summary>
    /// 浏览器事件
    /// </summary>
    public class WebBrowserEventSignal : CompositePresentationEvent<string>
    {

    }

    /// <summary>
    /// 浏览器事件，是否提示声光提示
    /// </summary>
    public class ShowPromptEventSignal : CompositePresentationEvent<ShowPromptEventArgs>
    {

    }

    public class ShowPromptEventArgs
    {
        public int Count { get; set; }
        public string Type { get; set; }
    }

    /// <summary>
    /// 向浏览器发送消息
    /// </summary>
    public class SendMessageToWebBrowserEventSignal : CompositePresentationEvent<string>
    {

    }

    /// <summary>
    /// 收到页面js发来的消息，并转发浏览器的其他页面
    /// </summary>
    public class ReceiveWebBrowserMessageEventSignal : CompositePresentationEvent<string>
    {

    }

    //
    /// <summary>
    /// 收到页面js发来的消息,但不再返回js
    /// </summary>
    public class ReceiveWebBrowserMessageToClientEventSignal : CompositePresentationEvent<string>
    {

    }

    //
    /// <summary>
    /// 收到页面js发来的消息,转发到websocket服务器
    /// </summary>
    public class ReceiveWebMessageToWebSocketClientEventSignal : CompositePresentationEvent<string>
    {

    }

    public class OpenWindowEventSignal : CompositePresentationEvent<OpenWindowInfo>
    {

    }

    public class BrowserLogOutEventSignal : CompositePresentationEvent<BrowserLogOutInfo>
    {

    }
    public class BrowserExitEventSignal : CompositePresentationEvent<object>
    {

    }

    /// <summary>
    /// JS打开窗口数据
    /// </summary>
    public class OpenWindowInfo
    {
        /// <summary>
        /// 要打开的地址
        /// </summary>
        public string Uri { get; set; }

        /// <summary>
        /// 显存在第几屏
        /// </summary>
        public int ScreenNumber { get; set; }
    }

    public class BrowserLogOutInfo
    {
        public string Uri { get; set; }

        public Action CallBack { get; set; }
    }

}
