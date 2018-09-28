using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AlwaysOnline.Browser;
using CefSharp;
using CefSharp.WinForms;
using WC.Lib.Controls;
using WC.Lib.Extentions;

namespace AlwaysOnline
{
    public partial class MainForm : ClientBaseForm
    {
        public ChromiumWebBrowser browser;
        private bool _taskProcessing = false;

        public MainForm()
        {
            try
            {
                InitializeComponent();
                InitBrowser();
                //this.Opacity = 0;
                //this.ShowInTaskbar = false;
                this.panel1.Controls.Add(browser);
                //browser.Dock = DockStyle.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void InitBrowser()
        {
            var setting = new CefSettings();
            setting.Locale = "zh-CN";
            Cef.Initialize(setting);

            //browser = new ChromiumWebBrowser("http://localhost");
            string _url = CurrentConfig.SystemUrl;
            browser = new ChromiumWebBrowser(_url);
            browser.FrameLoadStart += browser_FrameLoadStart;
            browser.FrameLoadEnd += browser_FrameLoadEnd;
            browser.LifeSpanHandler = new CustomLifeSpanHandler();
            browser.RequestHandler = new CustomRequestHandler();
            browser.JsDialogHandler = new CustomJsHandler();
            browser.RegisterJsObject("WCShell", new JsEventFunction(this));
            browser.MenuHandler = new MenuHandler();


        }

        void browser_FrameLoadStart(object sender, FrameLoadStartEventArgs e)
        {
            this.BeginInvoke(new Func<bool>(() =>
            {
                checkjQuery();
                return true;
            }));
        }

        void browser_FrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
                //this.BeginInvoke(new Func<bool>(() =>
                //{
                //    //Opacity = 1;
                //    //this.FormBorderStyle = FormBorderStyle.None;
                //    //this.WindowState = FormWindowState.Maximized;
                //    //this.ShowInTaskbar = false;


                //    return true;
                //}));
            //e.
            // CaptureHtml();
        }

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



        public override void CallJsFunction(string jsFunction)
        {
            browser.ExecuteScriptAsync(jsFunction);
            browser.ExecuteScriptAsync(" console.log('" + jsFunction + "') ");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();

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

        public void OnCaptureLinksCallBack(List<string> links)
        {
            //FilterLinks(links);
            //var _list = links.Except(TaskDoneQueue);
            //_list = _list.Except(TaskQueue);
            //_list.ToList().ForEach(i => TaskQueue.Enqueue(i));
            //lblNum.SetControlText(TaskQueue.Count.ToString());
            //lblNum.Text= TaskQueue.Count.ToString();
        }

        /// <summary>
        /// 网络检测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (InternetCheck.IsConnectInternet() && InternetCheck.PingIpOrDomainName("www.baidu.com"))
            {
                label3.Text = "网络状态:已连接";
            }
            else
            {
                label3.Text = "网络状态:未连接";
                if (!_taskProcessing)
                {
                    _taskProcessing = true;
                    setp1();
                    step2();
                    step3();
                }
            }
                ///网络状态:...
        }
        private void button2_Click(object sender, EventArgs e)
        {
            browser.ShowDevTools();
        }

        void setp1()
        {
            browser.Load(CurrentConfig.SystemUrl);
        }

        void step2()
        {
            Task.Delay(1000);
            string username = textBox1.Text;
            string pwd = textBox2.Text;

            string js = " $(\"#id_userName\").val(\"" + username + "\");$(\"#id_userPwd\").val(\"" + pwd + "\");";
            CallJsFunction(js);
            //id_userPwd    id_userName
        }

        void step3()
        {
            //f_login()
            Task.Delay(1000);
            string js = " f_login()";
            CallJsFunction(js);
            Task.Delay(3000);
            _taskProcessing = false;
        }

        private void checkjQuery()
        {
            string js = " WCShell.callbackjQueryChecker(typeof jQuery==='function'); ";
            CallJsFunction(js);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            browser.Load("http://www.baidu.com");

        }

        
    }
}
