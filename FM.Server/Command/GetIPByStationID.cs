using System;
using System.Linq;
using System.Threading.Tasks;
using FM.Lib.Model;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;

namespace FM.Server.Command
{

    public sealed class GetIPByStationIDCommand : ICommand<AsyncBinaryCommandInfo>, ISocketServiceContainer
    {
        /// <summary>
        /// 返回服务名称
        /// </summary>
        public string Name
        {
            get { return "GetIPByStationID"; }
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
            string content = System.Text.Encoding.Default.GetString(commandInfo.Buffer);
            var stationid = Guid.Parse(content);//视频参数数据

            //Console.WriteLine(string.Format(" 当前客户端Id:{0} ,Seq Id:{1},内容:{2}", connection.ConnectionID, commandInfo.SeqID, System.Text.Encoding.Default.GetString(commandInfo.Buffer)));
            var form = Container as MainForm;
            var onlineList = form.OnlineUsers.FirstOrDefault(i => i.IsLogin && i.StationID == stationid).IpAddress;
            //form.CurrentTaskQueue.TryAdd(dto.InChargeUser.ToString(), string.Format("{0},{1}", Commands.GetUsedtoByLoginID.ToString(), dto.RoomId));
            var data = string.Join(",", onlineList);
            commandInfo.Reply(connection, System.Text.Encoding.Default.GetBytes(data));
        }




        public System.Windows.Forms.Form Container { get; set; }
    }
}
