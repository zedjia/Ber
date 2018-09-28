using System;
using System.Threading.Tasks;
using FM.Lib.Model;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;

namespace FM.Server.Command
{

    public sealed class LoginCommand : ICommand<AsyncBinaryCommandInfo>, ISocketServiceContainer
    {
        /// <summary>
        /// 返回服务名称
        /// </summary>
        public string Name
        {
            get { return "Login"; }
        }
        /// <summary>
        /// 执行命令并返回结果
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandInfo"></param>
        public void ExecuteCommand(IConnection connection, AsyncBinaryCommandInfo commandInfo)
        {
            if (commandInfo.Buffer == null || commandInfo.Buffer.Length == 0)
            {
                Console.WriteLine("数据为空");
                //connection.BeginDisconnect();
                return;
            }
            
            //Console.WriteLine(string.Format(" 当前客户端Id:{0} ,Seq Id:{1},内容:{2}", connection.ConnectionID, commandInfo.SeqID, System.Text.Encoding.Default.GetString(commandInfo.Buffer)));
            string content = System.Text.Encoding.Default.GetString(commandInfo.Buffer);

            var form = Container as MainForm;
            var dto = Util.Json.ToObject<UserDto>(content);
            if (form.LoginCommand(connection, dto))
            {
                form.DisplayMsg(string.Format("服务器已经收到{0}登陆指令:登陆成功!", dto.UserName));
                commandInfo.Reply(connection, System.Text.Encoding.Default.GetBytes("服务器已经收到登陆指令:登陆成功!"));
                
            }
            else
            {
                form.DisplayMsg(string.Format("服务器已经收到{0}登陆指令:登陆失败!", dto.UserName));
                commandInfo.Reply(connection, System.Text.Encoding.Default.GetBytes("服务器已经收到登陆指令:登陆失败!"));
                
            }
            
        }

        /// <summary>
        /// 当登陆成功后，通知所有客户端，有新的连接
        /// </summary>
        //async void LogigSuccessCallback(IConnection conn)
        //{
        //    Task.Run(() =>
        //    {
        //        SocketServerManager.CurrentHost().ForEach(h=>
        //    {
        //        h.GetAllConnection().ForEach(i =>
        //        {
        //            i.BeginSend();
        //        });
        //    });
        //    });
            
        //}



        public System.Windows.Forms.Form Container { get; set; }
    }
}
