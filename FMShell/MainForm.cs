using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Ber.DB;
using CefSharp;
using CefSharp.OffScreen;
using Newtonsoft.Json;
using WC.Browser;
using WC.Lib;
using WC.Lib.Controls;


namespace Ber
{
    public partial class MainForm : ClientBaseForm
    {
        public ChromiumWebBrowser browser;
        
       protected ConcurrentDictionary<string,IFrame> TaskList=new ConcurrentDictionary<string, IFrame>();
       protected ConcurrentQueue<string> TaskQueue = new ConcurrentQueue<string>();
       protected ConcurrentQueue<string> TaskDoneQueue = new ConcurrentQueue<string>();

        


        /*
         var setting = new CefSharp.CefSettings();  
        setting.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/50.0.2661.102 Safari/537.36";  
        CefSharp.Cef.Initialize(setting, true, false);  
             */



        public MainForm()
        {
            try
            {
                InitializeComponent();
                //InitBrowser();
                InitOffScreenBrowser();
                //this.Opacity = 0;
                //this.ShowInTaskbar = false;
                //this.panel1.Controls.Add(browser);
                //browser.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Console.WriteLine("MainForm_Load");
            timer1.Start();
            Refreshtimer.Start();
            
        }


        #region browser


        void InitOffScreenBrowser()
        {
            var settings = new CefSettings()
            {
                //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")
            };

            //Perform dependency check to make sure all relevant resources are in our output directory.
            Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);


            // Create the offscreen Chromium browser.
            browser = new ChromiumWebBrowser(CurrentConfig.SystemUrl);
            var requestHandler = new CustomRequestHandler();
            requestHandler.NotifyOrderData += RequestHandler_NotifyOrderData;
            browser.RequestHandler = requestHandler;
            // An event that is fired when the first page is finished loading.
            // This returns to us from another thread.
            //browser.LoadingStateChanged += BrowserLoadingStateChanged;
        }


        void InitBrowser()
        {
            var setting = new CefSettings();
            setting.Locale = "zh-CN";
            //Cef.Initialize(setting);

            //browser = new ChromiumWebBrowser("http://localhost");
            string _url = CurrentConfig.SystemUrl;
            browser = new ChromiumWebBrowser(_url);
            //browser.FrameLoadStart += browser_FrameLoadStart;
            //browser.FrameLoadEnd += browser_FrameLoadEnd;
            //browser.LifeSpanHandler = new CustomLifeSpanHandler();
            var requestHandler= new CustomRequestHandler();
            requestHandler.NotifyOrderData += RequestHandler_NotifyOrderData;
            browser.RequestHandler = requestHandler;
            //browser.RegisterJsObject("WCShell", new JsEventFunction(this));
            //browser.MenuHandler=new MenuHandler();
            
            
        }
        
        /// <summary>
        /// 获取到订单数据的回调函数
        /// </summary>
        /// <param name="data"></param>
        private void RequestHandler_NotifyOrderData(string data)
        {
            try
            {


                var order = JsonConvert.DeserializeObject<BTMOrder>(data);
                order.data = JsonConvert.SerializeObject(order.d);
                List<BTMOrdersDetails> list = new List<BTMOrdersDetails>();
                foreach (var d in order.d)
                {
                    BTMOrdersDetails item = new BTMOrdersDetails();
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
                    list.Add(item);
                }

                DbOperation(order, list);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
            }
        }
        //async Task
        async Task DbOperation(BTMOrder order, List<BTMOrdersDetails> list)
        {
            using (BerMasterEntities db = new BerMasterEntities())
            {
                db.BTMOrder.Add(order);
                //db.SaveChanges();
                db.BTMOrdersDetails.AddRange(list);
                if (await db.SaveChangesAsync() <= 0)
                {
                    MessageBox.Show(this, "保存失败了.");
                }

            }
            refreshSign = true;
        }

        private bool refreshSign;
        /// <summary>
        /// 当30秒没有数据进来的时候，就刷新页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refreshtimer_Tick(object sender, EventArgs e)
        {
            if (!refreshSign)
            {
                browser.Reload(true);
            }
            refreshSign = false;
        }


        private bool showwaiteform=true;
        private bool closewaiteform = true;

