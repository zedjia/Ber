using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FM.Lib;
using FM.Lib.Model;
using FM.Server.Db;
using OMCS;
using OMCS.Server;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.SocketBase;

namespace FM.Server
{
    public partial class MainForm : Form
    {

        //[DllImport("kernel32.dll")]
        //public static extern bool AllocConsole();
        private const string VERSIONFILEINFO ="sys.info";
        public BindingList<UserDto> OnlineUsers;
        public UpdatePackage CurrentPackageInfo;

        public static ConcurrentDictionary<long, UserDto> CurrentClient = new ConcurrentDictionary<long, UserDto>();
        public ConcurrentDictionary<string, string> CurrentTaskQueue = new ConcurrentDictionary<string, string>();
        //public ConcurrentDictionary<string, string> TaskQueue = new ConcurrentDictionary<string, string>();


        public ConfigEntity CurrentConfig { get; set; }

        public MainForm()
        {
            LoadVersionInfo();
            //AllocConsole();
            CurrentConfig = new ConfigEntity();
            InitializeComponent();
            InitSocket();
            dataGridView1.AutoGenerateColumns = false;
            
            OnlineUsers = new BindingList<UserDto>(new List<UserDto>());
            flag = DateTime.Now.Date.AddDays(1).ToString("MM-dd");
            InitOMCSServer();
            //RefreashGrid();
            UpdateDataTimer.Start();

            
        }

        

        #region 指令方法

        /// <summary>
        /// 异步刷新控件
        /// </summary>
        private void RefreashGrid()
        {
            dataGridView1.BeginInvoke(new Func<bool>(() =>
            {
                dataGridView1.DataSource = OnlineUsers;
                dataGridView1.Refresh();
                return true;
            }));
        }

        /// <summary>
        /// 当连接的时候就添加一个连接记录，通过登陆传入的数据，更新记录列表.如果该连接未登陆，则返回false
        /// </summary>
        /// <param name="conn"></param>
        public bool UpdateConnection(IConnection conn)
        {

            UserDto dto; //更新在线用户列表
            if (CurrentClient.TryGetValue(conn.ConnectionID, out dto))
            {
                dto.BeatTime = DateTime.Now;
                dto.IpAddress = conn.RemoteEndPoint.ToString();
                return !dto.IsLogin && (dto.BeatTime - dto.ConnectTime).TotalSeconds > 20;//此处是已经连接，未登陆，并且链接时间大于XX秒的用户则要求重新提交登陆数据    todo: 实际生产环境数据建议大于180 ,
            }
            else 
            {
                UserDto newDto=new UserDto();
                newDto.ConnectionID = conn.ConnectionID;
                newDto.BeatTime = newDto.ConnectTime = DateTime.Now;
                newDto.IpAddress = conn.RemoteEndPoint.ToString();
                CurrentClient.TryAdd(conn.ConnectionID,newDto);
                return false;

            }
        }

        public bool LoginCommand(IConnection conn, UserDto dto)
        {
            UserDto _dto; //更新在线用户列表   //没有判断同一个账号在不同客户端同时登录的情况
           
            if (CurrentClient.TryGetValue(conn.ConnectionID, out _dto))
            {
                dto.ConnectionID = conn.ConnectionID;
                dto.BeatTime = DateTime.Now;
                dto.IpAddress = conn.RemoteEndPoint.ToString();
                dto.ConnectTime = _dto.ConnectTime;
                dto.IsLogin = true;
                CurrentClient.TryUpdate(conn.ConnectionID, dto, _dto);
                //return true;
            }
            try
            {
                AppLog.WriteLog(dto.DeptName + dto.StaffName + "使用" + dto.UserName + "登录系统");
                using (FMDBEntities db = new FMDBEntities())
                {

                    var OnlineLoginID = CurrentClient.Values.Select(i => i.LoginID).ToList();

                    var loginedmodel = db.System_Login.Where(i => OnlineLoginID.Contains(i.LoginID)).ToList();
                    foreach (var model in loginedmodel)
                    {
                        if (model != null)
                        {
                            if (model.IsOnline == null || model.IsOnline == false) //更新数据库中已登录的账户的状态
                            {
                                model.IsOnline = true;
                            }
                            model.IP = CurrentClient.Values.First(t => t.LoginID == model.LoginID).IpAddress;
                        }
                    }
                    var OffLine = db.System_Login.Where(i =>i.StatusValue==1&&!OnlineLoginID.Contains(i.LoginID)).ToList();
                    foreach (var offlinesuer in OffLine)
                    {
                        if (offlinesuer != null)
                        {
                            if (offlinesuer.IsOnline == true || offlinesuer.IsOnline == null)//更新数据库中未登录的账户的状态，防止server崩溃后，用户也下线了，当再次启动服务的时候该用户的在线的状态一直不会被改变一直处于在线状态
                            {
                                offlinesuer.IsOnline = false;
                            }
                        }
                    }
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                //DisplayMsg(ex.StackTrace);
                AppLog.WriteLog("用户登录后修改数据库值出错"+  ex.StackTrace);
                //throw;
            }
            return true;//todo:需要登陆验证

        }

        public void DisconnectionCommand(IConnection conn)
        {
            UserDto _dto;
            CurrentClient.TryRemove(conn.ConnectionID, out _dto);
            DisplayMsg(string.Format(" 用户 {0} ({1}) 断开链接了 ",_dto.UserName,_dto.StaffName));
            using (FMDBEntities db = new FMDBEntities())
            {
                var model = db.System_Login.FirstOrDefault(i => i.LoginID == _dto.LoginID);
                if (model != null)
                {
                    model.IsOnline = false;
                }
                db.SaveChanges();
            }


        }


        #endregion


        #region  方法

        public void DisplayMsg(string msg)
        {
            if (this.listBox1.InvokeRequired)
            {
                //this.be
                this.listBox1.BeginInvoke(new Func<bool>(() =>
                {
                    this.listBox1.Items.Add(msg);
                    //this.listBox1.AutoScrollOffset= 
                    //this.listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
                    return true;
                }));

            }
            else
            {
                this.listBox1.Items.Add(msg);
            }

        }
        /// <summary>
        /// 读取配置文件
        /// </summary>
        void LoadVersionInfo()
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = path + VERSIONFILEINFO;
            if (File.Exists(filePath))
            {
                string data = File.ReadAllText(filePath, Encoding.UTF8);
                CurrentPackageInfo = Util.Json.ToObject<UpdatePackage>(data);

            }
            else
            {
                CurrentPackageInfo = CurrentPackageInfo ?? new UpdatePackage();
                //MessageBox.Show("版本文件不存在");
            }
        }

