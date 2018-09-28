using System;
using FM.Lib.Model;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;

namespace FM.Server.Command
{

    public sealed class VersionCheckCommand : ICommand<AsyncBinaryCommandInfo>, ISocketServiceContainer
    {
        /// <summary>
        /// 返回服务名称
        /// </summary>
        public string Name
        {
            get { return "VersionCheck"; }
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
                return;
            }
            ////Console.WriteLine(string.Format(" 当前客户端Id:{0} ,Seq Id:{1},内容:{2}", connection.ConnectionID, commandInfo.SeqID, System.Text.Encoding.Default.GetString(commandInfo.Buffer)));
            //string content = System.Text.Encoding.Default.GetString(commandInfo.Buffer);

            var form = Container as MainForm;
            string json = Util.Json.ToJson(form.CurrentPackageInfo);
            commandInfo.Reply(connection, System.Text.Encoding.Default.GetBytes(json));
        }

        public System.Windows.Forms.Form Container { get; set; }
    }
}
