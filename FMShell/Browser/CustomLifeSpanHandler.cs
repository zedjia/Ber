﻿using CefSharp;

namespace WC.Browser
{
    public class CustomLifeSpanHandler : ILifeSpanHandler
    {
        public bool DoClose(IWebBrowser browserControl, IBrowser browser)
        {
            return false;
        }

        public void OnAfterCreated(IWebBrowser browserControl, IBrowser browser)
        {
            return;
        }

        public void OnBeforeClose(IWebBrowser browserControl, IBrowser browser)
        {
            return;
        }


        public bool OnBeforePopup(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, string targetFrameName, WindowOpenDisposition targetDisposition, bool userGesture, IPopupFeatures popupFeatures, IWindowInfo windowInfo, IBrowserSettings browserSettings, ref bool noJavascriptAccess, out IWebBrowser newBrowser)
        {
            frame.LoadUrl(targetUrl);
            newBrowser = null;
            return true;
        }
    }
}
