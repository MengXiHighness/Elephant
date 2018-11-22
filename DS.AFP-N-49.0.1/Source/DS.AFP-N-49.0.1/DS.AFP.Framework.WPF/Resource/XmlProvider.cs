

namespace DS.AFP.Framework.WPF
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.IO;
    using System.Xml.Linq;
    using System.Windows;
    using System.ComponentModel;
    using System.Threading;
    using DS.AFP.Common.Core;
    using System.Windows.Media.Imaging;
    using global::Common.Logging;
    using System.Collections.Concurrent;

    public class XmlProvider : IResourceProvider
    {
        public ILog Logger
        {
            get
            {
                return LogManager.GetLogger("ResManager");
            }
        }
        /// <summary>
        /// Cached resource managers
        /// </summary>
        private static ConcurrentDictionary<string, WeakReference> _resourceManagers = new ConcurrentDictionary<string, WeakReference>();
        public static ConcurrentDictionary<string, XDocument> XDocumentS = new ConcurrentDictionary<string, XDocument>();

        public XmlProvider()
        {

        }

        //IShell shell = Container.GetObject("IShell") as IShell;

        public object GetResource(string resxName, string key)
        {
            object result = null;
            try
            {
                //if (this.IsInDesignMode)
                //{
                //    return GetRes(resxName, key, theme);
                //}
                string culture = CultureManager.UICulture.Name;
                // string cultureName = Thread.CurrentThread.CurrentUICulture.Name;

                WeakReference reference = null;

                var hash = resxName + key + culture;
                if (_resourceManagers.TryGetValue(hash, out reference))
                {
                    result = reference.Target as object;
                    if (result == null)
                    {
                        WeakReference temp_reference = null;
                        _resourceManagers.TryRemove(hash,out temp_reference);
                    }
                }

                if (result == null)
                {
                    result = GetRes(resxName, key, culture);
                    _resourceManagers.TryAdd(hash, new WeakReference(result));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return result;
        }

        private object GetRes(string resxName, string key, string cultureName)
        {

            object obj = null;
            if (string.IsNullOrEmpty(resxName))
                return obj;
            var arr = resxName.Split('_');

            string path = "{0}{1}\\{2}\\{3}\\{4}\\{5}\\{6}".FormatString(PathHelper.GetRootPath(), "Addin", arr[0], "Resources\\Themes", ThemeManage.CurrentTheme, "Culture", cultureName);
            string xml = "";
            if (arr.Length > 0)
            {
                xml = Path.Combine(path, "Resource.xml");
            }
            if (!File.Exists(xml))
            {
                throw new FileNotFoundException(string.Format("Resource {0} file does not exist!", xml));
            }

            string path_key = xml.ToLower();
            XDocument doc;
            lock (XDocumentS)
            {
                if (!XDocumentS.TryGetValue(path_key, out doc))
                {
                    doc = XDocument.Load(xml);
                    XDocumentS.TryAdd(path_key, doc);
                }
            }


            //XDocument doc = XDocument.Load(xml);

            XElement item = null;

            try
            {
                if (arr.Length == 1)
                {//如果resxName 中有一级.在根目录
                    item = doc.Element("res").Elements("item")
                         .Where((r) => string.Equals(r.Attribute("key").Value, key, StringComparison.CurrentCultureIgnoreCase)).Single();

                }
                else if (arr.Length == 2)
                {
                    var nodeName = arr[1];
                    var node = doc.Element("res").Elements("node")
                         .Where((n) => n.Attribute("name").Value == nodeName).Single();

                    item = node.Elements("item")
                        .Where((r) => string.Equals(r.Attribute("key").Value, key)).Single();

                }

                if (string.Equals(item.Attribute("type").Value, "string", StringComparison.CurrentCultureIgnoreCase))
                {
                    obj = item.Attribute("value").Value;
                }
                else if (string.Equals(item.Attribute("type").Value, "image", StringComparison.CurrentCultureIgnoreCase))
                {
                    string value = item.Attribute("value").Value;
                    var imgfile = path + value;
                    //obj = new System.Drawing.Bitmap(imgfile, false);

                    //BitmapImage bitmaptemp = new BitmapImage(new Uri(imgfile, UriKind.Relative));
                    //obj = new System.Drawing.Bitmap(bitmaptemp.StreamSource, false);
                    //obj = new BitmapImage(new Uri(imgfile, UriKind.Relative));
                    obj = imgfile;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Format("不能读取资源:{0},检查Node:{1} 和Key:{2}", xml, resxName, key);
                throw new Exception(msg, ex);
            }
            return obj;
        }

    }
}
