using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// 提供确认窗口和弹窗方法
    /// </summary>
    public static class AttachedWindow
    {
        public static bool? AttachedWindowService(WindowStyleBase winStyle)
        {
            if (winStyle.Wintype == windowType.AlertWindow)
            {
                AlterWin alertwin = new AlterWin(winStyle);
                alertwin.Width = winStyle.Width;
                alertwin.Height = winStyle.Height;
                alertwin.WinTitle = winStyle.Title;
                alertwin.WinContent = winStyle.Description;
                alertwin.OkButtonText = winStyle.OkBtnText;
                return alertwin.ShowDialog();
            }
            else
            {
                ConfirmWin confirWin = new ConfirmWin(winStyle);
                confirWin.Width = winStyle.Width;
                confirWin.Height = winStyle.Height;
                confirWin.WinTitle = winStyle.Title;
                confirWin.WinContent = winStyle.Description;
                confirWin.OkButtonText = winStyle.OkBtnText;
                confirWin.CancleButtonText = winStyle.CancleBtnText;
                return confirWin.ShowDialog();
            }
        }
    }
}
