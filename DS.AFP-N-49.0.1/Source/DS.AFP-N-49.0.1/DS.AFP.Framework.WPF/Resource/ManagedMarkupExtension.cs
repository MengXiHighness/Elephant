
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Reflection;
using System.Windows;
using System.ComponentModel;
using System.Globalization;
using System.Diagnostics;

[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2006/xaml/presentation", "DS.AFP.Framework.WPF")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2007/xaml/presentation", "DS.AFP.Framework.WPF")]
[assembly: XmlnsDefinition("http://schemas.microsoft.com/winfx/2008/xaml/presentation", "DS.AFP.Framework.WPF")]
namespace DS.AFP.Framework.WPF
{
    
    public abstract class ManagedMarkupExtension : MarkupExtension
    {

        #region Member Variables

        
        private List<WeakReference> _targetObjects = new List<WeakReference>();

        
        private object _targetProperty;

        #endregion

        #region Public Interface

      
        public ManagedMarkupExtension(MarkupExtensionManager manager)
        {
            manager.RegisterExtension(this);
        }

       
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var targetHelper = serviceProvider.GetService(typeof(IProvideValueTarget)) as IProvideValueTarget;
            if (targetHelper.TargetObject != null)
            {
                _targetProperty = targetHelper.TargetProperty;

                if (targetHelper.TargetObject is DependencyObject || !(_targetProperty is DependencyProperty))
                {
                    _targetObjects.Add(new WeakReference(targetHelper.TargetObject));
                    return RetriveValue();
                }
                else
                {
                    
                    return this;
                }
            }
            return null;
        }

       
        public void UpdateTarget()
        {
            if (_targetProperty != null)
            {
                foreach (WeakReference reference in _targetObjects)
                {
                    if (_targetProperty is DependencyProperty)
                    {
                        DependencyObject target = reference.Target as DependencyObject;
                        if (target != null)
                        {
                            target.SetValue(_targetProperty as DependencyProperty, RetriveValue());
                        }
                    }
                    else if (_targetProperty is PropertyInfo)
                    {
                        object target = reference.Target;
                        if (target != null)
                        {
                            (_targetProperty as PropertyInfo).SetValue(target, RetriveValue(), null);
                        }
                    }
                }
            }
        }

       
        public bool IsTargetAlive
        {
            get
            {
               
                if (_targetObjects.Count == 0)
                    return true;

                
                foreach (WeakReference reference in _targetObjects)
                {
                    if (reference.IsAlive) return true;
                }
                return false;
            }
        }

       
        public bool IsInDesignMode
        {
            get
            {
                foreach (WeakReference reference in _targetObjects)
                {
                    DependencyObject element = reference.Target as DependencyObject;
                    if (element != null && DesignerProperties.GetIsInDesignMode(element)) return true;
                }
                return false;
            }
        }

        #endregion

        #region Protected Methods

       
        protected object TargetProperty
        {
            get { return _targetProperty as DependencyProperty; }
        }

        
        protected Type TargetPropertyType
        {
            get
            {
                Type propertyType = null;
                if (_targetProperty is DependencyProperty)
                    propertyType = (_targetProperty as DependencyProperty).PropertyType;
                else if (_targetProperty is PropertyInfo)
                    propertyType = (_targetProperty as PropertyInfo).PropertyType;
                return propertyType;
            }
        }

        
        protected abstract object RetriveValue();

        #endregion

    }
}
