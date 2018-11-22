using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DS.AFP.Common.Core;
using System.Collections.Concurrent;
using Common.Logging;

namespace DS.AFP.Common.Core
{
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

        public object GetResource(string resxName, string key)
        {
            object result = null;
            try
            {
                //culture从配置文件中读取
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
                        _resourceManagers.TryRemove(hash, out temp_reference);
                    }
                }
                if (result == null)
                {
                    var arr = resxName.Split('_');
                    if (arr != null && arr.Length != 0)
                    {
                        if (arr[0].ToLower().EndsWith("setup"))
                        {
                            result = GetRes(ComponentType.Setup, resxName, key, culture);
                            _resourceManagers.TryAdd(hash, new WeakReference(result));
                        }
                    }
                }
                if (result == null)
                {
                    result = GetRes(ComponentType.Addin, resxName, key, culture);
                    _resourceManagers.TryAdd(hash, new WeakReference(result));
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return result;
        }

        private object GetRes(ComponentType ct, string resxName, string key, string cultureName)
        {

            object obj = null;
            //if (string.IsNullOrEmpty(resxName))
            //    return obj;
            var arr = resxName.Split('_');

            //string path = "{0}{1}\\{2}\\{3}\\{4}\\{5}\\{6}".FormatString(PathHelper.GetRootPath(), "Addin", arr[0], "Resources\\Themes", "DeepBlue", "Culture", cultureName);

            string path = GetPath(ct, resxName, cultureName);
            string xml = "";
            if (arr.Length > 0)
            {
                xml = Path.Combine(path, "Resource.xml");
            }
            if (!File.Exists(xml))
            {
                throw new FileNotFoundException(string.Format("Resources {0} File does not exist!", xml));
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
                    obj = imgfile;
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return obj;
        }


        private string GetPath(ComponentType ct, string resxName, string cultureName)
        {
            switch (ct)
            {
                case ComponentType.Addin:
                    {
                        object obj = null;
                        if (string.IsNullOrEmpty(resxName))
                            return "";
                        var arr = resxName.Split('_');

                        string path = "{0}{1}\\{2}\\{3}\\{4}\\{5}\\{6}".FormatString(PathHelper.GetRootPath(), "Addin", arr[0], "Resources\\Themes", "DeepBlue", "Culture", cultureName);
                        return path;
                        break;
                    }
                case ComponentType.Host:
                    {
                        object obj = null;
                        if (string.IsNullOrEmpty(resxName))
                            return "";

                        string path = "{0}{1}\\{2}\\{3}\\{4}".FormatString(PathHelper.GetRootPath(), "Resources\\Themes", "DeepBlue", "Culture", cultureName);
                        return path;
                        break;
                    }
                case ComponentType.Setup:
                    {
                        object obj = null;
                        if (string.IsNullOrEmpty(resxName))
                            return "";

                        string path = "{0}{1}\\{2}\\{3}\\{4}".FormatString(PathHelper.GetRootPath(), "Resources\\Themes", "DeepBlue", "Culture", cultureName);
                        return path;
                        break;
                    }
                default:
                    return "";
            }
        }


        public object GetHostResource(string nodeName, string key)
        {
            string culture = CultureManager.UICulture.Name;

            WeakReference reference = null;
            object result = null;
            var hash = nodeName + key + culture;
            if (_resourceManagers.TryGetValue(hash, out reference))
            {
                result = reference.Target as object;
                if (result == null)
                {
                    WeakReference temp_reference = null;
                    _resourceManagers.TryRemove(hash, out temp_reference);
                }
            }

            if (result == null)
            {
                result = GetRes(ComponentType.Host, nodeName, key, culture);
                _resourceManagers.TryAdd(hash, new WeakReference(result));
            }
            return result;
        }

        public enum ComponentType
        {
            Addin = 0, Host = 1, Setup = 2
        }
    }

}
