using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Dapper.Contrib.Extensions;
using FuturesMaster.DB;

namespace BerMaster.DB
{
    public class DapperDbContext
    {
        public string ConnectionString { get; private set; }
        public DapperDbContext(string connStr)
        {
            ConnectionString = connStr;
        }
        /// <summary>
        /// 插入交易订单记录
        /// </summary>
        /// <param name="orderdetails"></param>
        public void InsertTradeDetailsData(List<TradeDepthsEntity> orderdetails)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                //对对象进行操作
                string query = string.Format("INSERT INTO TradeDepths(amount,id,liquid,price,type,money,CreateTime)VALUES(@amount,@id,@liquid,@price,@type,@money,@CreateTime)");
                conn.Execute(query, orderdetails);
            }
        }

        public async Task InsertTradeDetailsDataAsync(List<TradeDepthsEntity> orderdetails)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                //对对象进行操作
                string query = string.Format("INSERT INTO TradeDepths(amount,id,liquid,price,type,money,CreateTime)VALUES(@amount,@id,@liquid,@price,@type,@money,@CreateTime)");
                conn.ExecuteAsync(query, orderdetails);

            }
        }
        public void InsertTradeInfo(tradeInfo tradeInfo)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                //对象进行操作
                conn.Insert(tradeInfo);
            }
        }
        public async Task InsertTradeInfoAsync(tradeInfo tradeInfo)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                //对对象进行操作
                await conn.InsertAsync(tradeInfo);
            }
        }

        public IEnumerable<AmountQueryData> QueryAmountData(DateTime start,DateTime end)
        {
            string sql = string.Format(
                @"
  select  moneysection,buyup,sellup, buyup-sellup as updiff,buydown,selldown, buydown-selldown downdiff ,(buyup-sellup)+(selldown-buydown) as totaldiff
  from (
	  select moneysection 
	  ,sum(case type when 1 then amount else 0 end) as buyup
	  ,sum(case type when 2 then amount else 0 end) as buydown
	  ,sum(case type when 3 then amount else 0 end) as sellup
	  ,sum(case type when 4 then amount else 0 end) as selldown
    ,sort
	  from(
	   select '全部' as moneysection, sum(amount) as amount,type,5 as sort  from [dbo].[TradeDepths] with(nolock)
			  where createtime>='{0}' and createtime<='{1}'
			  group by type 
		  union
		    select '开仓<=1000' as moneysection,sum(amount)  as  amount,type ,3 as sort  from [dbo].[TradeDepths] with(nolock)
			where  amount <=1000
			and createtime>='{0}' and createtime<='{1}'
			group by type
	      union
			  select '开仓>1000' as moneysection,sum(amount)  as  amount,type ,4 as sort   from [dbo].[TradeDepths] with(nolock)
			  where amount >1000   
			  and createtime>='{0}' and createtime<='{1}'
			  group by type
		) a
	  group by moneysection ,sort
	) b order by sort
", start.ToString("yyyy-MM-dd HH:mm"), end.ToString("yyyy-MM-dd HH:mm"));

            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                var result=conn.QueryAsync<AmountQueryData>(sql);
                return result.Result;
            }
        }

        //public  void InsertOrderDetailData(string tableName, List<F> orderdetails)
        //{
        //    using (IDbConnection conn = new SqlConnection(ConnectionString))
        //    {
        //        string query = string.Format("INSERT INTO {0}OrdersDetails(pid,type,time,price,amount,money)VALUES(@pid,@type,@time,@price,@amount,@money)", tableName);
        //        //对对象进行操作
        //        conn.Execute(query, orderdetails);
        //    }
        //}

    }
}
