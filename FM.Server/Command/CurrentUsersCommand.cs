using System;
using System.Linq;
using FM.Lib.Model;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;

namespace FM.Server.Command
{

    public sealed class CurrentUsersCommand : ICommand<AsyncBinaryCommandInfo>, ISocketServiceContainer
    {
        /// <summary>
        /// 返回服务名称
        /// </summary>
        public string Name
        {
            get { return "CurrentUsers"; }
        }
        /// <summary>
        /// 执行命令并返回结果
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandInfo"></param>
        public void ExecuteCommand(IConnection connection, AsyncBinaryCommandInfo commandInfo)
        {
            
            //if (commandInfo.Buffer == null || commandInfo.Buffer.Length == 0)
            //{
            //    Console.WriteLine("数据为空");
            //    return;
            //}
            //string content = System.Text.Encoding.Default.GetString(commandInfo.Buffer);

            var form = Container as MainForm;
            var onlineList = form.OnlineUsers.Where(i => i.IsLogin).Select(i => i.LoginID).ToList();
            
            var data= string.Join(",", onlineList);
            commandInfo.Reply(connection, System.Text.Encoding.Default.GetBytes(data));
            
            
        }

        public System.Windows.Forms.Form Container { get; set; }
    }
}
