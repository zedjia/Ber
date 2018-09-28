using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Windows.Forms;
using FM.Lib.Model;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;

namespace FM.Server
{
    public class ServerService : CommandSocketService<AsyncBinaryCommandInfo>
    {


        public ServerService(Form form) : base(form)
        {
            MainForm = form as MainForm;
        }

        public MainForm MainForm { private get; set; }

        public override void OnReceived(IConnection connection, AsyncBinaryCommandInfo cmdInfo)
        {
            if (cmdInfo.CmdName == Commands.Beat.ToString() && MainForm.CurrentTaskQueue.Any())
            { 
                UserDto dto;
                if (MainForm.CurrentClient.TryGetValue(connection.ConnectionID, out dto))
                {
                    if(MainForm.CurrentTaskQueue.ContainsKey(dto.LoginID.ToString()))
                    {
                        string _val;
                        MainForm.CurrentTaskQueue.TryRemove(dto.LoginID.ToString(), out _val);
                        string _cmd = _val.Split(',').FirstOrDefault();
                        _val = _val.Split(',').LastOrDefault();
                        if (_cmd == Commands.Inspect.ToString())
                        {
                            cmdInfo.Reply(connection,
                            System.Text.Encoding.Default.GetBytes(string.Format("{0},{1}", Commands.Inspect.ToString(), _val).ToCharArray()));
                            this.MainForm.DisplayMsg(string.Format("服务器已经给{0}{1}{2}发送查岗指令", dto.DeptName, dto.DutyName, dto.UserName));
                        }
                        else if (_cmd == Commands.Task.ToString())
                        {
                            cmdInfo.Reply(connection,
                            System.Text.Encoding.Default.GetBytes(string.Format("{0},{1}", Commands.Task.ToString(), _val).ToCharArray())); //任务下发指令 
                            this.MainForm.DisplayMsg(string.Format("服务器已经给{0}{1}{2}发送广播指令", dto.DeptName, dto.DutyName, dto.UserName));
                        }
                        else if (_cmd == Commands.Voice.ToString())
                        {
                            cmdInfo.Reply(connection,
                            System.Text.Encoding.Default.GetBytes(string.Format("{0},{1}", Commands.Voice.ToString(), _val).ToCharArray())); //语音对讲指令 
                            this.MainForm.DisplayMsg(string.Format("服务器已经给{0}{1}{2}发送开启语音指令", dto.DeptName, dto.DutyName, dto.UserName));
                        }
                        else if (_cmd == Commands.VoiceEnd.ToString())
                        {
                            cmdInfo.Reply(connection,
                            System.Text.Encoding.Default.GetBytes(string.Format("{0},{1}", Commands.VoiceEnd.ToString(), _val).ToCharArray())); //语音对讲结束指令 
                            this.MainForm.DisplayMsg(string.Format("服务器已经给{0}{1}{2}发送结束语音指令", dto.DeptName, dto.DutyName, dto.UserName));
                        }
                    }

                    

                }
            }

            base.OnReceived(connection, cmdInfo);
        }

        /// <summary>
        /// 当连接时会调用此方法
        /// </summary>
        /// <param name="connection"></param>
        public override void OnConnected(IConnection connection)
        {
            base.OnConnected(connection);
            
            this.MainForm.UpdateConnection(connection);
            this.MainForm.DisplayMsg(connection.RemoteEndPoint.ToString() + " connected");
        }
        /// <summary>
        /// 当连接断开时会调用此方法
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="ex"></param>
        public override void OnDisconnected(IConnection connection, Exception ex)
        {
            base.OnDisconnected(connection, ex);
            this.MainForm.DisconnectionCommand(connection);
        }
        /// <summary>
        /// 当发生错误时会调用此方法
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="ex"></param>
        public override void OnException(IConnection connection, Exception ex)
        {
            base.OnException(connection, ex);
            this.MainForm.DisplayMsg("error: " + ex.ToString());
        }
        /// <summary>
        /// 当服务端发送Packet完毕会调用此方法
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="e"></param>
        public override void OnSendCallback(IConnection connection, SendCallbackEventArgs e)
        {
            base.OnSendCallback(connection, e);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("send " + e.Status.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        /// <summary>
        /// 处理未知的命令
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandInfo"></param>
        protected override void HandleUnKnowCommand(IConnection connection, AsyncBinaryCommandInfo commandInfo)
        {
            this.MainForm.DisplayMsg("unknow command: " + commandInfo.CmdName);
        }
    }
}
