//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
using System;
using System.Globalization;
using System.Threading;
using System.Linq;
using DS.AFP.Framework;
using DS.AFP.Framework.WPF;

using Microsoft.Practices.ServiceLocation;
using DS.AFP.Common.Core;
using DS.AFP.Framework.Modularity;

namespace DS.AFP.Framework.Regions
{
   
    public static class RegionManagerExtensions
    {
        /// <summary>
        ///     Add a view to the Views collection of a Region. Note that the region must already exist in this regionmanager. 
        /// </summary>
        /// <param name="regionManager">The regionmanager that this extension method effects.</param>
        /// <param name="regionName">The name of the region to add a view to</param>
        /// <param name="view">The view to add to the views collection</param>
        /// <returns>The RegionManager, to easily add several views. </returns>
        public static IRegionManager AddToRegion(this IRegionManager regionManager, string regionName, object view)
        {
            if (regionManager == null) throw new ArgumentNullException("regionManager");

            if (!regionManager.Regions.ContainsRegionWithName(regionName))
            {
                throw new ArgumentException(
                    string.Format(Thread.CurrentThread.CurrentCulture, Resources.RegionNotFound, regionName), "regionName");
            }

            IRegion region = regionManager.Regions[regionName];

            return region.Add(view);
        }

        /// <summary>
        /// Associate a view with a region, by registering a type. When the region get's displayed
        /// this type will be resolved using the ServiceLocator into a concrete instance. The instance
        /// will be added to the Views collection of the region
        /// </summary>
        /// <param name="regionManager">The regionmanager that this extension method effects.</param>
        /// <param name="regionName">The name of the region to associate the view with.</param>
        /// <param name="viewType">The type of the view to register with the </param>
        /// <returns>The regionmanager, for adding several views easily</returns>
        public static IRegionManager RegisterViewWithRegion(this IRegionManager regionManager, string regionName, Type viewType, string viewObjectId)
        {
            var regionViewRegistry = ServiceLocator.Current.GetInstance<IRegionViewRegistry>("RegionViewRegistry");

            regionViewRegistry.RegisterViewWithRegion(regionName, viewType, viewObjectId);

            return regionManager;
        }

        /// <summary>
        /// Associate a view with a region, using a delegate to resolve a concreate instance of the view. 
        /// When the region get's displayed, this delelgate will be called and the result will be added to the
        /// views collection of the region. 
        /// </summary>
        /// <param name="regionManager">The regionmanager that this extension method effects.</param>
        /// <param name="regionName">The name of the region to associate the view with.</param>
        /// <param name="getContentDelegate">The delegate used to resolve a concreate instance of the view.</param>
        /// <returns>The regionmanager, for adding several views easily</returns>
        public static IRegionManager RegisterViewWithRegion(this IRegionManager regionManager, string regionName, Func<object> getContentDelegate)
        {
            var regionViewRegistry = ServiceLocator.Current.GetInstance<IRegionViewRegistry>();

            regionViewRegistry.RegisterViewWithRegion(regionName, getContentDelegate);

            return regionManager;
        }

        /// <summary>
        /// Adds a region to the regionmanager with the name received as argument.
        /// </summary>
        /// <param name="regionCollection">The regionmanager's collection of regions.</param>
        /// <param name="regionName">The name to be given to the region.</param>
        /// <param name="region">The region to be added to the regionmanager.</param>        
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="region"/> or <paramref name="regionCollection"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="regionName"/> and <paramref name="region"/>'s name do not match and the <paramref name="region"/> <see cref="IRegion.Name"/> is not <see langword="null"/>.</exception>
        public static void Add(this IRegionCollection regionCollection, string regionName, IRegion region)
        {
            if (region == null) throw new ArgumentNullException("region");
            if (regionCollection == null) throw new ArgumentNullException("regionCollection");

            if (region.Name != null && region.Name != regionName)
            {
                throw new ArgumentException(string.Format(CultureInfo.CurrentCulture, Resources.RegionManagerWithDifferentNameException, region.Name, regionName), "regionName");
            }

            if (region.Name == null)
            {
                region.Name = regionName;
            }

            regionCollection.Add(region);
        }

