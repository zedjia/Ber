﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WC.Lib.Extentions
{
    public class InternetCheck
    {
         [DllImport("wininet.dll")]
         private extern static bool InternetGetConnectedState(int Description, int ReservedValue);
 
         #region 方法一
         /// <summary>
         /// 用于检查网络是否可以连接互联网,true表示连接成功,false表示连接失败 
         /// </summary>
         /// <returns></returns>
         public static bool IsConnectInternet()
         {
             int Description = 0;
             return InternetGetConnectedState(Description, 0);
         }
         #endregion
 
         #region 方法二
         /// <summary>
         /// 用于检查IP地址或域名是否可以使用TCP/IP协议访问(使用Ping命令),true表示Ping成功,false表示Ping失败 
         /// </summary>
         /// <param name="strIpOrDName">输入参数,表示IP地址或域名</param>
         /// <returns></returns>
         public static bool PingIpOrDomainName(string strIpOrDName)
         {
             try
             {
                 Ping objPingSender = new Ping();
                 PingOptions objPinOptions = new PingOptions();
                 objPinOptions.DontFragment = true;
                 string data = "";
                 byte[] buffer = Encoding.UTF8.GetBytes(data);
                 int intTimeout = 120;
                 PingReply objPinReply = objPingSender.Send(strIpOrDName, intTimeout, buffer, objPinOptions);
                 string strInfo = objPinReply.Status.ToString();
                 if (strInfo == "Success")
                 {
                     return true;
                 }
                 else
                 {
                     return false;
                 }
             }
             catch (Exception)
             {
                 return false;
             }
         }
         #endregion
     }

    
}
