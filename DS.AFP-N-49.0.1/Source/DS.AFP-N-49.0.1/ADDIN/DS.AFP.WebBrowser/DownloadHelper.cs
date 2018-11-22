using CefSharp.Wpf.ViewModel;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DS.AFP.WebBrowser
{
    public static class DownloadHelper
    {
        /// <summary>
        /// 缓存下载窗口
        /// </summary>
        public static ConcurrentDictionary<int, DownloadWindow> DownloadWindows = new ConcurrentDictionary<int, DownloadWindow>();

        public static void AddDownload(DownloadFileInfo downloadItem)
        {
            DownloadWindow tempWindow = null;
            lock (DownloadWindows)
            {
                if (!DownloadWindows.TryGetValue(downloadItem.Id, out tempWindow))//不在缓存列表
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.FileName = downloadItem.SuggestedFileName;
                    sfd.Filter = "所有文件|*";

                    var res = sfd.ShowDialog();
                    if (res == DialogResult.OK)
                    {
                        tempWindow = new DownloadWindow();
                        var vm = new DownloadItemViewModel()
                        {
                            FullPath = sfd.FileName,
                            ID = downloadItem.Id,
                            ReceivedBytes = 0,
                            Url = downloadItem.Url,
                            TotalBytes = downloadItem.TotalBytes
                        };
                        tempWindow.Closed += (s, e) =>
                        {
                            tempWindow.DataContext = null;
                            tempWindow.Close();
                            DownloadWindows.TryRemove(downloadItem.Id, out tempWindow);
                        };
                        tempWindow.DataContext = vm;
                        tempWindow.Show();
                        tempWindow.Activate();

                        DownloadWindows.TryAdd(downloadItem.Id, tempWindow);
                    }
                }
            }
        }

    }


    public class DownloadFileInfo
    {
        public int Id { get; set; }

        //
        // 摘要:
        //     Returns the suggested file name.
        public string SuggestedFileName { get; set; }
        //
        // 摘要:
        //     Returns the total number of bytes.
        public long TotalBytes { get; set; }
        //
        // 摘要:
        //     Returns the URL.
        public string Url { get; set; }
    }
}
