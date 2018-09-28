using System;
using System.IO;
using FM.Lib.Model;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;

namespace FM.Server.Command
{

    public sealed class DownloadCommand : ICommand<AsyncBinaryCommandInfo>, ISocketServiceContainer
    {
        /// <summary>
        /// 返回服务名称
        /// </summary>
        public string Name
        {
            get { return "Download"; }
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
            var form = Container as MainForm;
            string content = System.Text.Encoding.Default.GetString(commandInfo.Buffer);
            UpdateFileModel file = Util.Json.ToObject<UpdateFileModel>(content);
            if (file == null)
            {
                form.DisplayMsg(string.Format("请求更新文件出现错误,session Id:{0}", connection.ConnectionID));
                
            }
            form.DisplayMsg(string.Format("{0} 正在下载文件{1}", connection.ConnectionID, file.FileName));
            
            FileStream fs = new FileStream(file.FilePath, FileMode.Open);
            byte[] fileblock = new byte[4096];
            while (fs.Read(fileblock, 0, 4096) != 0)
            {
                form.DisplayMsg(string.Format("正在下载文件{0}", file.FileName));

                commandInfo.Reply(connection, fileblock);
            }


        }

        public System.Windows.Forms.Form Container { get; set; }
    }
}
