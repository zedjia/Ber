﻿using System;
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
    public class FuturesRequestHandler : IRequestHandler 
    {
        public string Prefix { get; private set; }

        private FuturesResponseFilter filter = null;
        public event Action<string, OrderEntity, List<OrderDetailEntity>> NotifyOrderData;
        public event Action<string> NotifyTradeData;
        public Action<DateTime> SetUpdateLabel;

        //public event Action CallbackWhenNoData;
        private bool _catchDataStatus;
        private CustomWebBrowser _currentBrowser;

        public FuturesRequestHandler(CustomWebBrowser browser,string prefix)
        {
            _currentBrowser = browser;
            Prefix = prefix;

            //Task.Run(checkCatchStatus);
        }
        
        public IResponseFilter GetResourceResponseFilter(IWebBrowser browserControl, IBrowser browser, IFrame frame,
            IRequest request, IResponse response)
        {
            var url = new Uri(request.Url);
            var filter = FilterManager.CreateFilter(request.Identifier.ToString());
            return filter;
            //this.filter = new FuturesResponseFilter();
            //filter.NotifyData += filter_NotifyOrderData;
        }
        
        public bool OnResourceResponse(IWebBrowser browserControl, IBrowser browser, IFrame frame, IRequest request, IResponse response)
        {
            return false;
        }
        

        private OrderDetailEntity _lastdata;
        void filter_NotifyOrderData(string data)
        {
            return;
            //_catchDataStatus = true;//有数据的时候就设置为得到数据状态
            _currentBrowser.LastCatchDataTime=DateTime.Now;
            
            SetUpdateLabel?.Invoke(DateTime.Now);

            try
            {
                //var order = JsonConvert.DeserializeObject<BTMOrder>(data);
                var order = JsonConvert.DeserializeObject<OrderEntity>(data);
                order.sourcesite = "jubi";
                order.data = JsonConvert.SerializeObject(order.d);
                List<OrderDetailEntity> list = new List<OrderDetailEntity>();
                foreach (var d in order.d)
                {
                    OrderDetailEntity item = new OrderDetailEntity();
                    double p, am;
                    double.TryParse(d[1], out p);
                    item.price = p;
                    double.TryParse(d[2], out am);
                    item.amount = am;
                    item.money = p * am;
                    DateTime dt;
                    DateTime.TryParse(d[4] + " " + d[0], out dt);
                    item.time = dt;
                    item.type = d[3] == "buy";
                    item.sourcesite = "jubi";

                    list.Add(item);
                }
                //var groupList= list.GroupBy(i => new {i.type, i.time}).Select(i => i).ToList();
                //list.Clear();
                //foreach (var orderDetailEntities in groupList)///分组合并同时间大资金分批的问题.
                //{
                //    OrderDetailEntity item = new OrderDetailEntity();

                //    item.amount = orderDetailEntities.Sum(i => i.amount);
                //    item.money = orderDetailEntities.Sum(i => i.money);
                //    item.time = orderDetailEntities.Key.time;
                //    item.price = item.type
                //        ? orderDetailEntities.Min(i => i.price)
                //        : orderDetailEntities.Max(i => i.price);
                //    item.type = orderDetailEntities.Key.type;
                //    list.Add(item);
                //}
                if (_lastdata == null)//下面代码是在一定程度上解决数据重复的问题.
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
                    else if (_index>=1)
                    {
                        list.RemoveRange(_index, list.Count - _index);
                    }
                    _lastdata = _lastdata = list.FirstOrDefault(i => i.time == list.Max(a => a.time));
                }
                //NotifyOrderData?.Invoke(Prefix, order, list);
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
                var filter = FilterManager.GetFileter(request.Identifier.ToString()) as FuturesResponseFilter;
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