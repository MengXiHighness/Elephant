using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using DS.AFP.Common.Core;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// 主题风格管理器
    /// </summary>
    public class ThemeManage
    {
        static ThemeManage()
        {
            currentTheme = "DeepBlue";
        }

        public event EventHandler ThemeChanged;
        private static readonly Dictionary<string, ResourceDictionary> cachedResourceDictionaries = new Dictionary<string, ResourceDictionary>();
        private static readonly string[] allThemes = new string[]
        {
            "DeepBlue",
            "Gray"
        };
        private static readonly string[] defaultReferencesNamesForApplication = new string[]
        {
            "DS.AFP.UI"
        };

        private static  string currentTheme;

        public static  string CurrentTheme
        {
            get
            {
                return currentTheme;
            }
            set
            {
                currentTheme = value;
            }
        }

        /// <summary>
        /// 获取所有主题风格
        /// </summary>
        /// <returns></returns>
        public static string[] GetAllThemes()
        {
            return allThemes;
        }

        /// <summary>
        /// 为主题风格加载资源
        /// </summary>
        /// <param name="themeName">主题名</param>
        /// <param name="resourcesPaths">主题所在文件路径集合</param>
        public void EnsureResourcesForTheme(string themeName, string HostName, string[] resourcesPaths = null)
        {
            if (resourcesPaths == null)
            {
                resourcesPaths = new string[] { };
            }

            // always include default resources
            resourcesPaths = resourcesPaths.Union(defaultReferencesNamesForApplication).ToArray();

            // temporarily save QSF's resources
            var telerikThemeDictionaries = from keyValuePair in cachedResourceDictionaries
                                           where keyValuePair.Key.Contains("DS.AFP.Themes.")
                                           select keyValuePair.Value;
            var qsfOnlyDictionaries = Application.Current.Resources.MergedDictionaries.Except(telerikThemeDictionaries).ToList();

            Action resetApplicationResources = () =>
            {
                Application.Current.Resources.MergedDictionaries.Clear();

                // add new resources
                foreach (string resourcePath in resourcesPaths)
                {
                    //var xamlFile = resourcePath.Split(',')[0].ToLower(CultureInfo.InvariantCulture) + ".xaml";
                    //var uriStringToAdd = "/Telerik.Windows.Themes." + themeName + ";component/themes/" + xamlFile;
                    var xamlFile = "Theme.xaml";
                    var uriStringToAdd = "/{0};component/Resources/Themes/{1}/{2}".FormatString(HostName, themeName, xamlFile);
                    // this call may cause "Collection was modified" exception
                    AddDictionaryToApplicationResources(uriStringToAdd);
                }

                //add back QSF's resources
                if (qsfOnlyDictionaries != null && qsfOnlyDictionaries.Count() > 0)
                {
                    foreach (var resourceDictionary in qsfOnlyDictionaries)
                    {
                        Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
                    }
                }
            };

            // retry to reset resources several times (hides the exception with "Collection was modified")
            int retries = 0;

            while (retries < 5)
            {
                try
                {
                    resetApplicationResources();
                    break;
                }
                catch
                {
                    retries++;
                }
            }
            currentTheme = themeName;

            OnThemeChanged();
        }

        /// <summary>
        /// 为换肤加载应用程序资源
        /// </summary>
        /// <param name="themeName"></param>
        public static void EnsureApplicationResources(string themeName, string HostName)
        {
            var resourcesPaths = new string[] { };

            // always include default resources
            resourcesPaths = defaultReferencesNamesForApplication.ToArray();

            // temporarily save QSF's resources
            var telerikThemeDictionaries = from keyValuePair in cachedResourceDictionaries
                                           where keyValuePair.Key.Contains("DS.AFP.Themes.")
                                           select keyValuePair.Value;
            var qsfOnlyDictionaries = Application.Current.Resources.MergedDictionaries.Except(telerikThemeDictionaries).ToList();

            //Application.Current.Resources.MergedDictionaries.Clear();

            // add new resources
            foreach (string resourcePath in resourcesPaths)
            {
                var xamlFile = "Theme.xaml";
                var uriStringToAdd = "/{0};component/Resources/Themes/{1}/{2}".FormatString(HostName, themeName, xamlFile);

                var uri = new Uri(uriStringToAdd, UriKind.RelativeOrAbsolute);
                try
                {
                    var resourceDictionary = new ResourceDictionary() { Source = uri };
                    Application.Current.Resources.MergedDictionaries.Add(resourceDictionary);
                }
                catch (Exception e)
                {
                }
            }

            currentTheme = themeName;

            

        }


        /// <summary>
        /// 为FrameworkElement提供主题风格
        /// </summary>
        /// <param name="element">FrameworkElement元素</param>
        /// <param name="themeName">主题名</param>
        public static void EnsureFrameworkElementResourcesForTheme(FrameworkElement element, string themeName, string HostName)
        {
            var resourcesPaths = new string[] { };

            // always include default resources
            resourcesPaths = defaultReferencesNamesForApplication.ToArray();

            // temporarily save QSF's resources
            var telerikThemeDictionaries = from keyValuePair in cachedResourceDictionaries
                                           where keyValuePair.Key.Contains("DS.AFP.Themes.")
                                           select keyValuePair.Value;
            var qsfOnlyDictionaries = Application.Current.Resources.MergedDictionaries.Except(telerikThemeDictionaries).ToList();

            element.Resources.MergedDictionaries.Clear();

            // add new resources
            foreach (string resourcePath in resourcesPaths)
            {
                var xamlFile = "Theme.xaml";
                var uriStringToAdd = "/{0};component/Resources/Themes/{1}/{2}".FormatString(HostName, themeName, xamlFile);
                
                var uri = new Uri(uriStringToAdd, UriKind.RelativeOrAbsolute);
                var resourceDictionary = new ResourceDictionary() { Source = uri };
                element.Resources.MergedDictionaries.Add(resourceDictionary);
            }
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

        private void OnThemeChanged()
        {
            if (ThemeChanged != null)
            {
                this.ThemeChanged(this, EventArgs.Empty);
            }
        }
    }
}
