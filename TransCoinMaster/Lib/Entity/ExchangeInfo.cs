using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCoinMaster.Lib.Entity
{
    public class ExchangeInfo
    {
        /// <summary>
        /// 站点名称
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// 当前站点登录名
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// 登陆密码
        /// </summary>
        public string LoginPwd { get; set; }
        /// <summary>
        /// 交易密码
        /// </summary>
        public string TradePwd { get; set; }

        /// <summary>
        /// 当前站点下有哪些可以交易的币
        /// </summary>
        public List<CoinInfo> CoinList { get; set; }

    }
}
