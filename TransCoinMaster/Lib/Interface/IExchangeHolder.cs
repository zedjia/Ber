using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCoinMaster.Lib.Interface
{
    public interface IExchangeHolder
    {
        
        /// <summary>
        /// 获取或者配置当前站点可以交易的币种
        /// </summary>
        void GetTradeCoinsInfo();

        /// <summary>
        /// 获取某个站点下最
        /// </summary>
        void GetLowPriceByCoin();

        /// <summary>
        /// 获取站点可用资金
        /// </summary>
        void GetAvailableFunds();

        /// <summary>
        /// 获取某个币最新的行情信息
        /// </summary>
        void GetLastestCoinMarketInfo();

    }
}
