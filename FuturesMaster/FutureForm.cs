using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BerMaster.Browser;
using BerMaster.DB;
using CefSharp;
using WC.Lib.Controls;

namespace FuturesMaster
{
    public partial class FutureForm : ClientBaseForm
    {
        protected ConcurrentDictionary<string, CustomWebBrowser> CurrentBrowsers = new ConcurrentDictionary<string, CustomWebBrowser>();
        public FutureForm()
        {
            try
            {
                InitializeComponent();
                var settings = new CefSettings()
                {
                    //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                    CachePath =
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            "CefSharp\\Cache"),
                    UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2661.102 Safari/537.36"
                };
                //Perform dependency check to make sure all relevant resources are in our output directory.
                Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);

                InitTimePickerControl();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                
            }

        }

        private void RequestHandler_NotifyOrderData(List<TradeDepthsEntity> list,tradeInfo tradeInfo)
        {
            try
            {
                if (list == null || !list.Any())
                {
                    return;
                }

                DapperDbContext context = new DapperDbContext(CurrentConfig.ConnectionString);
                context.InsertTradeDetailsDataAsync(list);
                //context.InsertTradeInfoAsync(tradeInfo);
                context.InsertTradeInfo(tradeInfo);
                //Task.WaitAll(t1, t2);
            }
            catch (Exception e)
            {
                logPrint(string.Format("{0}-{1}", "RequestHandler_NotifyOrderData", e.Message));
            }

            //BTMrefreshSign = true;
        }
        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Text = "已启动";
            button1.Enabled = false;
            Task.Run(() => startCatchData());
        }


        async Task startCatchData()
        {
            string url = "https://www.okex.com/future/refreshFutureFulLPub.do?tradeSize=50&contractId=20170929034&t=" +
                         (DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).Ticks;
            CustomWebBrowser browser = new CustomWebBrowser(url, "future");//https://www.okex.com/future/refreshFutureFulLPub.do?t=1504729261143  --LTC   //1504807921269
            var requestHandler = new FuturesRequestHandler(browser, "future");
            requestHandler.NotifyOrderData += RequestHandler_NotifyOrderData;
            requestHandler.LogPrint = logPrint;
            browser.RequestHandler = requestHandler;
            requestHandler.SetUpdateLabel = UpdateLabelStatus;
            CurrentBrowsers.TryAdd("test", browser);
            logPrint("开始抓取:"+ url);
            while (true)
            {
                await Task.Delay(2000);
                url = "https://www.okex.com/future/refreshFutureFulLPub.do?tradeSize=50&contractId=20170929034&t=" +
                         (DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1))).Ticks;
                browser.Load(url);
                //logPrint("开始抓取:"+ url);

            }

        }


        private void FutureForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                foreach (var val in CurrentBrowsers)
                {
                    val.Value.CloseDevTools();
                    val.Value.GetBrowser().CloseBrowser(true);

                }
            }
            catch { }
            try
            {
                foreach (var val in CurrentBrowsers)
                {
                    val.Value.Dispose();
                }

                Cef.Shutdown();

            }
            catch { }
        }

        public async void UpdateLabelStatus(tradeInfo info)
        {
            this.Invoke((EventHandler) delegate
            {
                this.label2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                this.label4.Text = info.last_price.ToString();
                this.label6.Text = info.hold.ToString();
                this.label8.Text = info.amount24h.ToString();
                this.label12.Text = info.last_index.ToString("f");
                this.label14.Text = info.last_price.ToString("f");
                this.label16.Text = (info.last_index - info.last_price).ToString("f");
            });
        }

        public void logPrint(string message)
        {
            this.Invoke((EventHandler)delegate
            {
                this.logTextBox.Text =string.Format("{0}: {1} \r\n{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),message, this.logTextBox.Text);
                
            });
        }



        #region 资金流动统计

        void InitTimePickerControl()
        {
            dateTimePicker1.Value=new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            dateTimePicker2.Value = DateTime.Now;

        }


        private void button2_Click(object sender, EventArgs e)
        {
            DapperDbContext context = new DapperDbContext(CurrentConfig.ConnectionString);
            var list =context.QueryAmountData(dateTimePicker1.Value, dateTimePicker2.Value);
            dataGridView1.DataSource = list;
        }



        /// <summary>
        /// 今天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            dateTimePicker2.Value = DateTime.Now;
        }
        /// <summary>
        /// 最近6小时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddHours(-6);
            dateTimePicker2.Value = DateTime.Now;
        }
        /// <summary>
        /// 最近1小时
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddHours(-1);
            dateTimePicker2.Value = DateTime.Now;
        }
        /// <summary>
        /// 最近三十分钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddMinutes(-30);
            dateTimePicker2.Value = DateTime.Now;
        }
        /// <summary>
        /// 最近15分钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddMinutes(-15);
            dateTimePicker2.Value = DateTime.Now;
        }
        /// <summary>
        /// 最近5分钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddMinutes(-5);
            dateTimePicker2.Value = DateTime.Now;
        }
        /// <summary>
        /// 最近1分钟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddMinutes(-1);
            dateTimePicker2.Value = DateTime.Now;
        }

        #endregion

        
    }
}
