using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FM.Lib.Model;

namespace FM.SocketClientDemo
{
    class Program
    {
        

        static void Main(string[] args)
        {

            //注册服务器节点，这里可注册多个(name不能重复）
            //client.RegisterServerNode("localdemo", new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 18899));
            //client.RegisterServerNode("127.0.0.1:8402", new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.2"), 8401));

            #region 初始化

            Sodao.FastSocket.Client.AsyncBinarySocketClient socketClient;
            ConfigEntity CurrentConfig = new ConfigEntity();
            try
            {
                string serverIp = CurrentConfig.SocketServerIp;
                int serverPort = CurrentConfig.SocketServerPort;
                if (string.IsNullOrEmpty(serverIp))
                    return;
                socketClient = new Sodao.FastSocket.Client.AsyncBinarySocketClient(8192, 8192, 3000, 3000);
                socketClient.RegisterServerNode(serverIp,
                    new System.Net.IPEndPoint(System.Net.IPAddress.Parse(serverIp), serverPort));

            }
            catch (Exception)
            {
                throw;
            }
            #endregion


            #region 心跳包指令


            while (true)
            {
                if (socketClient == null)
                    return;

                byte[] data =
                    System.Text.Encoding.Default.GetBytes("心跳包:" + DateTime.Now.ToString("yyyy MMMM dd hh:mm:ss"));
                socketClient.Send(Commands.Beat.ToString(), data,
                    res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(q =>
                    {
                        Console.WriteLine("发送心跳");

                        if (q.IsFaulted)
                        {
                            //MessageBox.Show(string.Format("心跳包异常:{0}", q.Exception.ToString()));
                            Console.WriteLine(string.Format("登陆异常:{0}", q.Exception.ToString()));

                            return;
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(q.Result) && q.Result.Split(',').Any())
                            {
                                string cmd = q.Result.Split(',').FirstOrDefault();
                                if (cmd == Commands.Inspect.ToString())
                                {
                                    Console.WriteLine("收到查岗指令");
                                }
                                else if (cmd == Commands.Login.ToString())
                                {
                                    string loginId = ConfigurationManager.AppSettings["loginId"];
                                    string uname = ConfigurationManager.AppSettings["username"];
                                    string pwd = ConfigurationManager.AppSettings["password"];
                                    UserDto dto = new UserDto() { UserName = uname, PassWord = pwd, LoginID = new Guid(loginId) };

                                    byte[] userdata = System.Text.Encoding.Default.GetBytes(Util.Json.ToJson(dto));
                                    socketClient.Send(Commands.Login.ToString(), userdata, res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(qq =>
                                    {

                                        if (qq.IsFaulted)
                                        {
                                            Console.WriteLine(string.Format("登陆异常:{0}", qq.Exception.ToString()));
                                            return;
                                        }
                                        Console.WriteLine("登陆成功");

                                    });
                                }
                                else if (cmd == Commands.Voice.ToString())
                                {
                                    Console.WriteLine("收到语音开始指令");
                                }
                                else if (cmd == Commands.VoiceEnd.ToString())
                                {
                                    Console.WriteLine("收到语音结束指令");
                                }
                            }
                        }
                    });

                Thread.Sleep(3000);
            }



            #endregion

            Console.ReadLine();


            //string content=string.Empty;
            //while (content != "exit")
            //{
            //    content = Console.ReadLine();
            //    var dto = new UserDto(){UserName = content,DeptName = "测试部门", DutyName = "测试职位"};
            //    var json= Util.Json.ToJson(dto);
            //    var client = new Sodao.FastSocket.Client.AsyncBinarySocketClient(8192, 8192, 3000, 3000);
            //    client.RegisterServerNode(content, new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 18899));
            //    client.Send("Login", System.Text.Encoding.Default.GetBytes(json), res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(c =>
            //    {
            //        if (c.IsFaulted)
                    
            //        {
            //            Console.WriteLine(c.Exception.ToString());
            //            return;
            //        }
            //        Task.Run(() =>
            //        {
            //            while (true)
            //            {

            //                byte[] data = System.Text.Encoding.Default.GetBytes("请求内容:" + DateTime.Now.Ticks.ToString());
            //                client.Send("Beat", data, res => System.Text.Encoding.Default.GetString(res.Buffer)).ContinueWith(q =>
            //                {
            //                    if (q.IsFaulted)
            //                    {
            //                        Console.WriteLine(q.Exception.ToString());
            //                        return;
            //                    }
            //                    Console.WriteLine(q.Result);
            //                });
            //                Task.Delay(5000);

            //            }
            //        });
            //        Console.WriteLine(c.Result);
            //    });
            //}


            //组织sum参数, 格式为<<i:32-limit-endian,....N>>
            //这里的参数其实也可以使用thrift, protobuf, bson, json等进行序列化，
            //byte[] bytes = null;
            //using (var ms = new System.IO.MemoryStream())
            //{
            //    for (int i = 1; i <= 1000; i++) ms.Write(BitConverter.GetBytes(i), 0, 4);
            //    bytes = ms.ToArray();
            //}
            //发送sum命令
            //client.Send("sum", bytes, res => BitConverter.ToInt32(res.Buffer, 0)).ContinueWith(c =>
            //{
            //    if (c.IsFaulted)
            //    {
            //        Console.WriteLine(c.Exception.ToString());
            //        return;
            //    }
            //    Console.WriteLine(c.Result);
            //});


            //client.
            Console.ReadLine();
        }



        

    }


}
