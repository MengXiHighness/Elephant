
using System;
using System.Windows;
using System.Windows.Controls;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// Defines a wrapper for the <see cref="Window"/> class that implements the <see cref="IWindow"/> interface.
    /// </summary>
    public class WindowWrapper : Window,IWindow
    {
        private readonly Window window;

        /// <summary>
        /// Initializes a new instance of <see cref="WindowWrapper"/>.
        /// </summary>
        public WindowWrapper()
        {
            this.window = new PopuWin();
        }

        /// <summary>
        /// Ocurrs when the <see cref="Window"/> is closed.
        /// </summary>
        public event EventHandler Closed
        {
            add { this.window.Closed += value; }
            remove { this.window.Closed -= value; }
        }


        /// <summary>
        /// Gets or Sets the content for the <see cref="Window"/>.
        /// </summary>
        public object Content
        {
            get
            {
                ContentControl contrl = this.window.FindName("DS_AFP_UserContent") as ContentControl;
                if (contrl != null)
                    return contrl.Content;
                else
                    throw new NullReferenceException("The form has not been able to find the Name = DS_AFP_UserContent ContentControl controls");
            }
            set
            {
                ContentControl contrl = this.window.FindName("DS_AFP_UserContent") as ContentControl;
                contrl.Content = value;
            }
        }

        /// <summary>
        /// 设置窗口标题
        /// </summary>
        public string Title
        {
            set
            {
                TextBlock contrl = this.window.FindName("DS_AFP_Title") as TextBlock;
                if(contrl!=null)
                    contrl.Text = value;
            }

        }
        /// <summary>
        /// 设置窗口宽为填充用户控件宽度
        /// </summary>
        public double Width
        {
            get {
                return this.window.Width;
            }
            set
            {
                this.window.Width = value;
            }
        }
        /// <summary>
        /// 设置窗口高为填充用户控件高度
        /// </summary>
        public double Height
        {
            get
            {
                return this.window.Height;
            }
            set
            {
                this.window.Height=value;
            }
        }


        /// <summary>
        /// Gets or Sets the <see cref="Window.Owner"/> control of the <see cref="Window"/>.
        /// </summary>
        public object Owner
        {
            get { return this.window.Owner; }
            set { this.window.Owner = value as Window; }
        }

        /// <summary>
        /// Gets or Sets the <see cref="FrameworkElement.Style"/> to apply to the <see cref="Window"/>.
        /// </summary>
        public Style Style
        {
            get { return this.window.Style; }
            set { this.window.Style = value; }
        }

        /// <summary>
        /// Opens the <see cref="Window"/>.
        /// </summary>
        public void Show()
        {
            this.window.ShowDialog();
        }

        /// <summary>
        /// Closes the <see cref="Window"/>.
        /// </summary>
        public void Close()
        {
            this.window.Close();
        }
    }
}