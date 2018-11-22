using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


public class Win32API
{
    [DllImport("User32.dll", EntryPoint = "FindWindow")]
    public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("User32.dll", EntryPoint = "FindWindowEx")]
    public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpClassName, string lpWindowName);

    /// <summary>
    /// 自定义的结构
    /// </summary>
    // 注意：必须是结构体不能是类即必须为struct关键字不能是class,否则在接收消息时会产生异常 
    public struct My_lParam
    {
        public string playerid;
        public string json;
    }

    /// <summary>
    /// 使用COPYDATASTRUCT来传递字符串
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct COPYDATASTRUCT
    {
        public IntPtr dwData;
        public int cbData;
        [MarshalAs(UnmanagedType.LPStr)]
        public string lpData;
    }

    //消息发送API
    [DllImport("User32.dll", EntryPoint = "SendMessage")]
    public static extern int SendMessage(
        IntPtr hWnd,        // 信息发往的窗口的句柄
        int Msg,            // 消息ID
        int wParam,         // 参数1
        int lParam          //参数2
    );

    //消息发送API
    [DllImport("User32.dll", EntryPoint = "SendMessage")]
    public static extern int SendMessage(
        IntPtr hWnd,        // 信息发往的窗口的句柄
        int Msg,            // 消息ID
        int wParam,         // 参数1
        ref My_lParam lParam //参数2
    );

    //消息发送API
    [DllImport("User32.dll", EntryPoint = "SendMessage")]
    public static extern int SendMessage(
        IntPtr hWnd,        // 信息发往的窗口的句柄
        int Msg,            // 消息ID
        int wParam,         // 参数1
        ref COPYDATASTRUCT lParam  //参数2
    );

    //消息发送API
    [DllImport("User32.dll", EntryPoint = "PostMessage")]
    public static extern int PostMessage(
        IntPtr hWnd,        // 信息发往的窗口的句柄
        int Msg,            // 消息ID
        int wParam,         // 参数1
        int lParam            // 参数2
    );



    //消息发送API
    [DllImport("User32.dll", EntryPoint = "PostMessage")]
    public static extern int PostMessage(
        IntPtr hWnd,        // 信息发往的窗口的句柄
        int Msg,            // 消息ID
        int wParam,         // 参数1
        ref My_lParam lParam //参数2
    );

    //异步消息发送API
    [DllImport("User32.dll", EntryPoint = "PostMessage")]
    public static extern int PostMessage(
        IntPtr hWnd,        // 信息发往的窗口的句柄
        int Msg,            // 消息ID
        int wParam,         // 参数1
        ref COPYDATASTRUCT lParam  // 参数2
    );

}
