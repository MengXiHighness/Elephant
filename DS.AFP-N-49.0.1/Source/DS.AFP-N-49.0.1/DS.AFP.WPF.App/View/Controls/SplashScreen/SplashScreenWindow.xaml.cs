using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using DS.AFP.WPF.App.View.Controls.SplashScreen;
using System.Reflection;
using System.Windows.Threading;
using System.Threading;
using System.Windows.Media.Animation;
using System.Windows.Navigation;

namespace DS.AFP.WPF.App
{
    /// <summary>
    /// Interaction logic for SplashScreenWindow.xaml
    /// </summary>
    public partial class SplashScreenWindow : Window, ILoadEventRegist
    {


        private bool _allowDirectNavigation = false;
        private NavigatingCancelEventArgs _navArgs = null;

        public SplashScreenWindow()
        {
            InitializeComponent();

           

            this.SplashPage.Navigating += SplashPage_Navigating;
        }

        void SplashPage_Navigating(object sender, System.Windows.Navigation.NavigatingCancelEventArgs e)
        {
            if (!_allowDirectNavigation)
            {
                e.Cancel = true;
                _navArgs = e;
                this.SplashPage.IsHitTestVisible = false;
                DoubleAnimation da = new DoubleAnimation(0.3d, new Duration(TimeSpan.FromMilliseconds(300)));
                da.Completed += FadeOutCompleted;
                this.SplashPage.BeginAnimation(OpacityProperty, da);
            }
            _allowDirectNavigation = false;
        }

        private void FadeOutCompleted(object sender, EventArgs e)
        {
            (sender as AnimationClock).Completed -= FadeOutCompleted;

            _allowDirectNavigation = true;
            this.IsHitTestVisible = true;

            SplashPage.NavigationService.Navigate(_navArgs.Content);

            SplashPage.Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                (ThreadStart)delegate()
            {
                DoubleAnimation da = new DoubleAnimation(1.0d, new Duration(TimeSpan.FromMilliseconds(200)));
                SplashPage.BeginAnimation(OpacityProperty, da);
            });
        }

        public void Init(Framework.Events.IEventAggregator EventAggregator)
        {
            EventAggregator.GetEvent<LoadModuleEvent>().Subscribe(o =>
            {
                if (o.ModuleInfo.SplashType != null)
                {
                    Page page = System.Activator.CreateInstance(o.ModuleInfo.SplashType) as Page;
                    this.SplashPage.Dispatcher.Invoke(new Action(() =>
                    {
                        SplashPage.NavigationService.Navigate(page);

                    }));
                    this.Info.Dispatcher.Invoke(new Action(() =>
                    {
                        Info.Text = string.Format("正在加载{0}...", o.ModuleInfo.ModuleName);

                    }));

                }
            });
        }
    }
}
