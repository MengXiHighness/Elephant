//
//      FILE:   ResxExtension.cs.
//
// COPYRIGHT:   Copyright 2008 
//              Infralution
//
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Windows.Interop;
using System.IO;
using System.Runtime.InteropServices;

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "DS.AFP.Framework.WPF")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2007/xaml/presentation", "DS.AFP.Framework.WPF")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2008/xaml/presentation", "DS.AFP.Framework.WPF")]
namespace DS.AFP.Framework.WPF
{
   
    public delegate object GetResourceHandler(string resxName, string key, CultureInfo culture);

   
    [MarkupExtensionReturnType(typeof(object))]
    public class ResExtension : ManagedMarkupExtension
    {

        #region Member Variables

        /// <summary>
        /// The type name that the resource is associated with
        /// </summary>
        private string _resxName;

        /// <summary>
        /// The key used to retrieve the resource
        /// </summary>
        private string _key;

        /// <summary>
        /// The default value for the property
        /// </summary>
        private string _defaultValue;

        /// <summary>
        /// Cached resource managers
        /// </summary>
        private static Dictionary<string, WeakReference> _resourceManagers = new Dictionary<string, WeakReference>();

        /// <summary>
        /// The manager for resx extensions
        /// </summary>
        private static MarkupExtensionManager _markupManager = new MarkupExtensionManager(40);


        #endregion

        #region Public Interface

        
        public ResExtension()
            : base(_markupManager)
        {
        }

       
        public ResExtension(string resxName, string key, string defaultValue)
            : base(_markupManager)
        {
            this._resxName = resxName;
            this._key = key;
            if (!string.IsNullOrEmpty(defaultValue))
            {
                this._defaultValue = defaultValue;
            }
        }


      
        public string Node
        {
            get { return _resxName; }
            set { _resxName = value; }
        }



       
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }

        
        public string DefaultValue
        {
            get { return _defaultValue; }
            set { _defaultValue = value; }
        }

       
        public static MarkupExtensionManager MarkupManager
        {
            get { return _markupManager; }
        }

       
        public static void UpdateAllTargets()
        {
            _markupManager.UpdateAllTargets();
        }

       
        public static void UpdateTarget(string key)
        {
            foreach (ResExtension target in _markupManager.ActiveExtensions)
            {
                if (target.Key == key)
                {
                    target.UpdateTarget();
                }
            }
        }

        #endregion

        #region Local Methods



        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

       
        private object ConvertValue(object value)
        {
            object result = null;
            BitmapSource bitmapSource = null;

            
            if (value is Icon)
            {
                Icon icon = value as Icon;

                
                using (MemoryStream iconStream = new MemoryStream())
                {
                    icon.Save(iconStream);
                    iconStream.Seek(0, SeekOrigin.Begin);
                    bitmapSource = BitmapFrame.Create(iconStream);
                }
            }
            else if (value is Bitmap)
            {
                Bitmap bitmap = value as Bitmap;
                IntPtr bitmapHandle = bitmap.GetHbitmap();
                bitmapSource = Imaging.CreateBitmapSourceFromHBitmap(
                    bitmapHandle, IntPtr.Zero, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
                bitmapSource.Freeze();
                DeleteObject(bitmapHandle);
            }

            if (bitmapSource != null)
            {
               
                if (TargetPropertyType == typeof(object))
                {
                    System.Windows.Controls.Image imageControl = new System.Windows.Controls.Image();
                    imageControl.Source = bitmapSource;
                    imageControl.Width = bitmapSource.Width;
                    imageControl.Height = bitmapSource.Height;
                    result = imageControl;
                }
                else
                {
                    result = bitmapSource;
                }
            }
            else
            {
                result = value;

               
                Type targetType = TargetPropertyType;
                if (value is String && targetType != typeof(String) && targetType != typeof(object))
                {
                    TypeConverter tc = TypeDescriptor.GetConverter(targetType);
                    result = tc.ConvertFromInvariantString(value as string);
                }
            }

            return result;
        }

        
        private object GetDefaultValue(string key)
        {
            object result = _defaultValue;
            Type targetType = TargetPropertyType;
            if (_defaultValue == null)
            {
                if (targetType == typeof(String) || targetType == typeof(object))
                {
                    result = "#" + key;
                }
            }
            else if (targetType != null)
            {
                
                if (targetType != typeof(String) && targetType != typeof(object))
                {
                    try
                    {
                        TypeConverter tc = TypeDescriptor.GetConverter(targetType);
                        result = tc.ConvertFromInvariantString(_defaultValue);
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }

        
        protected override object RetriveValue()
        {
            if (string.IsNullOrEmpty(Node))
                throw new ArgumentException("ResxName cannot be null");
            if (string.IsNullOrEmpty(Key))
                throw new ArgumentException("Key cannot be null");

            object result = null;

            try
            {
                object resource = null;
                resource = ResManager.Instance.GetResource(Node, Key);
                result = ConvertValue(resource);
            }
            catch (Exception ex)
            {
                if (this.IsInDesignMode)
                {
                    throw ex;
                }
                Console.WriteLine("ResxExtension RetriveValue failed:{0}", ex.Message);
            }

            if (result == null)
            {
                result = GetDefaultValue(Key);
            }
            return result;
        }

        #endregion

    }

}
