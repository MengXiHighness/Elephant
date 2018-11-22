
using DS.AFP.Framework.Events;
using DS.AFP.Framework.Regions;
using DS.AFP.Framework.Regions.Behaviors;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// Defines a behavior that creates a Dialog to display the active view of the target <see cref="IRegion"/>.
    /// </summary>
    public abstract class DialogActivationBehavior : RegionBehavior, IHostAwareRegionBehavior
    {
        /// <summary>
        /// The key of this behavior
        /// </summary>
        public const string BehaviorKey = "DialogActivation";

        private IWindow contentDialog;

        /// <summary>
        /// Gets or sets the <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// </summary>
        /// <value>A <see cref="DependencyObject"/> that the <see cref="IRegion"/> is attached to.
        /// This is usually a <see cref="FrameworkElement"/> that is part of the tree.</value>
        public DependencyObject HostControl { get; set; }

        /// <summary>
        /// Performs the logic after the behavior has been attached.
        /// </summary>
        protected override void OnAttach()
        {
            this.Region.ActiveViews.CollectionChanged += this.ActiveViews_CollectionChanged;
        }

        /// <summary>
        /// Override this method to create an instance of the <see cref="IWindow"/> that 
        /// will be shown when a view is activated.
        /// </summary>
        /// <returns>
        /// An instance of <see cref="IWindow"/> that will be shown when a 
        /// view is activated on the target <see cref="IRegion"/>.
        /// </returns>
        protected abstract IWindow CreateWindow();

        private void ActiveViews_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                this.CloseContentDialog();
                this.PrepareContentDialog(e.NewItems[0]);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                this.CloseContentDialog();
            }
        }

        private Style GetStyleForView()
        {
            return this.HostControl.GetValue(RegionPopupBehaviors.ContainerWindowStyleProperty) as Style;
        }

        private void PrepareContentDialog(object view)
        {
            UserControlBase userbase = new UserControlBase();
            userbase=view as UserControlBase;
            this.contentDialog = this.CreateWindow();
            this.contentDialog.Title = userbase.Title;
            this.contentDialog.Content = view;
            this.contentDialog.Owner = this.HostControl;
            this.contentDialog.Closed += this.ContentDialogClosed;
            this.contentDialog.Style = this.GetStyleForView();
            UserControl user = view as UserControl;
            this.contentDialog.Width = user.Width;
            this.contentDialog.Height = user.Height;
            // Here we raise the event to show the overlay
            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<OverlayEvent>().Publish(true);
            
            this.contentDialog.Show();
            //µ¯³ö´°¿ÚÊ±ÉèÖÃ±³¾°»Ò°µ
            
        }

        private void CloseContentDialog()
        {
            if (this.contentDialog != null)
            {
                this.contentDialog.Closed -= this.ContentDialogClosed;
                this.contentDialog.Close();
                this.contentDialog.Content = null;
                this.contentDialog.Owner = null;
                this.contentDialog.Width = 0;
                this.contentDialog.Height = 0;
                // Here we raise the event to hide the overlay
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<OverlayEvent>().Publish(false);

                //¹Ø±Õµ¯³ö´°¿ÚÊ±È¥³ý±³¾°»Ò°µ

            }
        }

        private void ContentDialogClosed(object sender, System.EventArgs e)
        {
            this.Region.Deactivate(this.contentDialog.Content);
            this.CloseContentDialog();
        }
    }
}
