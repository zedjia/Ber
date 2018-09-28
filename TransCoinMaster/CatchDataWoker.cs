using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WC.Lib.Helper;

namespace TransCoinMaster
{
    public class CatchDataWoker
    {
        public static async Task<string> GetUSDToRMB()
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = "https://www.binance.com/exchange/public/cnyusd",//URL     必需项  
                Method = "GET",//URL     可选项 默认为Get  
                //IsGzip = true,//GZIP解压处理  
                Timeout = 100000,//连接超时时间     可选项默认为100000  
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000  
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写  
                Cookie = "",//字符串Cookie     可选项  
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0",//用户的浏览器类型，版本，操作系统     可选项有默认值  
                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值  
                ContentType = "application/json",//返回类型    可选项有默认值  
                Referer = "https://www.binance.com/",//来源URL     可选项  
                                                     //Allowautoredirect = False,//是否根据３０１跳转     可选项  
                                                     //AutoRedirectCookie = False,//是否自动处理Cookie     可选项  
                                                     //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数  
                                                     //Connectionlimit = 1024,//最大连接数     可选项 默认为1024  
            Postdata = "",//Post数据     可选项GET时不需要写  
                              //ProxyIp = "192.168.1.105：2020",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数  
                              //ProxyPwd = "123456",//代理服务器密码     可选项  
                              //ProxyUserName = "administrator",//代理服务器账户名     可选项  
                ResultType = ResultType.String,//返回数据类型，是Byte还是String  
            };
            item.Header.Add("authority", "www.binance.com");//设置请求头信息（Header）  
            item.Header.Add("method", "GET");//设置请求头信息（Header）  
            item.Header.Add("path", "/exchange/public/cnyusd");//设置请求头信息（Header）  
            item.Header.Add("scheme", "https");//设置请求头信息（Header）  
            item.Header.Add("clienttype", "web");//设置请求头信息（Header）  
            item.Header.Add("lang", "cn");//设置请求头信息（Header）  
            HttpResult result = http.GetHtml(item);
            string html = result.Html;
            string cookie = result.Cookie;
            return result.Html;
        }
        
        public static async Task<string> GetBTCToUSD()
        {
            HttpHelper http = new HttpHelper();
            HttpItem item = new HttpItem()
            {
                URL = "https://bittrex.com/Api/v2.0/pub/currencies/GetBTCPrice",//URL     必需项  
                Method = "GET",//URL     可选项 默认为Get  
                //IsGzip = true,//GZIP解压处理  
                Timeout = 100000,//连接超时时间     可选项默认为100000  
                ReadWriteTimeout = 30000,//写入Post数据超时时间     可选项默认为30000  
                IsToLower = false,//得到的HTML代码是否转成小写     可选项默认转小写  
                Cookie = "",//字符串Cookie     可选项  
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0",//用户的浏览器类型，版本，操作系统     可选项有默认值  
                Accept = "text/html, application/xhtml+xml, */*",//    可选项有默认值  
                ContentType = "application/json",//返回类型    可选项有默认值  
                Referer = "https://bittrex.com/",//来源URL     可选项  
                //Allowautoredirect = False,//是否根据３０１跳转     可选项  
                //AutoRedirectCookie = False,//是否自动处理Cookie     可选项  
                                           //CerPath = "d:\123.cer",//证书绝对路径     可选项不需要证书时可以不写这个参数  
                                           //Connectionlimit = 1024,//最大连接数     可选项 默认为1024  
                Postdata = "",//Post数据     可选项GET时不需要写  
                              //ProxyIp = "192.168.1.105：2020",//代理服务器ID     可选项 不需要代理 时可以不设置这三个参数  
                              //ProxyPwd = "123456",//代理服务器密码     可选项  
                              //ProxyUserName = "administrator",//代理服务器账户名     可选项  
                ResultType = ResultType.String,//返回数据类型，是Byte还是String  
            };
            HttpResult result = http.GetHtml(item);
            string html = result.Html;
            string cookie = result.Cookie;
            return result.Html;
        }
    }
}
