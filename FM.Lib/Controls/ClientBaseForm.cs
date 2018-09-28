using WC.Lib.Model;

namespace WC.Lib.Controls
{
    public class ClientBaseForm:ClientBase
    {

        private const string VERSIONFILEINFO = "sys.info";
        public UpdatePackage CurrentPackageInfo;
        //private Sodao.FastSocket.Client.AsyncBinarySocketClient socketClient;
        public ConfigEntity CurrentConfig { get; set; }
        //public AudioChatClient AudioChatClient { get; set; }
        public UserDto CurrentUser { get; set; }

        public string  GettedUserIP { get; set; }


        

        public ClientBaseForm()
        {
            CurrentPackageInfo=new UpdatePackage();
            CurrentConfig = new ConfigEntity();
            //SocketClientInit();
            //LoadVersionInfo();  todo:版本检测
            //AudioChatClient = new AudioChatClient(CurrentConfig);

        }

        /*
        #region socket

        void SocketClientInit()
        {
            try
            {
                string serverIp = CurrentConfig.SocketServerIp;
                int serverPort = CurrentConfig.SocketServerPort;
                if (string.IsNullOrEmpty(serverIp))
                    return;
                socketClient = new Sodao.FastSocket.Client.AsyncBinarySocketClient(8192, 8192, 3000, 3000);
                socketClient.RegisterServerNode(serverIp, new System.Net.IPEndPoint(System.Net.IPAddress.Parse(serverIp), serverPort));
                
            }
            catch (Exception)
            {
                throw;
            }
            
        }


        



        /// <summary>
        /// 
        /// </summary>
        protected virtual void SendBeatCommand()
        {
            if (socketClient == null)
                return;
            byte[] data = System.Text.Encoding.Default.GetBytes("心跳包:" + DateTime.Now.ToString("yyyy MMMM dd hh:mm:ss"));
            socketClient.Send(Commands.Beat.ToString(), data, res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(q =>
            {
                if (q.IsFaulted)
                {
                    //MessageBox.Show(string.Format("心跳包异常:{0}", q.Exception.ToString()));
                    return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(q.Result)&&q.Result.Split(',').Any())
                    {
                        string cmd = q.Result.Split(',').FirstOrDefault();
                        if (cmd == Commands.Login.ToString())
                        {
                            SendLoginCommand(Util.Json.ToJson(CurrentUser));
                        }
                        else if (cmd == Commands.Voice.ToString())
                        {
                            StartAudio(i => { }, q.Result.Split(',').LastOrDefault());
                        }
                        else if (cmd == Commands.VoiceEnd.ToString())
                        {
                            EndAudio();
                        }
                    }
                }
            });
        }


        protected virtual void SendLoginCommand(string json)
        {
            if (socketClient == null)
                return;
            byte[] data = System.Text.Encoding.Default.GetBytes(json);
            socketClient.Send(Commands.Login.ToString(), data, res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(q =>
            {

                if (q.IsFaulted)
                {
                    //MessageBox.Show(string.Format("登陆异常:{0}", q.Exception.ToString()));
                    return;
                }

            });
        }


        protected virtual void SendVoiceCommand(string json)
        {
            if (socketClient == null)
                return;
            byte[] data = System.Text.Encoding.Default.GetBytes(json);
            socketClient.Send(Commands.Voice.ToString(), data, res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(q =>
            {

                if (q.IsFaulted)
                {
                    //MessageBox.Show(string.Format("语音异常:{0}", q.Exception.ToString()));
                    return;
                }

            });
        }

        protected virtual void SendVoiceEndCommand(string json)
        {
            if (socketClient == null)
                return;
            byte[] data = System.Text.Encoding.Default.GetBytes(json);
            socketClient.Send(Commands.VoiceEnd.ToString(), data, res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(q =>
            {

                if (q.IsFaulted)
                {
                    //MessageBox.Show(string.Format("语音结束异常:{0}", q.Exception.ToString()));
                    return;
                }
                EndAudio();
            });
        }



        /// <summary>
        /// 发送查岗指令
        /// </summary>
        /// <param name="json"></param>
        protected virtual void SendInspectCommand(string json)
        {
            if (socketClient == null)
                return;
            byte[] data = System.Text.Encoding.Default.GetBytes(json);
            socketClient.Send(Commands.Inspect.ToString(), data, res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(q =>
            {

                if (q.IsFaulted)
                {
                    //MessageBox.Show(string.Format("查岗异常:{0}", q.Exception.ToString()));
                    return;
                }

            });
        }

        /// <summary>
        /// 任务下发指令
        /// </summary>
        /// <param name="json"></param>
        protected virtual void SendTaskCommand(string json)
        {
            if (socketClient == null)
                return;
            byte[] data = System.Text.Encoding.Default.GetBytes(json);
            socketClient.Send(Commands.Task.ToString(), data, res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(q =>
            {

                if (q.IsFaulted)
                {
                    //MessageBox.Show(string.Format("下发任务异常:{0}", q.Exception.ToString()));
                    return;
                }

            });
        }
        



        /// <summary>
        /// 获取当前的在线链接用户
        /// </summary>
        /// <param name="json"></param>
        protected virtual void CurrentUsersCommand()
        {
            if (socketClient == null)
                return;
            byte[] data = System.Text.Encoding.Default.GetBytes("1");
            socketClient.Send(Commands.CurrentUsers.ToString(), data, res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(q =>
            {

                if (q.IsFaulted)
                {
                    //MessageBox.Show(string.Format("获取在线用户异常:{0}", q.Exception.ToString()));
                    return;
                }
                CallJsFunction(string.Format("remarkFireCtrlRoomByUsers('{0}',true);",q.Result));

            });
        }

        /// <summary>
        /// 通过loginid获取登录用户的信息
        /// </summary>
        /// <param name="json"></param>
        protected virtual void GetUsedtoByLoginID(string json)
        {
            if (socketClient == null)
                return;
            byte[] data = System.Text.Encoding.Default.GetBytes(json);
            socketClient.Send(Commands.GetUsedtoByLoginID.ToString(), data, res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(q =>
            {

                if (q.IsFaulted)
                {
                    //MessageBox.Show(string.Format("获取在线用户异常:{0}", q.Exception.ToString()));
                    return;
                }

                GettedUserIP = q.Result;
            });
            //while (true)
            //{
            //    if (!string.IsNullOrEmpty(GettedUserIP))
            //    {
            //        return;
            //    }
            //}

        }

        /// <summary>
        /// 通过loginid获取登录用户的信息
        /// </summary>
        /// <param name="json"></param>
        protected virtual void GetUsedtoByStationID(string json)
        {
            if (socketClient == null)
                return;
            byte[] data = System.Text.Encoding.Default.GetBytes(json);
            socketClient.Send(Commands.GetIPByStationID.ToString(), data, res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(q =>
            {

                if (q.IsFaulted)
                {
                    //MessageBox.Show(string.Format("获取在线用户异常:{0}", q.Exception.ToString()));
                    return;
                }

                GettedUserIP = q.Result;
            });
            //while (true)
            //{
            //    if (!string.IsNullOrEmpty(GettedUserIP))
            //    {
            //        return;
            //    }
            //}

        }
        #region 版本部分 暂时没用

        /// <summary>
        /// 版本检测
        /// </summary>
        protected async virtual void VersionCheckCommand()
        {
            await Task.Run(() =>
            {
                if (socketClient == null)
                    return;
                byte[] data = System.Text.Encoding.Default.GetBytes("1");
                socketClient.Send(Commands.VersionCheck.ToString(), data, res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(q =>
                {

                    if (q.IsFaulted)
                    {
                        MessageBox.Show(string.Format("版本检测异常:{0}", q.Exception.ToString()));
                        return;
                    }
                    UpdatePackage versionDto = Util.Json.ToObject<UpdatePackage>(q.Result);
                    if (versionDto == null)
                    {
                        MessageBox.Show(string.Format("接收到的版本更新数据异常"));
                    }
                    if (ConvertToVersionInt(versionDto.Version) > ConvertToVersionInt(CurrentPackageInfo.Version))//服务器版本大于本地版本，则需要更新
                    {
                        versionDto.FilesList.ForEach(i =>
                        {
                            DownloadVersionFile(i);
                        });


                    }




                    
                });
            });

            
        }

        /// <summary>
        /// todo: 下载未完成!!
        /// </summary>
        /// <param name="fileModel"></param>
        protected virtual void DownloadVersionFile(UpdateFileModel fileModel)
        {
            if (socketClient == null)
                return;
            byte[] data = System.Text.Encoding.Default.GetBytes(Util.Json.ToJson(fileModel));




            var task = socketClient.Send(Commands.Download.ToString(), data,
                res =>
                {

                    return res;
                    //System.Text.Encoding.Default.GetString(res.Buffer);
                    //byte[] file = new byte[4096];

                    //MemoryStream ms = new MemoryStream();
                    //long length = Convert.ToInt64(fileModel.FileSize);
                    //while (length > 0)
                    //{
                    //    //res.
                    //    //FileStream fs = new FileStream(@"c:\xxx.avi", FileMode.OpenOrCreate, FileAccess.Write); //写入文件流
                    //    //res.Buffer
                    //    //int count = client.Client.Receive(file);
                    //    ms.Write(res.Buffer, 0, res.Buffer.Length);
                    //    ms.Flush();
                    //    length -= res.Buffer.Length;
                    //}
                    //ms.Position = 0;
                    //return string.Empty;
                }).ContinueWith(q =>
                {

                    if (q.IsFaulted)
                    {
                        MessageBox.Show(string.Format("下载文件异常:{0}", q.Exception.ToString()));
                        return;
                    }


                });

        }
        /// <summary>
        /// 更新本地版本文件
        /// </summary>
        /// <param name="newVersion"></param>
        void UpdateLocalVersionFile(UpdatePackage newVersion)
        {
            var path = AppDomain.CurrentDomain.BaseDirectory;
            var filePath = path + VERSIONFILEINFO;
            string json = Util.Json.ToJson(newVersion);
            try
            {
                File.WriteAllText(filePath, json, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show("发布文件时发生错误:" + ex.Message);
            }
            //ConvertToVersionInt(versionPackage.Version) > ConvertToVersionInt(CurrentPackageInfo.Version)
            CurrentPackageInfo = newVersion;
        }
        /// <summary>
        /// 读取本地版本文件，读取后，在与服务器版本号比对
        /// </summary>
        async Task LoadVersionInfo()
        {
            await Task.Run(() =>
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                var filePath = path + VERSIONFILEINFO;
                if (File.Exists(filePath))
                {
                    string data = File.ReadAllText(filePath, Encoding.UTF8);
                    CurrentPackageInfo = Util.Json.ToObject<UpdatePackage>(data);
                }
                //else
                //{
                VersionCheckCommand(); //如果本地没有文件，则从服务器取.
                //}
            });
        }


        

        int ConvertToVersionInt(string version)
        {
            if (string.IsNullOrEmpty(version))
                version = "0";
            Regex reg = new Regex(@"\D+");
            version = reg.Replace(version, string.Empty);
            int _v;
            int.TryParse(version, out _v);
            return _v;
            //version.re

        }

        #endregion

        #endregion

      
        */
        

        public override void CallJsFunction(string jsFunction)
        {
            return;
        }
    }
}
