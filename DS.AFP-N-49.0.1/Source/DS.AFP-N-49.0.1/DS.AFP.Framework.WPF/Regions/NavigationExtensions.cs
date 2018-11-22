using DS.AFP.Framework.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// 导航扩展
    /// </summary>
    public static class NavigationExtensions
    {
        /// <summary>
        /// 请求导航
        /// </summary>
        /// <param name="regionManager">IRegionManager对象</param>
        /// <param name="regionName">RegionName</param>
        /// <param name="target">目标Uri</param>
        /// <param name="navigationCallback">回调方法</param>
        /// <param name="navigationParameters">导航传参对象</param>
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            if (regionName == null)
            {
                navigationCallback(new NavigationResult(new NavigationContext(null, target), false));
                return;
            }
            if (regionManager.Regions.ContainsRegionWithName(regionName))
            {
                regionManager.Regions[regionName].RequestNavigate(target, navigationCallback, navigationParameters);
            }
            else
            {
                navigationCallback(new NavigationResult(new NavigationContext(null, target), false));
            }
        }

        /// <summary>
        /// 请求导航
        /// </summary>
        /// <param name="regionManager">IRegionManager对象</param>
        /// <param name="regionName">RegionName</param>
        /// <param name="target">目标Uri</param>
        /// <param name="navigationParameters">导航传参对象</param>
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, string target, NavigationParameters navigationParameters)
        {
            RequestNavigate(regionManager, regionName, new Uri(target, UriKind.RelativeOrAbsolute), (nr) => { }, navigationParameters);
        }

        /// <summary>
        /// 请求导航
        /// </summary>
        /// <param name="regionManager">IRegionManager对象</param>
        /// <param name="regionName">RegionName</param>
        /// <param name="target">目标Uri</param>
        /// <param name="navigationCallback">回调方法</param>
        /// <param name="navigationParameters">导航传参对象</param>
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, string target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            RequestNavigate(regionManager, regionName, new Uri(target, UriKind.RelativeOrAbsolute), navigationCallback, navigationParameters);
        }

        /// <summary>
        /// 请求导航
        /// </summary>
        /// <param name="regionManager">IRegionManager对象</param>
        /// <param name="regionName">RegionName</param>
        /// <param name="target">目标Uri</param>
        /// <param name="navigationParameters">导航传参对象</param>
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, Uri target, NavigationParameters navigationParameters)
        {
            RequestNavigate(regionManager, regionName, target, (nr) => { }, navigationParameters);
        }

        /// <summary>
        /// 请求导航
        /// </summary>
        /// <param name="regionManager">IRegionManager对象</param>
        /// <param name="target">目标Uri</param>
        /// <param name="navigationCallback">回调方法</param>
        /// <param name="navigationParameters">导航传参对象</param>
        public static void RequestNavigate(this IRegion region, Uri target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            if (region == null)
            {
                return;
            }
            region.Context = navigationParameters;
            region.RequestNavigate(target, navigationCallback, navigationParameters);
        }

        /// <summary>
        /// 请求导航
        /// </summary>
        /// <param name="regionManager">IRegionManager对象</param>
        /// <param name="target">目标Uri</param>
        /// <param name="navigationParameters">导航传参对象</param>
        public static void RequestNavigate(this IRegion region, string target, NavigationParameters navigationParameters)
        {
            RequestNavigate(region, new Uri(target, UriKind.RelativeOrAbsolute), (nr) => { }, navigationParameters);
        }

        /// <summary>
        /// 请求导航
        /// </summary>
        /// <param name="regionManager">IRegionManager对象</param>
        /// <param name="target">目标Uri</param>
        /// <param name="navigationCallback">回调方法</param>
        /// <param name="navigationParameters">导航传参对象</param>
        public static void RequestNavigate(this IRegion region, string target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters)
        {
            RequestNavigate(region, new Uri(target, UriKind.RelativeOrAbsolute), navigationCallback, navigationParameters);
        }

        /// <summary>
        /// 请求导航
        /// </summary>
        /// <param name="regionManager">IRegionManager对象</param>
        /// <param name="target">目标Uri</param>
        /// <param name="navigationParameters">导航传参对象</param>
        public static void RequestNavigate(this IRegion region, Uri target, NavigationParameters navigationParameters)
        {
            RequestNavigate(region, target, (nr) => { }, navigationParameters);
        }

        /// <summary>
        /// 获取导航传参对象
        /// </summary>
        /// <param name="context">导航请求信息</param>
        /// <returns>导航传参对象</returns>
        //public static NavigationParameters GetNavigationParameters(this NavigationContext context)
        //{
        //    if (context == null)
        //    {
        //        return new NavigationParameters();
        //    }
        //    if (context.NavigationService == null || context.NavigationService.Region == null)
        //    {
        //        return new NavigationParameters();
        //    }
        //    if (context.NavigationService.Region.Context is NavigationParameters)
        //    {
        //        return context.NavigationService.Region.Context as NavigationParameters;
        //    }
        //    return new NavigationParameters();
        //}
    }
}
