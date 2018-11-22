
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Resources;
using System.Reflection;
using System.Windows;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using System.Windows.Interop;
using System.Runtime.InteropServices;

namespace DS.AFP.Framework.WPF
{

   
    [MarkupExtensionReturnType(typeof(XmlLanguage))]
    public class UICultureExtension : ManagedMarkupExtension
    {
        
        private static MarkupExtensionManager _markupManager = new MarkupExtensionManager(2);

       
        public UICultureExtension()
            : base(_markupManager)
        {
        }

        
        protected override object GetValue()
        {
            return XmlLanguage.GetLanguage(CultureManager.UICulture.IetfLanguageTag);
        }

        /// <summary>
        /// Return the MarkupManager for this extension
        /// </summary>
        public static MarkupExtensionManager MarkupManager
        {
            get { return _markupManager; }
        }

        /// <summary>
        /// Use the Markup Manager to update all targets
        /// </summary>
        public static void UpdateAllTargets()
        {
            _markupManager.UpdateAllTargets();
        }

    }

}