        private string flag;
        private  const string time = "02:00";
        private void UpdateDataTimer_Tick(object sender, EventArgs e)
        {

            if (DateTime.Now.ToString("HH:mm") == time)
            {
                if (flag == DateTime.Now.Date.ToString("MM-dd"))
                {
                    UpdateDataTimer.Stop();
                    restartApp();
                }

            }
            var _list = CurrentClient.Values.ToList();
            OnlineUsers.Clear();
            OnlineUsers = null;
            OnlineUsers = new BindingList<UserDto>(_list);
            RefreashGrid();
            this.toolStripStatusLabel1.Text = string.Format("当前连接数:{0}", CurrentClient.Count);
        }


        private void restartApp()
        {
          SocketServerManager.Stop();
          System.Diagnostics.Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location);
         Application.Exit();

        }
        #endregion

        


        #region socket setting
        void InitSocket()
        {
            DisplayMsg("start the server!");

            SocketServerManager.Init("socketServer",this);
            SocketServerManager.Start();
            
            DisplayMsg("The server started successfully!");

            //Stop the appServer
            //appServer.Stop();
        }



        #endregion



        #region omcs server


        private static IMultimediaServer MultimediaServer;
        void InitOMCSServer()
        {
            try
            {
                GlobalUtil.SetAuthorizedUser("FreeUser", "");
                GlobalUtil.SetMaxLengthOfUserID(CurrentConfig.MaxLengthOfUserID);
                OMCSConfiguration config = new OMCSConfiguration();

                //用于验证登录用户的帐密
                DefaultUserVerifier userVerifier = new DefaultUserVerifier();
                MultimediaServer = MultimediaServerFactory.CreateMultimediaServer(CurrentConfig.AudioPort, userVerifier, config, true);
                MultimediaServer.UserConnected += MultimediaServer_UserConnected;
                MultimediaServer.UserDisconnected += MultimediaServer_UserDisconnected;
                
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message);
            }
        }

        void MultimediaServer_UserDisconnected(string obj)
        {
            DisplayMsg(string.Format("语音系统 {0} :语音聊天断开", obj));

        }

        void MultimediaServer_UserConnected(string obj)
        {
            DisplayMsg(string.Format("语音系统 {0} :语音聊天连接", obj));
        }

        #endregion



        private void button1_Click(object sender, EventArgs e)
        {
            LoadVersionInfo();
            textBox1.Text = label2.Text = CurrentPackageInfo.Version;
            
            //UpdatePackage
            //listView1.Items.Add(new ListViewItem())

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button3.Enabled = false;

            if (folderBrowserDialog1.ShowDialog(this) == DialogResult.OK)
            {
                dataGridView2.DataSource = null;
                List<UpdateFileModel> fileModels=new List<UpdateFileModel>();

                string dirpath = folderBrowserDialog1.SelectedPath;
                var files = FileHelper.GetAllFiles(dirpath);
                files.ForEach(file =>
                {
                    fileModels.Add(new UpdateFileModel() { FileName = file.Name, FilePath = file.FullName, FileRelativePath = file.FullName.Replace(dirpath,string.Empty), FileSize = file.Length.ToString() });
                });
                dataGridView2.DataSource = fileModels;
                if (fileModels.Any())
                {
                    button3.Enabled = true;
                }
            }

        }
        #region 右键菜单

        private void 退出程序ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定要退出服务吗？", "提示", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Application.ExitThread();
                Application.Exit();
                Process.GetCurrentProcess().Kill();
                System.Environment.Exit(0);
            }
        }

        private void 清空日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            UpdatePackage newVersion=new UpdatePackage();
            newVersion.Version = textBox1.Text;
            newVersion.PublishTime = dateTimePicker1.Value;
            newVersion.Title = textBox2.Text;
            newVersion.Remark = textBox3.Text;
            newVersion.FilesList = dataGridView2.DataSource as List<UpdateFileModel>;
            string json= Util.Json.ToJson(newVersion);

            var path = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = path + VERSIONFILEINFO;
            try
            {
                File.WriteAllText(filePath, json, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("发布文件时发生错误:" + ex.Message);
            }
            MessageBox.Show("新版本发布成功");
            CurrentPackageInfo = newVersion;
            //VERSIONFILEINFO


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.notifyIcon1.Text = this.Text;

        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Visible = true;
            this.WindowState = FormWindowState.Normal;
            this.notifyIcon1.Visible = false;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.Hide();
                this.notifyIcon1.Visible = true;
            }
        }





    }
   
    //}
}
