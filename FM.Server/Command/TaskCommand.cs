using System;
using System.Linq;
using System.Threading.Tasks;
using FM.Lib.Model;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;

namespace FM.Server.Command
{

    public sealed class TaskCommand : ICommand<AsyncBinaryCommandInfo>, ISocketServiceContainer
    {
        /// <summary>
        /// 返回服务名称
        /// </summary>
        public string Name
        {
            get { return "Task"; }
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
            string content = System.Text.Encoding.Default.GetString(commandInfo.Buffer);//传过来的inchargeuser就是采集器的loginid
            var form = Container as MainForm;
            string[] ary = content.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string id = ary.FirstOrDefault();
            ary.Skip(1).ToList().ForEach(i =>
            {
                Guid cjqGuid = Guid.Parse(i);
                var cjqdto = form.OnlineUsers.FirstOrDefault(t => t.LoginID == cjqGuid);
                if (cjqdto!=null)
                {
                    form.CurrentTaskQueue.TryAdd(i, string.Format("{0},{1}", Commands.Task.ToString(), id));
                    form.DisplayMsg(string.Format("服务器已经收到{0}广播下发给{1}{2}{3}指令!", connection.ConnectionID,cjqdto.DeptName,cjqdto.DutyName,cjqdto.UserName));
                }
                else
                {
                    form.DisplayMsg(string.Format("服务器已经收到{0}广播下发指令!但是用户{1}没登录服务器", connection.ConnectionID,i));
                }
            });
            //form.DisplayMsg(string.Format("服务器已经收到{0}广播下发指令!", connection.ConnectionID));
            commandInfo.Reply(connection, System.Text.Encoding.Default.GetBytes("服务器已经收到广播指令"));
        }




        public System.Windows.Forms.Form Container { get; set; }
    }
}
