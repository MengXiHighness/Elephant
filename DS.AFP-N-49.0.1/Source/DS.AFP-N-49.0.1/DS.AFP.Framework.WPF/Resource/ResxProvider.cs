// -----------------------------------------------------------------------
// <copyright file="ResxProvider.cs" company="">
// 
// </copyright>
// -----------------------------------------------------------------------

namespace ElpRoom.Localization
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Resources;
    using System.Reflection;
    using System.Globalization;
    using DS.AFP.Framework.WPF;
    using System.Threading;

    /// <summary>
    /// use resx file storage multi language
    /// </summary>
    public class ResxProvider : IResourceProvider
    {
        /// <summary>
        /// Cached resource managers
        /// </summary>
        private static Dictionary<string, WeakReference> _resourceManagers = new Dictionary<string, WeakReference>();

        /// <summary>
        /// The resource manager to use for this extension.  Holding a strong reference to the
        /// Resource Manager keeps it in the cache while ever there are ResxExtensions that
        /// are using it.
        /// </summary>
        private ResourceManager _resourceManager;
        private string resxName;
        private string key;

        public object GetResource(string resxName, string key)
        {
            this.resxName = resxName;
            this.key = key;

            object resource = null;
            if (_resourceManager == null)
            {
                _resourceManager = GetResourceManager(resxName);
            }
            if (_resourceManager != null)
            {
                //CultureInfo culture;

                //if (Thread.CurrentThread.CurrentUICulture == "zh-cn")
                //{
                //    culture = new CultureInfo("zh-CN");
                //}
                //else
                //{
                //    culture = new CultureInfo("en");
                //}
                resource = _resourceManager.GetObject(key, Thread.CurrentThread.CurrentUICulture);
            }
            return resource;
        }

        /// <summary>
        /// Get the resource manager for this type
        /// </summary>
        /// <param name="resxName">The name of the embedded resx</param>
        /// <returns>The resource manager</returns>
        /// <remarks>Caches resource managers to improve performance</remarks>
        private ResourceManager GetResourceManager(string resxName)
        {
            WeakReference reference = null;
            ResourceManager result = null;
            if (_resourceManagers.TryGetValue(resxName, out reference))
            {
                result = reference.Target as ResourceManager;

                // if the resource manager has been garbage collected then remove the cache
                // entry (it will be readded)
                //
                if (result == null)
                {
                    _resourceManagers.Remove(resxName);
                }
            }

            if (result == null)
            {
                Assembly assembly = FindResourceAssembly();
                if (assembly != null)
                {
                    result = new ResourceManager(resxName, assembly);
                }
                _resourceManagers.Add(resxName, new WeakReference(result));
            }
            return result;
        }

        /// <summary>
        /// Find the assembly that contains the type
        /// </summary>
        /// <returns>The assembly if loaded (otherwise null)</returns>
        private Assembly FindResourceAssembly()
        {
            Assembly assembly = Assembly.GetEntryAssembly();

            // check the entry assembly first - this will short circuit a lot of searching
            //
            if (assembly != null && HasEmbeddedResx(assembly, resxName)) return assembly;

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (Assembly searchAssembly in assemblies)
            {
                // skip system assemblies
                //
                string name = searchAssembly.FullName;
                if (!name.StartsWith("Microsoft.") &&
                    !name.StartsWith("System.") &&
                    !name.StartsWith("System,") &&
                    !name.StartsWith("mscorlib,") &&
                    !name.StartsWith("PresentationFramework,") &&
                    !name.StartsWith("WindowsBase,"))
                {
                    if (HasEmbeddedResx(searchAssembly, resxName)) return searchAssembly;
                }
            }
            return null;
        }

        /// <summary>
        /// Check if the assembly contains an embedded resx of the given name
        /// </summary>
        /// <param name="assembly">The assembly to check</param>
        /// <param name="resxName">The name of the resource we are looking for</param>
        /// <returns>True if the assembly contains the resource</returns>
        private bool HasEmbeddedResx(Assembly assembly, string resxName)
        {
            try
            {
                string[] resources = assembly.GetManifestResourceNames();
                string searchName = resxName.ToLower() + ".resources";
                foreach (string resource in resources)
                {
                    if (resource.ToLower() == searchName) return true;
                }
            }
            catch
            {
                // GetManifestResourceNames throws an exception for some
                // dynamic assemblies - just ignore these assemblies.
            }
            return false;
        }
    }
}
