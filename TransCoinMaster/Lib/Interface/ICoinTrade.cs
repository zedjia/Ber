using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCoinMaster.Lib.Interface
{
    public interface ICoinTrade
    {

        /// <summary>
        /// 下单购买
        /// </summary>
        void OrderPurchase();

        void QueryOrderStatus();

        /// <summary>
        /// 撤销订单
        /// </summary>
        void OrderCancel();

        /// <summary>
        /// 资产划转,交易成功后开始转移搬砖
        /// </summary>
        void CoinTransfer();

        /// <summary>
        /// 查询转币的状态
        /// </summary>
        void QueryCoinTransStatus();
    }
}
