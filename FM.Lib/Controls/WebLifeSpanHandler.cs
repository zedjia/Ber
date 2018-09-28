using Xilium.CefGlue;

namespace FM.Lib.Controls
{
    internal sealed class WebLifeSpanHandler : CefLifeSpanHandler
    {
        private readonly WebBrowser _core;

        public WebLifeSpanHandler(WebBrowser core)
        {
            _core = core;
        }

        protected override void OnAfterCreated(CefBrowser browser)
        {
            base.OnAfterCreated(browser);

            _core.OnCreated(browser);
        }

        protected override bool DoClose(CefBrowser browser)
        {
            // TODO: dispose core
            return false;
        }

        protected override void OnBeforeClose(CefBrowser browser)
        {
            DemoApp.BrowserMessageRouter.OnBeforeClose(browser);
        }
    }
}
