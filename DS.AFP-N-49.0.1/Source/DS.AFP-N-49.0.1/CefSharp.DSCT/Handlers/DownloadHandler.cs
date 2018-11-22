// Copyright © 2010-2016 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using CefSharp;
using CefSharp.DSCT.Controls;
using CefSharp.Wpf;
using DS.AFP.Common.Core;
using DS.AFP.Framework.WPF.Browser.ViewModel;
using CefSharp.DSCT;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace CefSharp.DSCT.Handlers
{
    public class DownloadHandler : IDownloadHandler
    {


        public WebBrowserManager webBrowserManager
        {
            get
            {
                return DS.AFP.Framework.Spring.GlobalObject.Container.GetObject("WebBrowserManager") as WebBrowserManager;
            }
        }

        public ILoggerFacade Logger
        {
            get
            {
                return DS.AFP.Framework.Spring.GlobalObject.Container.GetObject("ILoggerFacade") as ILoggerFacade;
            }
        }


        Window hideWindow = null;
    

        public List<CacheModel> CacheList = new List<CacheModel>();
        /// <summary>
        /// 缓存下载窗口
        /// </summary>
        public ConcurrentDictionary<int, DownloadWindow> DownloadWindows = new ConcurrentDictionary<int, DownloadWindow>();

      


        public void OnBeforeDownload(IBrowser browser, DownloadItem downloadItem, IBeforeDownloadCallback callback)
        {
            try
            {
                if (!callback.IsDisposed)
                {
                    using (callback)
                    {
                        if (browser.IsPopup)
                        {
                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                try
                                {
                                    ChromiumWebBrowser chromiumWebBrowser = null;
                                    webBrowserManager.CacheWebBrowsers.TryTake(out chromiumWebBrowser);
                                    hideWindow = Window.GetWindow(chromiumWebBrowser);
                                    hideWindow.Hide();
                                }
                                catch (Exception ex)
                                {
                                    Logger.Error(ex);
                                }
                            }));
                        }

                        callback.Continue(downloadItem.SuggestedFileName, showDialog: true);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public void OnDownloadUpdated(IBrowser browser, DownloadItem downloadItem, IDownloadItemCallback callback)
        {
            try
            {
                if (!string.IsNullOrEmpty(downloadItem.FullPath))
                {
                    DownloadWindow tempWindow = null;

                    if (DownloadWindows.TryGetValue(downloadItem.Id, out tempWindow))//存在缓存里
                    {
                        var vm = tempWindow.DataContext as DownloadItemViewModel;
                        vm.ReceivedBytes = downloadItem.ReceivedBytes;
                        vm.TotalBytes = downloadItem.TotalBytes;

                        if (downloadItem.IsComplete)//删除完成删除
                        {
                            tempWindow.Close();
                            callback.Dispose();
                            DownloadWindows.TryRemove(downloadItem.Id, out tempWindow);
                        }
                    }
                    else
                    {
                        if (CacheList.Count(c => c.ID == downloadItem.Id) > 0)
                            return;

                        tempWindow = new DownloadWindow();
                        var vm = new DownloadItemViewModel()
                        {
                            FullPath = downloadItem.FullPath,
                            ID = downloadItem.Id,
                            ReceivedBytes = downloadItem.ReceivedBytes,
                            TotalBytes = downloadItem.TotalBytes,
                            Url = downloadItem.Url
                        };

                        tempWindow.Closed += (s, e) =>
                        {
                            CacheList.Add(new CacheModel() { ID = downloadItem.Id, AddTime = DateTime.Now });
                            downloadItem.IsCancelled = true;
                            tempWindow.DataContext = null;
                            tempWindow.Close();
                            callback.Dispose();
                            DownloadWindows.TryRemove(downloadItem.Id, out tempWindow);

                            Application.Current.Dispatcher.Invoke(new Action(() =>
                            {
                                if (hideWindow != null)
                                    hideWindow.Close();
                            }));
                        };
                        tempWindow.DataContext = vm;
                        tempWindow.Show();
                        tempWindow.Activate();

                        DownloadWindows.TryAdd(downloadItem.Id, tempWindow);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }
    }

    public class CacheModel
    {
        public int ID { get; set; }

        public DateTime AddTime { get; set; }
    }
}
