using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace TransCoinMaster.Lib
{
    public class CustomRenderProcessMessageHandler : IRenderProcessMessageHandler
    {
        public void OnContextCreated(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            frame.ExecuteJavaScriptAsync(@"(function() {
                if (window)
                {
                    delete window.WebSocket;
                    if ('WebSocket' in window) {
                        window.WebSocket = undefined;
                    }
                    console.log('WebSocket support disabled');
                }
            })(); ");
        }

        public void OnContextReleased(IWebBrowser browserControl, IBrowser browser, IFrame frame)
        {
            //throw new NotImplementedException();
        }

        public void OnFocusedNodeChanged(IWebBrowser browserControl, IBrowser browser, IFrame frame, IDomNode node)
        {
            //throw new NotImplementedException();
        }
    }
}
