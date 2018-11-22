using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CefSharp.Wpf.EX
{
    public class KeyBoardHander : IKeyboardHandler
    {
        public bool OnKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
        {
            return false;
        }

        public bool OnPreKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            const int VK_F5 = 0x74;
            if (windowsKeyCode == VK_F5 && nativeKeyCode!= 0)
            {
                browser.Reload(); //此处可以添加想要实现的代码段
                //var chromiumWebBrowser = (ChromiumWebBrowser)browserControl;
                //chromiumWebBrowser.
                //chromiumWebBrowser.Load(browser.MainFrame.Url);
            }
            return false;
        }
    }
}
