using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Windows.Forms;
using System.Windows.Threading;
using DS.AFP.Common.Core;
using DS.AFP.Framework.WPF;
using DS.AFP.Framework.WPF.Browser.EventSignal;

namespace DS.AFP.WPF.App.Utility
{
    public class AlertManager
    {
        SoundPlayer player;
        public event Action<int> CallBackEvent;

        public AlertManager()
        {
            this.player = new SoundPlayer("alert.wav");
            App.EventAggregator.GetEvent<ShowPromptEventSignal>().Subscribe((arg) =>
            {
                try
                {
                    if (arg.Count > 0)
                    {
                        Play();
                    }
                    else
                    {
                        Stop();
                    }
                }
                catch (Exception ex) { }

            }, Framework.Events.ThreadOption.UIThread, true);
        }

        /// <summary>
        /// 播放提示音并闪动任务栏图标
        /// </summary>
        public void Play()
        {
            try
            {
                if(CallBackEvent!=null)
                    CallBackEvent.Invoke(1);
                player.PlayLooping();
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 停止播放声音并停止图标闪动
        /// </summary>
        public void Stop()
        {
            try
            {
                if(CallBackEvent!=null)
                    CallBackEvent.Invoke(0);
                player.Stop();
            }
            catch (Exception ex) { }
        }


    }
}
