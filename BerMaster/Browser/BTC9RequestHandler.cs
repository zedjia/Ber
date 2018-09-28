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

namespace BerMaster.Browser
{
    public class BTC9RequestHandler : IRequestHandler 
    {
        public string Prefix { get; private set; }
        public string Bid { get; private set; }

        private CustomResponseFilter filter = null;
        public event Action<string, OrderEntity, List<OrderDetailEntity>> NotifyOrderData;
        public event Action<string> NotifyTradeData;
        public Action<DateTime> SetUpdateLabel;


        //public event Action CallbackWhenNoData;
        private bool _catchDataStatus;
        private CustomWebBrowser _currentBrowser;

        public BTC9RequestHandler(CustomWebBrowser browser,string prefix,string bid)
        {
            _currentBrowser = browser;
            Prefix = prefix;
            Bid = bid;
            //Task.Run(checkCatchStatus);
        }

        /// <summary>
        /// 定时检测是否数据抓取正常，没不正常则强制刷新
        /// </summary>
        /// <returns></returns>
        private async Task checkCatchStatus()
        {
            await Task.Delay(60000);

            while (true)
            {
                _catchDataStatus = false;
                await Task.Delay(30000);
                if (!_catchDataStatus && _currentBrowser != null) //第一次的时候可能会多刷新一次
                {
                    LoggerFactory.GetLog().Error(string.Format(" {0} 正在尝试重新刷新.  ", Prefix));
                    _currentBrowser.Reload(true);
                }
            }

        }



        public IResponseFilter GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            var url = new Uri(request.Url);
            //if (url.AbsoluteUri.Contains("/trades?"))
            //{
            //    this.filter = new CustomResponseFilter();
            //    filter.NotifyData += filter_NotifyTradeData;

            //    return filter;
            //}
            if (url.AbsoluteUri.Contains("/trade_"+ Bid))
            {
                this.filter = new CustomResponseFilter();
                filter.NotifyData += filter_NotifyOrderData;

                return filter;
            }
            return null;
        }


        


        public bool OnResourceResponse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            return false;
        }

        void filter_NotifyTradeData(string data)
        {
            //_catchDataStatus = true;//有数据的时候就设置为得到数据状态

            //var trad= JsonConvert.DeserializeObject<Trades>(data);
            if (NotifyTradeData != null)
            {
                NotifyTradeData(data);
            }
        }


        private OrderDetailEntity _lastdata;

        void filter_NotifyOrderData(string data)
        {
            //_catchDataStatus = true;//有数据的时候就设置为得到数据状态
            _currentBrowser.LastCatchDataTime = DateTime.Now;
            SetUpdateLabel?.Invoke(DateTime.Now);
            try
            {
                //var order = JsonConvert.DeserializeObject<BTMOrder>(data);
                //var order = JsonConvert.DeserializeObject(data);
                var entity = JsonConvert.DeserializeObject<Btc9Entity>(data);
                OrderEntity order = new OrderEntity()
                {
                    max = entity.cmark.max_price,
                    min = entity.cmark.min_price,
                    sum = entity.cmark.H24_done_money,
                    volume = entity.cmark.H24_done_num,
                    data = data,
                    sourcesite = "btc9"
                };

                List<OrderDetailEntity> list = new List<OrderDetailEntity>();
                foreach (var d in entity.ctrade)
                {
                    OrderDetailEntity item = new OrderDetailEntity();
                    item.price = d.price;
                    item.amount = d.amount;
                    item.money = d.money;
                    System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1));
                        // 当地时区
                    item.time = startTime.AddSeconds(d.add_time);
                    item.type = d.type.Contains("red");
                    item.sourcesite = "btc9";

                    list.Add(item);
                }
                if (_lastdata == null) //下面代码是在一定程度上解决数据重复的问题.
                {
                    _lastdata = list.FirstOrDefault(i => i.time == list.Max(a => a.time));
                }
                else
                {
                    var _index = list.FindIndex(
                        i =>
                            i.time == _lastdata.time && i.price == _lastdata.price && i.type == _lastdata.type &&
                            i.amount == _lastdata.amount);
                    if (_index == 0)
                    {
                        return;

                    }
                    else if (_index >= 1)
                    {
                        list.RemoveRange(_index, list.Count - _index);
                    }
                    _lastdata = _lastdata = list.FirstOrDefault(i => i.time == list.Max(a => a.time));
                }

                NotifyOrderData?.Invoke(Prefix, order, list);


            }
            catch (Exception e)
            {
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

    public class cmark
    {
        public decimal H24_change { get; set; }
        public decimal H24_done_money { get; set; }
        public decimal H24_done_num { get; set; }
        public decimal H24_money { get; set; }
        public string currency_id { get; set; }
        public string currency_mark { get; set; }
        public string currency_name { get; set; }
        public decimal max_price { get; set; }
        public decimal min_price { get; set; }
        public decimal new_price { get; set; }

    }

    public class ctrade
    {
        public double amount { get; set; }
        public double money { get; set; }
        public double price { get; set; }
        public string num { get; set; }
        public string time { get; set; }
        public long add_time { get; set; }
        public string type { get; set; }

    }

    public class depth
    {
        public decimal amount { get; set; }
        public decimal num { get; set; }
        public decimal price { get; set; }
        public decimal sum { get; set; }
        public decimal total { get; set; }
        public decimal trade_num { get; set; }
        public string name { get; set; }
        public string type { get; set; }

    }

    public class Btc9Entity
    {
        public cmark cmark { get; set; }
        public ctrade[] ctrade { get; set; }

        //public ctrade 1 { get; set; }  //todo:这里要报错，要研究下怎么解析这个

    }

}
