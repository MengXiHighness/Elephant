// Copyright © 2010-2016 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp.Internals;
using CefSharp.Wpf.Internals;
using CefSharp.Wpf.Rendering;
using Microsoft.Win32.SafeHandles;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Threading;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using System.Collections.Generic;


namespace CefSharp.Wpf
{
    public partial class ChromiumWebBrowser : ContentControl, IRenderWebBrowser, IWpfWebBrowser
    {
        #region 添加最小化和启动其它应用程序
        public event Action<string, string> StartProcessEvent;
        public event Action MinimizeEvent;
        public event Action MaximizeEvent;
        public event Action<int,string> ShowPromptEvent;

        public static IntPtr Handle { get; set; }







        /// <summary>
        /// 启动exe程序
        /// </summary>
        /// <param name="processPath"></param>
        /// <param name="parameter"></param>
        public void OnStartProcess(string processPath, string parameter)
        {
            try
            {
                UiThreadRunAsync(() =>
                {
                    if(StartProcessEvent!=null)
                        StartProcessEvent(processPath, parameter);

                    if (string.IsNullOrEmpty(parameter))
                    {
                        Process.Start(processPath);
                    }
                    else
                    {
                        Process.Start(processPath, parameter);
                    }
                });
            }
            catch (Exception ex) { }
        }

        Window MainWindow = null;
        string[] windows = { "DS.AFP.WPF.DSCT.MainWindow", "DS.AFP.WPF.App.MainWindow" };
        public Window GetMainWindow()
        {
            if (MainWindow == null)
            {
                foreach (Window w in Application.Current.Windows)
                {
                    if (windows.Contains(w.GetType().FullName))
                    {
                        MainWindow = w;
                        break;
                    }
                }
            }
            return MainWindow;
        }

        /// <summary>
        /// 最小化
        /// </summary>
        public void OnMinimize()
        {
            try
            {
                UiThreadRunAsync(() =>
                {
                    if(MinimizeEvent!=null)
                        MinimizeEvent();
                    var win = GetMainWindow();
                    if (win != null)
                    {
                        win.WindowState = WindowState.Minimized;
                    }
                });
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 最大化
        /// </summary>
        public void OnMaximize()
        {
            try
            {
                UiThreadRunAsync(() =>
                {
                    if(MaximizeEvent!=null)
                        MaximizeEvent();
                    var win = GetMainWindow();
                    if (win != null)
                    {
                        GetMainWindow().WindowState = WindowState.Maximized;
                        GetMainWindow().Activate();
                    }
                });
            }
            catch (Exception ex) { }
        }


        /// <summary>
        /// 最大化
        /// </summary>
        public void OnShowPrompt(int count,string type)
        {
            try
            {
                UiThreadRunAsync(() =>
                {
                    if(ShowPromptEvent!=null)
                        ShowPromptEvent(count,type);
                });
            }
            catch (Exception ex) { }
        }




        #endregion


        public static DateTime LastTime;


        public static IList<string> PlayerHandles = new List<string>();

        /// <summary>
        /// 启动一个视频控件并返回控件handle
        /// </summary>
        /// <param name="path"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string OnStartPlayer(string path, string parameters)
        {
            LastTime = DateTime.Now;
            string res = "";
            try
            {
                Process p = null;
                if (string.IsNullOrEmpty(parameters))
                {
                    p = Process.Start(path);
                }
                else
                {
                    p = Process.Start(path, parameters);
                }

                while (p.MainWindowHandle == IntPtr.Zero && (DateTime.Now - LastTime).TotalSeconds < 5)
                {
                    Thread.Sleep(100);
                    p.Refresh();
                }
                //System.Threading.Thread.Sleep(50);
                res = p.MainWindowHandle.ToString();
                if (!string.IsNullOrEmpty(res))
                {
                    PlayerHandles.Add(res);
                }
            }
            catch (Exception ex)
            {
            }
            return res;
        }
    }
}
