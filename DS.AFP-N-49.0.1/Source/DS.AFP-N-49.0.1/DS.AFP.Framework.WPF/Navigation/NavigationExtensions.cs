using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DS.AFP.Framework.Regions;
//jn
namespace DS.AFP.Framework.WPF
{

    /// <summary>
    /// 导航扩展
    /// </summary>
    public static class NavigationExtensions
    {
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            if (regionName == null)
            {
                navigationCallback(new NavigationResult(new NavigationContext(null,target),false));
                return;
            }
            if (regionManager.Regions.ContainsRegionWithName(regionName))
            {
                regionManager.Regions[regionName].RequestNavigate(target, navigationCallback, navigationParameters);
            }
            else
            {
                navigationCallback(new NavigationResult(new NavigationContext(null,target),false));
            }
        }

        public static void RequestNavigate(this IRegionManager regionManager,string regionName, string target, NavigationParameters navigationParameters)
        {
            RequestNavigate(regionManager,regionName,new Uri(target,UriKind.RelativeOrAbsolute),(nr)=>{},navigationParameters);
        }

        public static void RequestNavigate(this IRegionManager regionManager, string regionName, string target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            RequestNavigate(regionManager,regionName,new Uri(target, UriKind.RelativeOrAbsolute),navigationCallback,navigationParameters);
        }

        public static void RequestNavigate(this IRegionManager regionManager, string regionName, Uri target, NavigationParameters navigationParameters)
        {
            RequestNavigate(regionManager, regionName, target, (nr) => { }, navigationParameters);
        }

        public static void RequestNavigate(this IRegion region, Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            if (region == null)
            {
                return;
            }
            region.Context = navigationParameters;
            region.RequestNavigate(target, navigationCallback);
        }

        public static void RequestNavigate(this IRegion region, string target, NavigationParameters navigationParameters)
        {
            RequestNavigate(region,new Uri(target, UriKind.RelativeOrAbsolute),(nr)=>{},navigationParameters);
        }

        public static void RequestNavigate(this IRegion region, string target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            RequestNavigate(region,new Uri(target,UriKind.RelativeOrAbsolute),navigationCallback,navigationParameters);
        }

        public static void RequestNavigate(this IRegion region,Uri target,NavigationParameters navigationParameters)
        {
            RequestNavigate(region, target, (nr) => { }, navigationParameters);
        }

        public static NavigationParameters GetNavigationParameters(this NavigationContext context)
        {
            if (context == null)
            {
                return new NavigationParameters();
            }
            if (context.NavigationService == null || context.NavigationService.Region == null)
            {
                return new NavigationParameters();
            }
            if (context.NavigationService.Region.Context is NavigationParameters)
            {
                return context.NavigationService.Region.Context as NavigationParameters;
            }
            return new NavigationParameters();
        }
    }
}
