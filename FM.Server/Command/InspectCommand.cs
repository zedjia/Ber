using System;
using System.Linq;
using System.Threading.Tasks;
using FM.Lib.Model;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;

namespace FM.Server.Command
{

    public sealed class InspectCommand : ICommand<AsyncBinaryCommandInfo>, ISocketServiceContainer
    {
        /// <summary>
        /// 返回服务名称
        /// </summary>
        public string Name
        {
            get { return "Inspect"; }
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
            string content = System.Text.Encoding.Default.GetString(commandInfo.Buffer);//{ 指令id:val,loginIds:'xxx,xxxx,xxx'}
            var form = Container as MainForm;
            string[] ary = content.Split(',');
            string id = ary.FirstOrDefault();
            ary.Skip(0).ToList().ForEach(i =>
            {
                form.CurrentTaskQueue.TryAdd(i, string.Format("{0},{1}",Commands.Inspect.ToString(),id)); //.Enqueue(string.Format("{0},{1}", id, i));
            });

            form.DisplayMsg(string.Format("服务器已经收到{0}查岗指令!", connection.ConnectionID));
            commandInfo.Reply(connection, System.Text.Encoding.Default.GetBytes("服务器已经收到查岗指令"));


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
