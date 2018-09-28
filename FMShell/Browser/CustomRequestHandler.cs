using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using Newtonsoft.Json;

namespace WC.Browser
{
    public class CustomRequestHandler : IRequestHandler
    {
        public bool GetAuthCredentials(IWebBrowser browserControl, IBrowser browser, IFrame frame, bool isProxy, string host, int port, string realm, string scheme, IAuthCallback callback)
        {
            return false;
            
        }

        public bool OnSelectClientCertificate(IWebBrowser browserControl, IBrowser browser, bool isProxy, string host, int port, X509Certificate2Collection certificates, ISelectClientCertificateCallback callback)
        {
            return true;
        }

        public void OnResourceRedirect(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, ref string newUrl)
        {
        }

        public IResponseFilter GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            //if (response.MimeType != "text/html")
            //{
            //    var url = new Uri(request.Url);
            //    string refUrl = request.ReferrerUrl;
            //    if (string.IsNullOrEmpty(refUrl))
            //    {
            //        refUrl = request.Url;
            //    }
            //    var hostUrl = new Uri(refUrl);
            //    ResourceEntity Entity = new ResourceEntity()
            //    {
            //        Identifier=request.Identifier,
            //        FileName = url.Segments.LastOrDefault(),
            //        SiteUrl = hostUrl.Host,
            //        MimeType = response.MimeType,
            //        FileSize = Convert.ToInt64(response.ResponseHeaders["Content-Length"])
            //    };
            //    if (response.MimeType.Contains("javascript"))
            //    {
            //        return new CustomJsResponseFilter(Entity);
            //    }
            //    if (response.MimeType.Contains("css"))
            //    {
            //        return new CustomCssResponseFilter(Entity);
            //    }
            //    if (response.MimeType.Contains("image"))
            //    {
            //        return new CustomImageResponseFilter(Entity);
            //    }
            //}
            var url = new Uri(request.Url);
            if (url.AbsoluteUri.Contains("/trades?"))
            {
                this.filter = new CustomResponseFilter();
                filter.NotifyData += filter_NotifyTradeData;

                return filter;
            }
            else if (url.AbsoluteUri.Contains("/order?"))
            {
                this.filter = new CustomResponseFilter();
                filter.NotifyData += filter_NotifyOrderData;

                return filter;
            }
            return null;


            //return new CustomResponseFilter();
        }


        private CustomResponseFilter filter = null;
        public event Action<string> NotifyOrderData;
        public event Action<string> NotifyTradeData;


        public bool OnResourceResponse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            //NOTE: You cannot modify the response, only the request  
            // You can now access the headers  
            //var headers = response.ResponseHeaders;  
            //try
            //{
            //    var content_length = int.Parse(response.ResponseHeaders["Content-Length"]);
            //    if (this.filter != null)
            //    {
            //        this.filter.SetContentLength(content_length);
            //    }
            //}
            //catch { }
            return false;
        }

        void filter_NotifyTradeData(string data)
        {
            //var trad= JsonConvert.DeserializeObject<Trades>(data);
            if (NotifyTradeData != null)
            {
                NotifyTradeData(data);
            }
        }

        void filter_NotifyOrderData(string data)
        {

            //var order = JsonConvert.DeserializeObject<BTMOrder>(data);

            if (NotifyOrderData != null)
            {
                NotifyOrderData(data);
            }
        }



        public bool OnBeforeBrowse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, bool isRedirect)
        {
            //request.Headers.Add("","");
            return false;
        }

        public CefReturnValue OnBeforeResourceLoad(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IRequestCallback callback)
        {
            return CefReturnValue.Continue;
        }

        public bool OnCertificateError(IWebBrowser browserControl, IBrowser browser, CefErrorCode errorCode, string requestUrl, ISslInfo sslInfo, IRequestCallback callback)
        {
            return false;
        }

        public bool OnOpenUrlFromTab(IWebBrowser browserControl, IBrowser browser, IFrame frame, string targetUrl, WindowOpenDisposition targetDisposition, bool userGesture)
        {
            return false;
        }

        public void OnPluginCrashed(IWebBrowser browserControl, IBrowser browser, string pluginPath)
        {
        }

        public bool OnProtocolExecution(IWebBrowser browserControl, IBrowser browser, string url)
        {
            return false;
        }

        public bool OnQuotaRequest(IWebBrowser browserControl, IBrowser browser, string originUrl, long newSize, IRequestCallback callback)
        {
            return false;
        }

        public void OnRenderProcessTerminated(IWebBrowser browserControl, IBrowser browser, CefTerminationStatus status)
        {
        }

        public void OnRenderViewReady(IWebBrowser browserControl, IBrowser browser)
        {
        }

        public void OnResourceLoadComplete(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response, UrlRequestStatus status, long receivedContentLength)
        {
            
        }

        public void OnResourceRedirect(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, ref string newUrl)
        {
        }

        
    }
}
