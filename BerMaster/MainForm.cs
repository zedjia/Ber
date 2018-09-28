using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BerMaster.Browser;
using BerMaster.DB;
using BerMaster.Uc;
using CefSharp;
using CefSharp.OffScreen;
using Newtonsoft.Json;
using Z.Lib.Controls;

namespace BerMaster
{
    public partial class MainForm : ClientBaseForm
    {
        //todo: 动态配置表，不再写死.查询统计  小秘圈

        protected ConcurrentDictionary<string, CustomWebBrowser> CurrentBrowsers = new ConcurrentDictionary<string, CustomWebBrowser>();


        public MainForm()
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

                BindBiCheckBoxList();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                throw;
            }
            Task.Run(checkCatchStatus);
        }
        private async Task checkCatchStatus()
        {
            await Task.Delay(60000);

            while (true)
            {
                CurrentBrowsers.Values.ToList().ForEach(i =>
                {
                    var span= DateTime.Now - i.LastCatchDataTime;
                    if (span.TotalSeconds > 30)
                    {
                        LoggerFactory.GetLog().Error(string.Format(" {0} 正在尝试重新刷新.  ", i.Prefix));
                        i.Reload(false);
                    }
                });
                
                await Task.Delay(30000);
            }

        }



        /// <summary>
        /// 不同币种的订单和订单详情数据入库
        /// </summary>
        /// <param name="prefix"></param>
        /// <param name="order"></param>
        /// <param name="list"></param>
        private void RequestHandler_NotifyOrderData(string prefix, OrderEntity order, List<OrderDetailEntity> list)
        {

            DapperDbContext context = new DapperDbContext(CurrentConfig.ConnectionString);
            context.InsertOrderData(prefix, order, list);
            //BTMrefreshSign = true;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            CustomWebBrowser browser = new CustomWebBrowser("https://www.okex.com/future/refreshFutureFulLPub.do?t=1504724334898", "future");//https://www.okex.com/future/refreshFutureFulLPub.do?t=1504729261143  --LTC
                                                                                                                                             //browser.
            var requestHandler = new FuturesRequestHandler(browser, "future");
            
                requestHandler.NotifyOrderData += RequestHandler_NotifyOrderData;
                browser.RequestHandler = requestHandler;
                //requestHandler.SetUpdateLabel = uc.SetLastUpdateTimeLabel;
        }



        /// <summary>
        /// 动态增加币种监测,并进行数据展示
        /// </summary>
        /// <param name="list"></param>
        void addTabsToWatch(params TableConfig[] tableConfigs)
        {
            foreach (var item in tableConfigs)
            {
                if (CurrentBrowsers.ContainsKey(item.alias))
                {
                    MessageBox.Show(string.Format("币种({0})已经在被监控了",item.alias));
                    return;
                }

                TabPage tab=new TabPage(item.alias);
                tabContainer.TabPages.Add(tab);
                TabShowControl uc = new TabShowControl(item);
                tab.Controls.Add(uc);


                CustomWebBrowser browser = new CustomWebBrowser(item.url, item.alias);
                if(item.sourcesite==null|| item.sourcesite.ToLower()=="jubi")
                { 
                    var requestHandler = new CustomRequestHandler(browser, item.alias);
                    requestHandler.NotifyOrderData += RequestHandler_NotifyOrderData;
                    browser.RequestHandler = requestHandler;
                    requestHandler.SetUpdateLabel = uc.SetLastUpdateTimeLabel;
                }
                else if (item.sourcesite.ToLower() == "btc9")
                {
                    var requestHandler = new BTC9RequestHandler(browser, item.alias, item.Bid);
                    requestHandler.NotifyOrderData += RequestHandler_NotifyOrderData;
                    browser.RequestHandler = requestHandler;
                    requestHandler.SetUpdateLabel = uc.SetLastUpdateTimeLabel;
                }
                CurrentBrowsers.TryAdd(item.alias, browser);
                //if (!)
                //{
                //    MessageBox.Show(string.Format("币种({0})已经在被监控了",item.alias));
                //    browser.CloseDevTools();
                //    browser.GetBrowser().CloseBrowser(true);
                //    browser.Dispose();
                //}
            }
        }
        private void chkAllButton_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, true);

            }
        }

        private void refrsChkListButton_Click(object sender, EventArgs e)
        {
            BindBiCheckBoxList();
        }

        void BindBiCheckBoxList()
        {
            DapperDbContext context = new DapperDbContext(CurrentConfig.ConnectionString);
            var list= context.QueryTableConfigs();
            checkedListBox1.DataSource = list;
            checkedListBox1.DisplayMember = "name";
            checkedListBox1.ValueMember = "alias";
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                var control = sender as CheckedListBox;
                if (control != null)
                {
                    List<TableConfig> list = control.DataSource as List<TableConfig>;
                    if (list != null)
                    {
                        var item = list[e.Index];
                        addTabsToWatch(item);
                    }
                }
            }
            else
            {
                return;
                
            }
        }
        
        /// <summary>
        /// 窗体关闭时释放程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
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



        /// <summary>
        /// 配置币种页面
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ConfigForm form = new ConfigForm();
            form.Show(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ToolTipBox tbox = new ToolTipBox();
            tbox.BalloonText = "测试信息";
            tbox.Show();
        }
        
    }
}
