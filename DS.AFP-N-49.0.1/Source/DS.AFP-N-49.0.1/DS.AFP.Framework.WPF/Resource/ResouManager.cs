
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Windows;
using System.Windows.Markup;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using DS.AFP.Common.Core;
using Microsoft.Practices.ServiceLocation;
using DS.AFP.Common.Core.ConfigurationNameSpace;

namespace DS.AFP.Framework.WPF
{

    public class ThemeInfo
    {
        private CultureInfo culture;

        public ThemeInfo()
        {
            culture = new CultureInfo("zh-cn");
        }

        /// <summary>
        /// Theme名称.即Theme文件夹下第一级目录名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 当前使用的Theme Culture.默认是zh-cn
        /// </summary>
        public CultureInfo Culture { get; set; }

        
    }

    public  class ThemeManager
    {
        public static event EventHandler ThemeChanged;

        private static ThemeInfo currentTheme;
        private static IList<ThemeInfo> supportedThemes;
        private static readonly Dictionary<string, ResourceDictionary> cachedResourceDictionaries = new Dictionary<string, ResourceDictionary>();
        public static IList<ThemeInfo> SupportedThemes
        {
            get { return supportedThemes; }
            set { supportedThemes = value; }
        }

        public static ThemeInfo CurrentTheme
        {
            get { return currentTheme; }
            set
            {
                if (value != currentTheme)
                {
                    if (value.Culture != currentTheme.Culture)
                    {
                        Thread.CurrentThread.CurrentUICulture = value.Culture;
                        SetThreadCulture(value.Culture);
                    }
                    currentTheme = value;

                }
            }

        }
        private static readonly string[] defaultReferencesNamesForApplication = new string[]
        {
            
           
        };
        

        private static string themedir;

        public static IList<ThemeInfo> SupportedThemes
        {
            get
            {
                if (supportedThemes == null)
                {
                    supportedThemes = new List<ThemeInfo>();
                    IDsConfigurationSection dsConfig = ServiceLocator.Current.GetInstance<IDsConfigurationSection>();

                    IList<ThemeElement> server = (from ThemeElement me in dsConfig.Themes
                                                  select me).ToList<ThemeElement>();
                    foreach(ThemeElement te in server)
                    {
                        supportedThemes.Add(new ThemeInfo() { 
                            Culture = new CultureInfo(te.Culture),
                            Name = te.Name
                        });
                    }
                }
                return supportedThemes;
            }
        }

        private static string ThemeFolder
        {
            get
            {
                if (string.IsNullOrEmpty(themedir))
                {
                    //var exedir =  Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                    var exedir = Path.GetDirectoryName(PathHelper.GetRootPath());
                    themedir = Path.Combine(exedir, "Resources");
                    themedir = Path.Combine(themedir, "Themes");                   
                }
                return themedir;
            }
        }



        /// <summary>
        /// 为主题风格加载资源
        /// </summary>
        /// <param name="themeName">主题对象</param>
        public static void LoadResourcesByTheme(ThemeInfo theme)
        {
            
            //
            // 总是包含默认资源
            string[] resourcesPaths = new string[1] { 
                ThemeFolder+"{0}/{1}/{2}".FormatString(theme.Name,theme.Culture,"Theme.xaml")
            };
            resourcesPaths = resourcesPaths.Union(defaultReferencesNamesForApplication).ToArray();

            Action resetApplicationResources = () =>
            {
                Application.Current.Resources.MergedDictionaries.Clear();

                // add new resources
                foreach (string resourcePath in resourcesPaths)
                {
                    //var uriStringToAdd = "/{0};component/Resources/Themes/{1}/{2}".FormatString("DS.AFP.WPF.App", themeName, xamlFile);

                    AddDictionaryToApplicationResources(resourcePath);
                }

            };
            // retry to reset resources several times (hides the exception with "Collection was modified")
            int retries = 0;

            while (retries < 5)
            {
                try
                {
                    LoadResourcesByTheme(theme);
                    break;
                }
                catch
                {
                    retries++;
                }
            }

            CurrentTheme = theme;
        }

        private static void AddDictionaryToApplicationResources(string uriStringToAdd)
        {
            ResourceDictionary resourceDictionary = null;

            // load ResourceDictionary from cache, if exists there, otherwise try to create it and add it to cache
            if (cachedResourceDictionaries.ContainsKey(uriStringToAdd))
            {
                resourceDictionary = cachedResourceDictionaries[uriStringToAdd];
            }
            else
            {
                try
                {
                    var uri = new Uri(uriStringToAdd, UriKind.RelativeOrAbsolute);

                    resourceDictionary = new ResourceDictionary() { Source = uri };
                    cachedResourceDictionaries.Add(uriStringToAdd, resourceDictionary);
                }
                catch
                {
                    resourceDictionary = null;
                    cachedResourceDictionaries.Add(uriStringToAdd, resourceDictionary);
                }
            }

            if (resourceDictionary != null)
            {
                Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
            }
        }

        private static void SetThreadCulture(CultureInfo value)
        {
            if (value.IsNeutralCulture)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(value.Name);
            }
            else
            {
                Thread.CurrentThread.CurrentCulture = value;
            }
        }

     
    }

}
