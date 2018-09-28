using Xilium.CefGlue;

namespace FM.Lib.Controls
{
    class WebRequestHandler : CefRequestHandler
    {
        protected override bool OnBeforeBrowse(CefBrowser browser, CefFrame frame, CefRequest request, bool isRedirect)
        {
            DemoApp.BrowserMessageRouter.OnBeforeBrowse(browser, frame);
            return base.OnBeforeBrowse(browser, frame, request, isRedirect);
        }

        protected override void OnRenderProcessTerminated(CefBrowser browser, CefTerminationStatus status)
        {
            DemoApp.BrowserMessageRouter.OnRenderProcessTerminated(browser);
        }
    }
}