        void browser_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            if (showwaiteform)//我也搞不懂为什么这个start和end时间会进入两次所以做了这个标记
            {
                showwaiteform = false;
                this.BeginInvoke(new Func<bool>(() =>
                {
                    return true;
                }));
            }
        }

        void browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            if (closewaiteform && !showwaiteform)
            {
                this.BeginInvoke(new Func<bool>(() =>
                {
                    closewaiteform = false;
                    Opacity = 1;
                    //this.FormBorderStyle = FormBorderStyle.None;
                    //this.WindowState = FormWindowState.Maximized;
                    //this.ShowInTaskbar = false;


                    return true;
                }));
            }
            TaskList.AddOrUpdate(e.Url, e.Browser.MainFrame, (a, b) => e.Browser.MainFrame);//如果旧值存在，则更新旧址.
            //e.
            // CaptureHtml();
        }
        

 

        public override void CallJsFunction(string jsFunction)
        {
            browser.ExecuteScriptAsync(jsFunction);
            browser.ExecuteScriptAsync(" console.log('"+jsFunction+"') ");

        }



        #endregion


        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!TaskList.IsEmpty)
            {
                //todo:如果不为空，则开多个线程去抓取数据.

            }

            //SendBeatCommand();
        }




        #region test

        private void button2_Click(object sender, EventArgs e)
        {
            browser.Reload(true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            browser.ShowDevTools();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            //VideoForm form = new VideoForm(this);
            //form.Show(this);
            //form.DisplayStatusMsg("远程连接失败.");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //GetOnlineUsers();
            //GetUsedtoByLoginID("A4BFA4C4-D97E-4372-82E2-69854768939B");
            var dto = GettedUserIP;
            MessageBox.Show(dto);
        }


        #endregion

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                browser.CloseDevTools();
                browser.GetBrowser().CloseBrowser(true);
            }
            catch { }

            try
            {
                if (browser != null)
                {
                    browser.Dispose();
                    Cef.Shutdown();
                }
            }
            catch { }  

            //string name = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
            //System.Diagnostics.Process[] process = System.Diagnostics.Process.GetProcessesByName(name);
            //foreach (System.Diagnostics.Process p in process)
            //{
            //    p.Kill();
            //}
        }

        private void exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void button5_Click(object sender, EventArgs e)
        {
            CaptureResultHtml();
        }
        private void button6_Click(object sender, EventArgs e)
        {
            checkjQuery();
        }

        void CaptureResultHtml()
        {
            
            var frame = browser.GetMainFrame();
            var source = frame.GetSourceAsync();
            //var source = browser.GetSourceAsync();
            source.Wait();
            txtResult.Text = source.Result;
            var uri = new Uri(frame.Url);
            string fileName = uri.Segments.LastOrDefault().EndsWith("/")
                ? "index.html"
                : uri.Segments.LastOrDefault();
            string path = string.Format("{0}/{1}/{2}", FileHelper.GetFilePath(uri.Host),uri.AbsolutePath, fileName);
            //File.WriteAllText(path, source.Result, Encoding.UTF8);
            FileHelper.SaveFile(path, source.Result);
            //todo:如果这样写，会导致页面样式路径不正确
        }

        private void checkjQuery()
        {
            string js = " WCShell.callbackjQueryChecker(typeof jQuery==='function'); ";
            CallJsFunction(js);
        }

        public void OnCheckjQueryCallBack(bool isSupport)
        {
            if (!isSupport)
            {
                string js = @"var oHead = document.getElementsByTagName('HEAD').item(0); 
                    var oScript= document.createElement('script'); 
                    oScript.src='http://libs.useso.com/js/jquery/1.9.1/jquery.min.js'; 
                    oHead.appendChild(oScript); ";
                CallJsFunction(js);
            }
        }

        private void CaptureLinks()
        {
            string js = @"
            var ary=[]; 
            $('a').each(function(i,o){
	                var url=$(o).attr('href');
	                if(url.indexOf('#')!=0){
		                ary.push(url);
		                console.log(url);
	                }
	            });
            WCShell.callbackGetAllLinks(ary.join(','));
            ";
            CallJsFunction(js);

        }
        /// <summary>
        /// 抓取页面所有链接，并且与已经处理过的链接和未处理过的链接比较，只把未包含的URL包含到任务列表里
        /// </summary>
        /// <param name="links"></param>
        public void OnCaptureLinksCallBack(List<string> links)
        {
            FilterLinks(links);
            var _list = links.Except(TaskDoneQueue);
            _list = _list.Except(TaskQueue);
            _list.ToList().ForEach(i => TaskQueue.Enqueue(i));
            lblNum.SetControlText(TaskQueue.Count.ToString());
            //lblNum.Text= TaskQueue.Count.ToString();
        }

        /// <summary>
        /// 1:去掉与当前链接相同的url
        /// 2:去掉#号结尾的url
        /// 3:去掉不同主机的url
        /// </summary>
        /// <param name="links"></param>
        private void FilterLinks(List<string> links)
        {
            var currentUrl = new Uri(browser.GetMainFrame().Url);
            var uris=links.Select(i=>new Uri(i)).ToList();
            uris.RemoveAll(
                i => i.AbsoluteUri == currentUrl.AbsoluteUri || i.AbsoluteUri.EndsWith("#") || currentUrl.Host!=i.Host);
            links.Clear();
            //uris.ForEach(i =>links.Add(i.AbsoluteUri);.ToList().CopyTo(links);
            links.AddRange(uris.Select(i=>i.AbsoluteUri));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            CaptureLinks();
        }


        private void ModifyImagesSrc(Uri uri)
        {
            string js = @"var ary=[]; 
            $('img').each(function(i,o){
	                var url=$(o).attr('src');
 			        url='" + FileHelper.GetFilePath(uri.Host).Replace("\\", "/") + @"/images/'+url.split('/').pop();
	                $(o).attr('src',url);
		            console.log(url);
	            });
		            console.log('images done!');

        ";
            CallJsFunction(js);
        }
        private void ModifyCssHref(Uri uri)
        {
            string js = @"var ary=[]; 
            $('link').each(function(i,o){
	                var url=$(o).attr('href');
 			        url='" + FileHelper.GetFilePath(uri.Host).Replace("\\", "/") + @"/css/'+url.split('/').pop();
	                $(o).attr('href',url);
		            console.log(url);
	            });
		            console.log('css done!');
        ";
            CallJsFunction(js);
        }
        private void ModifyScriptSrc(Uri uri)
        {
            string js = @"var ary=[]; 
            $('script[src]').each(function(i,o){
	                var url=$(o).attr('src');
 			        url='" + FileHelper.GetFilePath(uri.Host).Replace("\\", "/") + @"/js/'+url.split('/').pop();
	                $(o).attr('src',url);
		            console.log(url);
	            });
		        console.log('script done!');

        ";
            CallJsFunction(js);
        }

        private void ModifyLinksHref(Uri uri)
        {
            string js = @"
            $('a').each(function(i,o){
	                var url=$(o).attr('href');
                    var rawUrl=url;
                    if(url==''||url==undefined)
                    return;
                    if(url.indexOf('#')==url.length-1){
                        console.log(url);
                        $(o).attr('href','#');
                        return;
                    }
                    if(url.indexOf('#')>-1)
                    return;
                    url=url.split('//').pop();
                    var ary=url.split('/');
                    ary.shift();
                    
                    url='"+FileHelper.GetFilePath(uri.Host).Replace("\\","/")+ @"'+'/'+ary.join('/');
	                $(o).attr('href',url);
                    console.log(url+'---'+rawUrl);
	            });
		        console.log('Links done!');
        ";
            //var pageName=ary.pop();
            //pageName=pageName==''?'index.html':pageName;
            //+'/'+pageName
            CallJsFunction(js);
        }


        private void ModifyHtmlTags()
        {
            var frame = browser.GetMainFrame();
            var uri = new Uri(frame.Url);

            ModifyImagesSrc(uri);
            ModifyCssHref(uri);
            ModifyScriptSrc(uri);
            ModifyLinksHref(uri);
        }


        private void button8_Click(object sender, EventArgs e)
        {
            ModifyHtmlTags();
        }

        
    }


    
}
