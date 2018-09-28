using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace BerMaster.DB
{
    [Table("tradeinfo")]
    public class tradeInfo
    {
        public tradeInfo()
        {
            CreateTime=DateTime.Now;
        }
        public string amount24h { get; set; }
        public string amount24hbit { get; set; }
        public string hold { get; set; }
        public string holdbit { get; set; }
        public double last_price { get; set; }
        /// <summary>
        /// 指数
        /// </summary>
        public double last_index { get; set; }

        public System.DateTime CreateTime { get;private set; }

        [Computed]
        public TradeDepthsEntity[] tradeDepths { get; set; }

    }

    public class TradeDepthsEntity
    {
        public double amount { get; set; }
        public long createdDate { get; set; }
        public int id { get; set; }
        public bool liquid { get; set; }
        public double price { get; set; }
        public int type { get; set; }
        public double money { get; }

        public System.DateTime CreateTime
        {
            get
            {
                DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
                return dtStart.AddMilliseconds(createdDate);
            }
        }
    }

    
}