        /// <summary>
        /// Navigates the specified region manager.
        /// </summary>
        /// <param name="regionManager">The regionmanager that this extension method effects.</param>
        /// <param name="regionName">The name of the region to call Navigate on.</param>
        /// <param name="source">The URI of the content to display.</param>
        /// <param name="navigationCallback">The navigation callback.</param>
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, Uri source, Action<NavigationResult> navigationCallback)
        {
            if (regionManager == null) throw new ArgumentNullException("regionManager");
            if (navigationCallback == null) throw new ArgumentNullException("navigationCallback");

            if (regionManager.Regions.ContainsRegionWithName(regionName))
            {
                regionManager.Regions[regionName].RequestNavigate(source, navigationCallback,null);
            }
            else
            {
                navigationCallback(new NavigationResult(new NavigationContext(null, source), false));
            }
        }

        /// <summary>
        /// Navigates the specified region manager.
        /// </summary>
        /// <param name="regionManager">The regionmanager that this extension method effects.</param>
        /// <param name="regionName">The name of the region to call Navigate on.</param>
        /// <param name="source">The URI of the content to display.</param>
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, Uri source)
        {
            RequestNavigate(regionManager, regionName, source, nr => { });
        }

        /// <summary>
        /// Navigates the specified region manager.
        /// </summary>
        /// <param name="regionManager">The regionmanager that this extension method effects.</param>
        /// <param name="regionName">The name of the region to call Navigate on.</param>
        /// <param name="source">The URI of the content to display.</param>
        /// <param name="navigationCallback">The navigation callback.</param>
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, string source, Action<NavigationResult> navigationCallback)
        {
            if (source == null) throw new ArgumentNullException("source");

            RequestNavigate(regionManager, regionName, new Uri(source, UriKind.RelativeOrAbsolute), navigationCallback);
        }

        /// <summary>
        /// Navigates the specified region manager.
        /// </summary>
        /// <param name="regionManager">The regionmanager that this extension method effects.</param>
        /// <param name="regionName">The name of the region to call Navigate on.</param>
        /// <param name="source">The URI of the content to display.</param>
        public static void RequestNavigate(this IRegionManager regionManager, string regionName, string source)
        {
            RequestNavigate(regionManager, regionName, source, nr => { });
        }

        /// <summary>
        /// 注册region
        /// </summary>
        /// <param name="regionManager">region管理器</param>
        /// <param name="regionName">指定的region名</param>
        /// <param name="viewType">视图类型</param>
        /// <param name="module">插件</param>
        public static void Register(this IRegionManager regionManager, string regionName, Type viewType,IModule module)
        {
            Register(regionManager, regionName, viewType, viewType.Name,module);
        }

        /// <summary>
        /// 注册region
        /// </summary>
        /// <param name="regionManager">region管理器</param>
        /// <param name="regionName">指定的region名</param>
        /// <param name="viewType">视图类型</param>
        /// <param name="module">插件</param>
        public static void Register(this IRegionManager regionManager, string regionName, Type viewType, IModule module, bool isRegisterSingleton = true)
        {
            Register(regionManager, regionName, viewType, viewType.Name, module,true);
        }

        /// <summary>
        /// 注册region
        /// </summary>
        /// <param name="regionManager">region管理器</param>
        /// <param name="regionName">指定的region名</param>
        /// <param name="viewType">视图类型</param>
        /// <param name="viewObjectId">对象ID</param>
        /// <param name="module">插件</param>
        /// <param name="isRegisterSingleton">是否单例</param>
        /// <param name="isRegisterSingleton">是否单例直接默认构造容器时实例化</param>
        /// 
        public static void Register(this IRegionManager regionManager, string regionName, Type viewType,string viewObjectId,IModule module,bool isRegisterSingleton = true,bool isLazyInit = false)
        {
            try
            {
                module.RegisterRegionFormType(viewType.FullName.ToString(), viewType, isRegisterSingleton, isLazyInit);
                if (regionManager.Regions[regionName] != null)
                {
                    //如果是使用MEF IOC容器则需要判断是否存在view
                    // if (regionManager.Regions[regionName].Views.Count() <= 0)
                    regionManager.RegisterViewWithRegion(regionName, viewType, viewObjectId);
                }
                else
                {
                    regionManager.RegisterViewWithRegion(regionName, viewType, viewObjectId);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("RegionManage.Register(...) Abnormal ", e);
            }
        }

    
    }
}
