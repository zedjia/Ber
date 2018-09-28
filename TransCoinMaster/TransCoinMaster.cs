using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp;
using CefSharp.WinForms;
using TransCoinMaster.Lib;
using TransCoinMaster.Lib.Entity;
using WC.Lib.Controls;


namespace TransCoinMaster
{
    public partial class TransCoinMaster : Form
    {
        private ChromiumWebBrowser browser;
        public TransCoinMaster()
        {
            InitializeComponent();
            InitBrowser();
        }

        #region 浏览器控制


        

        void InitBrowser()
        {
            //var settings = new CefSettings()
            //{
            //    UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0)"
            //};
            ////Perform dependency check to make sure all relevant resources are in our output directory.
            //Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
            browser = new ChromiumWebBrowser("www.baidu.com")
            {
                Dock = DockStyle.Fill,
            };
            panelbrowser.Controls.Add(browser);
            browser.LoadingStateChanged += OnLoadingStateChanged;
            browser.ConsoleMessage += OnBrowserConsoleMessage;
            browser.StatusMessage += OnBrowserStatusMessage;
            browser.TitleChanged += OnBrowserTitleChanged;
            browser.AddressChanged += OnBrowserAddressChanged;
            browser.BindingContextChanged += Browser_BindingContextChanged;
            browser.RenderProcessMessageHandler=new CustomRenderProcessMessageHandler();
            //browser.sta
            var bitness = Environment.Is64BitProcess ? "x64" : "x86";
            this.InvokeOnUiThreadIfRequired(() => lblbrowserversion.Text = $"Chromium: {Cef.ChromiumVersion}, CEF: {Cef.CefVersion}, CefSharp: {Cef.CefSharpVersion}, Environment: {bitness}");
        }

        private void Browser_BindingContextChanged(object sender, EventArgs e)
        {
            PrintShowLogMessage(e.ToString());

        }

        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
            PrintShowLogMessage(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message), MessageType.Debug);
        }

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
            PrintShowLogMessage(args.Value, MessageType.Info);
        }

        private void OnLoadingStateChanged(object sender, LoadingStateChangedEventArgs args)
        {
            //args.
            
            //SetCanGoBack(args.CanGoBack);
            //SetCanGoForward(args.CanGoForward);

            //this.InvokeOnUiThreadIfRequired(() => SetIsLoading(!args.CanReload));
            //PrintShowLogMessage(args.Title, MessageType.Debug);
            //if (args.IsLoading)
            //{
            //    browser.ExecuteScriptAsync(@"(function() {
            //    if (window)
            //    {
            //        delete window.WebSocket;
            //        if ('WebSocket' in window) {
            //            window.WebSocket = undefined;
            //        }
            //        console.log('WebSocket support disabled');
            //    }
            //})(); ");
            //}
        }

        private void OnBrowserTitleChanged(object sender, TitleChangedEventArgs args)
        {
            PrintShowLogMessage(args.Title, MessageType.Error);
        }

        private void OnBrowserAddressChanged(object sender, AddressChangedEventArgs args)
        {
            PrintShowLogMessage(args.Address, MessageType.Warnning);
        }

        #endregion

        public void PrintShowLogMessage(string logmgs, MessageType type =MessageType.Info, Priority priority = Priority.Normal)
        {
            Color c;
            switch (type)
            {
                case MessageType.Info:
                    c = Color.Black;
                    break;
                case MessageType.Warnning:
                    c = Color.Chocolate;
                    break;
                case MessageType.Debug:
                    c = Color.DarkGray;
                    break;
                default:
                    c = Color.Red;
                    break;
            }
            this.InvokeOnUiThreadIfRequired(() =>
            {
                txtLogContainer.InsertTextColorful($"{DateTime.Now}:{logmgs}", c);
            });
        }

       
        private void LoadUrl(string url)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.RelativeOrAbsolute))
            {
                browser.Load(url);
            }
        }

        private void txtUrlBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter)
            {
                return;
            }

            LoadUrl(txtUrlBox.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadUrl(txtUrlBox.Text);
        }

        private void btnDevTools_Click(object sender, EventArgs e)
        {
            browser.ShowDevTools();
        }

        private async void button1_Click_1(object sender, EventArgs e)
        {
            var data=await CatchDataWoker.GetBTCToUSD();
            PrintShowLogMessage(data);
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            var data =await CatchDataWoker.GetUSDToRMB();
            PrintShowLogMessage(data);
        }
    }
}
