using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCoinMaster.Lib.Entity
{
    public class CoinInfo
    {
        /// <summary>
        /// 币名称
        /// </summary>
        public string CoinName { get; set; }
        /// <summary>
        /// 当前站点下币的数量
        /// </summary>
        public double CoinAmount { get; set; }
        /// <summary>
        /// 当前币的最新价格
        /// </summary>
        public double CoinPrice { get; set; }

        /// <summary>
        /// 买一价
        /// </summary>
        public double BidPrice { get; set; }

        /// <summary>
        /// 卖一价
        /// </summary>
        public double AskPrice { get; set; }

        /// <summary>
        /// 币地址
        /// </summary>
        public string Address { get; set; }

        

        /// <summary>
        /// 当前币是属于哪个交易所
        /// </summary>
        public ExchangeInfo BelongTo { get; set; }

    }
}
