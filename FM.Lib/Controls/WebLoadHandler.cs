using Xilium.CefGlue;

namespace FM.Lib.Controls
{
    internal sealed class WebLoadHandler : CefLoadHandler
    {
        private readonly WebBrowser _core;

        public WebLoadHandler(WebBrowser core)
        {
            _core = core;
        }

        protected override void OnLoadingStateChange(CefBrowser browser, bool isLoading, bool canGoBack, bool canGoForward)
        {
            _core.OnLoadingStateChanged(isLoading, canGoBack, canGoForward);
        }
    }
}
