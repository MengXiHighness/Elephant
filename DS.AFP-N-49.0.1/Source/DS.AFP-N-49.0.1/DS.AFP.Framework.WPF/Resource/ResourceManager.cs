

namespace DS.AFP.Framework.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Globalization;
    using System.Threading;
    using DS.AFP.Common.Core;
    using System.IO;
    using global::Common.Logging;

    public class ResManager
    {
        public ILog Logger
        {
            get
            {
                return LogManager.GetLogger("ResManager");
            }
        }

        private static ResManager resManager;

        public static ResManager Instance
        {
            get
            {
                if (resManager == null)
                    resManager = new ResManager();
                return resManager;
            }
        }

        public IResourceProvider ResProvider { get; set; }

        public object GetResource(string nodeName, string key)
        {
            try
            {
                string returnPath = this.GetResourceByTheme(nodeName, key);
                if (!returnPath.IsNullOrEmpty())
                {
                    return returnPath;
                }
                if (ResProvider == null)
                {
                    ResProvider = new XmlProvider();
                }
                if (ResProvider != null)
                {
                    object resValue = ResProvider.GetResource(nodeName, key);
                    if (resValue != null)
                    {
                        return resValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return null;
        }

        private string GetResourceByTheme(string nodeName, string key)
        {
            try
            {
                string theme = ThemeManage.CurrentTheme;
                string culture = CultureManager.UICulture.Name;
                string path1 = "{0}{1}\\{2}\\{3}\\{4}".FormatString(PathHelper.GetRootPath(), "Addin", nodeName, "Resources\\Images", key);
                if (File.Exists(path1))
                {
                    return path1;
                }

                string path2 = "{0}{1}\\{2}\\{3}\\{4}\\Images\\{5}".FormatString(PathHelper.GetRootPath(), "Addin", nodeName, "Resources\\Themes", ThemeManage.CurrentTheme, key);
                if (File.Exists(path2))
                {
                    return path2;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            return string.Empty;
        }




    }
}
