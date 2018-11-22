using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Core;
using System.Windows.Media;
using System.Windows.Input;


namespace DS.AFP.WPF.App
{
    public class LoginResult
    {
        public bool LoginState { get; set; }
        public string LoginMessage { get; set; }
    }
    public partial class Login : Window
    {
      
        private LoginResult _loginresult;
        
        public LoginResult LoginResult
        {
            get { return this._loginresult; }
        }
       
        public Login()
        {
           
            InitializeComponent();
            this.Loaded += Login_Loaded;
        }

        protected override void OnKeyDown(System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                UIElement elementWithFocus = Keyboard.FocusedElement as UIElement;
                if (elementWithFocus != null)
                {
                    elementWithFocus.MoveFocus(request);
                }                
                e.Handled = true;
            }
            base.OnKeyDown(e);
        }

        void Login_Loaded(object sender, RoutedEventArgs e)
        {
            UserId.Focus();
        }

        private void btnCannel_Click(object sender, RoutedEventArgs e)
        {
            this._loginresult = new LoginResult { LoginState = false, LoginMessage = "取消" };
            this.Close();
            Application.Current.Shutdown();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (UserId.Text.Trim()!="")
            {
                this.Hide();
            }
            else
            {

            }
            //this.ValidateStr.Content = "";
            //try
            //{
            //    this._loginresult = new LoginResult { LoginState = true, LoginMessage = "登录成功" };
            //    //ILoginService loginService = ServiceProxyFactory.GetService<ILoginService>(ServiceContractsKeys.ILoginService);
                //LoginPassportDTO dto = new LoginPassportDTO { PersonID = UserId.Text.Trim(), Password = password.Text.Trim() };
                //LoginResultDTO result = loginService.Logon(dto);
                //switch (result.LogonResult)
                //{
                //    case LogonResult.Success:
                //        {
                //            DXSplashScreen.Show<SplashScreenWindow>();
                //            AppContext.UserState.DivisionID = result.DivisionID == null ? 0 : Int32.Parse(result.DivisionID.ToString());
                //            AppContext.UserState.DivisionName = result.DivisionName;
                //            AppContext.UserState.PersonId = result.PersonId;
                //            AppContext.UserState.PersonName = result.PersonName;
                //            AppContext.UserState.IsCenter = result.IsCenter;
                //            AppContext.UserState.SystemDate = result.CurrentTime;
                          
                            
                           
                //            //POINT pt = new POINT();
                //            //ControlsPoint.GetCursorPos(out pt);
                //            this.Visibility = Visibility.Collapsed;
                //            Application.Current.MainWindow.Height = SystemParameters.WorkArea.Height;
                //            Application.Current.MainWindow.Width = SystemParameters.WorkArea.Width;
                //            Application.Current.MainWindow.Left = SystemParameters.WorkArea.X;
                //            Application.Current.MainWindow.Top = SystemParameters.WorkArea.Y;
                //            this._regionManager.Regions[RegionNames.WindowRootArea].RequestNavigate(new Uri("Ds.Pda.Client.Views.PdaMainWindow", UriKind.Relative), nr => { });

                //            //进度显示
                //            //DXSplashScreen.Show<SplashScreenWindow>();
                //            break;
                //        }
                //    case LogonResult.PasswordInvalid:
                //        {
                //            this._loginresult = new LoginResult { LoginState = false, LoginMessage = "密码不正确" };
                //            this.ValidateStr.Content = _loginresult.LoginMessage;
                //            break;
                //        }
                //    case LogonResult.PersonIdInvalid:
                //        {
                //            this._loginresult = new LoginResult { LoginState = false, LoginMessage = "没有该用户" };
                //            this.ValidateStr.Content = _loginresult.LoginMessage;
                //            break;
                //        }
                //    default:
                //        //this.DialogResult = false;
                //        break;
                //}
            //}
            //catch
            //{
            //    //this._loginresult = new LoginResult { LoginState = false, LoginMessage = "服务异常,请重新运行系统！" };
            //    //this.ValidateStr.Content = _loginresult.LoginMessage;
              

            //}
        }

       
    }
}
