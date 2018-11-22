
using System;
using System.Windows;

namespace DS.AFP.Framework.WPF
{
    /// <summary>
    /// Defines the interface for the Dialogs that are shown by <see cref="DialogActivationBehavior"/>.
    /// </summary>
    public interface IWindow
    {
        /// <summary>
        /// Gets or sets the content for the title
        /// </summary>
        //string Title { get;set;}

        /// <summary>
        /// Ocurrs when the <see cref="IWindow"/> is closed.
        /// </summary>
        event EventHandler Closed;

        /// <summary>
        /// Gets or sets the content for the <see cref="IWindow"/>.
        /// </summary>
        object Content { get; set; }

        /// <summary>
        /// 窗口宽度
        /// </summary>
        double Width { get; set; }

        /// <summary>
        /// 窗口高度
        /// </summary>
        double Height { get; set; }

        /// <summary>
        /// 窗口标题
        /// </summary>
        string Title{ set;}

        /// <summary>
        /// Gets or sets the owner control of the <see cref="IWindow"/>.
        /// </summary>
        object Owner { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="System.Windows.Style"/> to apply to the <see cref="IWindow"/>.
        /// </summary>
        Style Style { get; set; }

        /// <summary>
        /// Opens the <see cref="IWindow"/>.
        /// </summary>
        void Show();

        /// <summary>
        /// Closes the <see cref="IWindow"/>.
        /// </summary>
        void Close();
    }
}