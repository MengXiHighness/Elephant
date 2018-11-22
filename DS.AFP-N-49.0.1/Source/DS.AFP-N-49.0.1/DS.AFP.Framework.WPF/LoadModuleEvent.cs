using DS.AFP.Framework.Events;
using DS.AFP.Framework.Modularity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.WPF
{
    public class LoadModuleEvent : CompositePresentationEvent<LoadModuleEvent>
    {
        public int OffSetTime { get; set; }
        public LoadModuleCompletedEventArgs LoadModuleCompletedEventArgs { get; set; }
    }

    public class CloseSplashEvent : CompositePresentationEvent<CloseSplashEvent>
    {
    }

    public class NavigateImgUrEvent : CompositePresentationEvent<string[]>
    {
    }
    
    
}
