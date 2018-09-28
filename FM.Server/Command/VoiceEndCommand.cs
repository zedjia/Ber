using System;
using System.Linq;
using System.Threading.Tasks;
using FM.Lib.Model;
using Sodao.FastSocket.Server;
using Sodao.FastSocket.Server.Command;
using Sodao.FastSocket.SocketBase;

namespace FM.Server.Command
{

    public sealed class VoiceEndCommand : ICommand<AsyncBinaryCommandInfo>, ISocketServiceContainer
    {
        /// <summary>
        /// 返回服务名称
        /// </summary>
        public string Name
        {
            get { return "VoiceEnd"; }
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

            //Console.WriteLine(string.Format(" 当前客户端Id:{0} ,Seq Id:{1},内容:{2}", connection.ConnectionID, commandInfo.SeqID, System.Text.Encoding.Default.GetString(commandInfo.Buffer)));
            string content = System.Text.Encoding.Default.GetString(commandInfo.Buffer);
            var form = Container as MainForm;

            var dto = Util.Json.ToObject<VideoFrameDto>(content);//视频参数数据
            if (form.OnlineUsers.Any(i => i.LoginID==dto.InChargeUser))
            {
                //var loginId = form.OnlineUsers.FirstOrDefault(i => i.IsLogin).LoginID;
                form.CurrentTaskQueue.TryAdd(dto.InChargeUser.ToString(), string.Format("{0},{1}", Commands.VoiceEnd.ToString(), dto.RoomId)); 

                commandInfo.Reply(connection, System.Text.Encoding.Default.GetBytes("已经收到语音结束指令"));
                var FireControlUser = form.OnlineUsers.FirstOrDefault(t => t.LoginID == dto.InChargeUser);
                Guid dutyid = Guid.Parse("BC02EBBA-B20E-448C-8B01-501E30BF981A");
                var Cjqdto =
                    form.OnlineUsers.FirstOrDefault(i => i.DeptId == FireControlUser.DeptId && i.DutyId == dutyid);
                if (Cjqdto != null)
                {
                    form.CurrentTaskQueue.TryAdd(Cjqdto.LoginID.ToString(), string.Format("{0},{1}", Commands.Voice.ToString(), FireControlUser.UserName));

                    form.DisplayMsg(string.Format("服务器已经收到{0}语音结束指令!,转发给设备采集器:{1}", connection.ConnectionID, Cjqdto.UserName));
                }
                else
                {
                    form.DisplayMsg(string.Format("服务器已经收到{0}语音结束指令!,但是未找到该消控室的采集器登录", connection.ConnectionID));
                }
            }
            else
            {
                form.DisplayMsg(string.Format("服务器已经收到{0}语音结束指令!,但是用户{1}不存在", connection.ConnectionID, dto.InChargeUser));
                commandInfo.Reply(connection, System.Text.Encoding.Default.GetBytes("已经收到语音指令,用户不存在"));
                
            }


            commandInfo.Reply(connection, System.Text.Encoding.Default.GetBytes("服务器已经收到查岗指令"));


        }




        public System.Windows.Forms.Form Container { get; set; }
    }
}
