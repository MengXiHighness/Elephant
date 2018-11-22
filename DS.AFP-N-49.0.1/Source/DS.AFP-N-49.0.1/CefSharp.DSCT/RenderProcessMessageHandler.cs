﻿// Copyright © 2010-2017 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;

namespace CefSharp.DSCT
{
    public class RenderProcessMessageHandler : IRenderProcessMessageHandler
    {
        void IRenderProcessMessageHandler.OnFocusedNodeChanged(IWebBrowser browserControl, IBrowser browser, IFrame frame, IDomNode node)
        {
            var message = node == null ? "lost focus" : node.ToString();

            Console.WriteLine("OnFocusedNodeChanged() - " + message);
        }

        void IRenderProcessMessageHandler.OnContextCreated(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            const string script = "document.addEventListener('DOMContentLoaded', function(){ alert('DomLoaded'); });";

            //frame.ExecuteJavaScriptAsync(script);
        }

        void IRenderProcessMessageHandler.OnContextReleased(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            //The V8Context is about to be released, use this notification to cancel any long running tasks your might have
        }

        void IRenderProcessMessageHandler.OnUncaughtException(IWebBrowser browserControl, IBrowser browser, IFrame frame, JavascriptException exception) 
        {
            Console.WriteLine("OnUncaughtException() - " + exception.Message);
        }
    }
}