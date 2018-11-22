using DS.AFP.Framework.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.WPF.App.View.Controls.SplashScreen
{
    public interface ILoadEventRegist
    {
        void Init(IEventAggregator EventAggregator);
    }
}
