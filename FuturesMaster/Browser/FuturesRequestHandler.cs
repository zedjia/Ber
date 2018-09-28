using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using BerMaster.DB;
using CefSharp;
using CefSharp.OffScreen;
using Newtonsoft.Json;
using Z.Lib.Extentions;

namespace BerMaster.Browser
{
    public class FuturesRequestHandler : IRequestHandler 
    {
        public string Prefix { get; private set; }

        private FuturesResponseFilter filter = null;
        public event Action<List<TradeDepthsEntity>, tradeInfo> NotifyOrderData;
        public Action<tradeInfo> SetUpdateLabel;
        public Action<string> LogPrint;

        //public event Action CallbackWhenNoData;
        private CustomWebBrowser _currentBrowser;

        public FuturesRequestHandler(CustomWebBrowser browser,string prefix)
        {
            _currentBrowser = browser;
            Prefix = prefix;
        }
        
        public IResponseFilter GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame,
            IRequest request, IResponse response)
        {
            var url = new Uri(request.Url);
            return FilterManager.CreateOrGetFilter(request.Identifier.ToString());
        }
        
        public bool OnResourceResponse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            return false;
        }
        
        /// <summary>
        /// 1、4为买入，2、3为卖出
        /// 1:买多、3平多
        /// 2:买空、4平空
        /// </summary>
        private TradeDepthsEntity[] _lastdata;
        void filter_NotifyOrderData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return;
            }

            //_catchDataStatus = true;//有数据的时候就设置为得到数据状态
            _currentBrowser.LastCatchDataTime=DateTime.Now;
            List<TradeDepthsEntity> diff = new List<TradeDepthsEntity>();
            try
            {
                //var order = JsonConvert.DeserializeObject<BTMOrder>(data);
                var trade = JsonConvert.DeserializeObject<tradeInfo>(data);
                SetUpdateLabel?.Invoke(trade);
                if (!trade.tradeDepths.Any())
                {
                    LogPrint?.Invoke("没有抓取到数据");
                }
                if (_lastdata != null) 
                {
                    diff =
                        trade.tradeDepths.Except(_lastdata, Equality<TradeDepthsEntity>.CreateComparer(p => p.id))
                            .ToList();
                }
                else
                {
                    diff = trade.tradeDepths.ToList();
                }
                _lastdata = trade.tradeDepths;

                NotifyOrderData?.Invoke(diff,trade);
            }
            catch (Exception e)
            {
                LogPrint?.Invoke(string.Format("{0}-{1}", "filter_NotifyOrderData", e.Message));
                LoggerFactory.GetLog().Error("filter_NotifyOrderData 出错！", e);
            }
        }




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



        public bool OnBeforeBrowse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request,
            bool isRedirect)
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
            if (request.Url.Contains("future/refreshFutureFulLPub.do"))
            {
                var filter = FilterManager.CreateOrGetFilter(request.Identifier.ToString()) as FuturesResponseFilter;
                    string str = System.Text.Encoding.Default.GetString(filter.dataAll.ToArray());
                filter_NotifyOrderData(str);
                //filter.NotifyData(filter.dataAll.ToArray());
            }
        }

        public void OnResourceRedirect(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, ref string newUrl)
        {
        }

        
    }
}
