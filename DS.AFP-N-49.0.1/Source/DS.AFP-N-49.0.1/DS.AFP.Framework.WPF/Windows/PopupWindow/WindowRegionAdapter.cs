using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

using System.Collections.Specialized;
using System.Windows.Controls;
using DS.AFP.Framework.WPF;

namespace DS.AFP.Framework.Regions.Behaviors
{
    public class WindowRegionAdapter : RegionAdapterBase<Window>
    {
        protected override void Adapt(IRegion region, Window regionTarget)
        {
        }

        private readonly IRegionViewRegistry regionViewRegistry;
        public WindowRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
            this.regionViewRegistry = regionViewRegistry;
        }
        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }

        protected override void AttachBehaviors(IRegion region, Window regionTarget)
        {
            base.AttachBehaviors(region, regionTarget);

            WindowRegionBehavior behavior = new WindowRegionBehavior(regionTarget, region, WindowStyle);
            behavior.Attach();
        }

        public Style WindowStyle { get; set; }

        private class WindowRegionBehavior
        {
            private readonly WeakReference _ownerWeakReference;
            private readonly WeakReference _regionWeakReference;
            private readonly Style _windowStyle;

            internal WindowRegionBehavior(Window owner, IRegion region, Style windowStyle)
            {
                _ownerWeakReference = new WeakReference(owner);
                _regionWeakReference = new WeakReference(region);
                _windowStyle = windowStyle;
            }

            internal void Attach()
            {
                IRegion region = _regionWeakReference.Target as IRegion;

                if (region != null)
                {
                    region.Views.CollectionChanged += new NotifyCollectionChangedEventHandler(Views_CollectionChanged);
                    region.ActiveViews.CollectionChanged += new NotifyCollectionChangedEventHandler(ActiveViews_CollectionChanged);
                }
            }

            internal void Detach()
            {
                IRegion region = _regionWeakReference.Target as IRegion;

                if (region != null)
                {
                    region.Views.CollectionChanged -= Views_CollectionChanged;
                    region.ActiveViews.CollectionChanged -= ActiveViews_CollectionChanged;
                }
            }

            private void window_Activated(object sender, EventArgs e)
            {
                IRegion region = _regionWeakReference.Target as IRegion;
                Window window = sender as Window;

                if (window != null && window.Content!=null && !region.ActiveViews.Contains(window.Content) )
                    region.Activate(window.Content);
            }

            private void window_Deactivated(object sender, EventArgs e)
            {
                IRegion region = _regionWeakReference.Target as IRegion;
                Window window = sender as Window;

                if (window != null)
                    region.Deactivate(window.Content);
            }

            private void window_Closed(object sender, EventArgs e)
            {
                Window window = sender as Window;
                IRegion region = _regionWeakReference.Target as IRegion;
                UserControlBase userbase = window.Content as UserControlBase;
                if (userbase != null)
                    userbase.Close();
                if (window != null && region != null)
                    if (region.Views.Contains(window.Content))
                        region.Remove(window.Content);
            }


            private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                Window owner = _ownerWeakReference.Target as Window;

                if (owner == null)
                {
                    Detach();
                    return;
                }

                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (object view in e.NewItems)
                    {
                        Window window = GetContainerWindow(owner, view);

                        if (window != null && !window.IsFocused)
                        {
                            window.WindowState = WindowState.Normal;
                            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            window.Activate();
                        }
                    }
                }
            }

            private void Views_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
            {
                Window owner = _ownerWeakReference.Target as Window;

                if (owner == null)
                {
                    Detach();
                    return;
                }

                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    foreach (object view in e.NewItems)
                    {
                        UserControlBase content = view as UserControlBase;
                        if (content != null)
                        {
                            Window window = new Window();
                            window.Activated += new EventHandler(window_Activated);
                            window.Deactivated += new EventHandler(window_Deactivated);
                            window.Style = _windowStyle;

                            if (content != null)
                            {
                                window.Title = content.Title;
                                window.Height = (Double.IsNaN(content.Height) ? content.ActualHeight + 50 : content.Height + 50);
                                window.Width = (Double.IsNaN(content.Width) ? content.ActualWidth + 30 : content.Width + 30);
                                content.InitEvent(window.Close);
                            }
                            window.Content = view;
                            window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                            window.Closed += new EventHandler(window_Closed);
                            window.Owner = owner;
                            window.Show();
                        }
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    foreach (object view in e.OldItems)
                    {
                        Window window = GetContainerWindow(owner, view);

                        if (window != null)
                            window.Close();
                    }
                }
            }

            private Window GetContainerWindow(Window owner, object view)
            {
                foreach (Window window in owner.OwnedWindows)
                {
                    if (window.Content == view)
                        return window;
                }

                return null;
            }
        }
    }
}
