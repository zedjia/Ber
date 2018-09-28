using System;
using System.Windows.Forms;
using FM.Lib.Model;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;

namespace FM.Server.Command
{

    public sealed class BeatCommand : ICommand<AsyncBinaryCommandInfo>, ISocketServiceContainer
    {
        /// <summary>
        /// 返回服务名称
        /// </summary>
        public string Name
        {
            get { return "Beat"; }
        }
        /// <summary>
        /// 执行命令并返回结果
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="commandInfo"></param>
        public void ExecuteCommand(IConnection connection, AsyncBinaryCommandInfo commandInfo)
        {
            var form = Container as MainForm;
            if (form != null)
            {
                if (!form.UpdateConnection(connection)) //如果不要求重新登陆
                {
                    form.DisplayMsg(string.Format("心跳包---当前客户端Id:{0} ,Seq Id:{1},内容:{2}", connection.ConnectionID, commandInfo.SeqID, System.Text.Encoding.Default.GetString(commandInfo.Buffer)));
                    string content = "心跳包收到";
                    commandInfo.Reply(connection, System.Text.Encoding.Default.GetBytes(content.ToCharArray()));
                }
                else
                {
                    commandInfo.Reply(connection, System.Text.Encoding.Default.GetBytes(string.Format("{0},{1}", Commands.Login.ToString(), "1").ToCharArray()));
                }
            }
            
            commandInfo.Reply(connection, System.Text.Encoding.Default.GetBytes("0".ToCharArray()));
            //if (commandInfo.Buffer == null || commandInfo.Buffer.Length == 0)
            //{
            //    Console.WriteLine("sum参数为空");
            //    connection.BeginDisconnect();
            //    return;
            //}
            //if (commandInfo.Buffer.Length % 4 != 0)
            //{
            //    Console.WriteLine("sum参数错误");
            //    connection.BeginDisconnect();
            //    return;
            //}

            //int skip = 0;
            //var arr = new int[commandInfo.Buffer.Length / 4];
            //for (int i = 0, l = arr.Length; i < l; i++)
            //{
            //    arr[i] = BitConverter.ToInt32(commandInfo.Buffer, skip);
            //    skip += 4;
            //}

            //commandInfo.Reply(connection, BitConverter.GetBytes(arr.Sum()));
        }

        public Form Container { get; set; }
    }
}
