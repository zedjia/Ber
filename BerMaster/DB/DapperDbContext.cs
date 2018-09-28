using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BerMaster.DB.Jubi;
using Dapper;

namespace BerMaster.DB
{
    public class DapperDbContext
    {
        public string ConnectionString { get; private set; }
        public DapperDbContext(string connStr)
        {
            ConnectionString = connStr;
        }

        public async Task InsertOrderData(string tableName, OrderEntity orders, List<OrderDetailEntity> orderdetails)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                string query1 =string.Format("INSERT INTO {0}Order(max,min,sum,volume,data,sourcesite)VALUES(@max,@min,@sum,@volume,@data,@sourcesite)", tableName);
                //对对象进行操作
                await conn.ExecuteAsync(query1, orders);
                string query2 = string.Format("INSERT INTO {0}OrdersDetails(pid,type,time,price,amount,money,sourcesite)VALUES(@pid,@type,@time,@price,@amount,@money,@sourcesite)", tableName);
                await conn.ExecuteAsync(query2, orderdetails);

            }
        }


        public List<TableConfig> QueryTableConfigs()
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                return conn.Query<TableConfig>("select * from tableconfig").ToList();
            }
        }

        public bool InsertTableConfigs(TableConfig item)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                var _sitem =
                    conn.Query<TableConfig>("select * from tableconfig where alias=@alias", item).FirstOrDefault();
                if (_sitem != null)
                {
                    return false;
                }

                string ctable1 =
                    string.Format(
                        "CREATE TABLE[dbo].[{0}Order]([id][int] IDENTITY(1,1) NOT NULL,[max] [decimal](18, 6) NOT NULL,[min] [decimal](18, 6) NOT NULL,sourcesite varchar(50) , [sum] [decimal](18, 6) NOT NULL,[volume] [decimal](18, 6) NOT NULL,[data] [text] NOT NULL,[CreateTime] [datetime] NOT NULL, CONSTRAINT [PK_{0}Order] PRIMARY KEY CLUSTERED ([id] ASC) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]  ALTER TABLE [dbo].[{0}Order] ADD CONSTRAINT [DF_{0}Order_CreateTime]  DEFAULT(getdate()) FOR [CreateTime]", item.alias);
                conn.Execute(ctable1);
                string ctable2 =
                    string.Format(
                        "CREATE TABLE [dbo].[{0}OrdersDetails]( [Id] [int] IDENTITY(1,1) NOT NULL,[pid] [int] NOT NULL,[type] [bit] NOT NULL,[time] [datetime] NOT NULL,sourcesite varchar(50) , [price] [float] NOT NULL,[amount] [float] NOT NULL,[money] [float] NOT NULL,[CreateTime] [datetime] NOT NULL, CONSTRAINT [PK_{0}Orders] PRIMARY KEY CLUSTERED ( [Id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]  ALTER TABLE [dbo].[{0}OrdersDetails] ADD  CONSTRAINT [DF_{0}OrdersDetails_CreateTime]  DEFAULT (getdate()) FOR [CreateTime]",
                        item.alias);
                conn.Execute(ctable2);


                return conn.Execute("insert into tableconfig(name,alias,url,Bid,sourcesite) values(@name,@alias,@url,@Bid,@sourcesite)", item) > 0;

            }
        }


        public List<BiQueryData> QueryBData(string prifix,DateTime start,DateTime end)
        {
            string sql = string.Format(
                @"select moneysection,buymoney,cast(round((buymoney*100)/(buymoney+sellmoney),2)as varchar)+'%' as buyscal,sellmoney,cast(round((sellmoney*100)/(buymoney+sellmoney),2)as varchar)+'%' as sellscal,buymoney-sellmoney as diffmoney,buymoney+sellmoney as totalmoney from (
	  select moneysection 
	  ,sum(case type when 0 then money else 0 end) as sellmoney
	  ,sum(case type when 1 then money else 0 end) as buymoney
    ,sort
	  from(
			   select '全部资金' as moneysection, sum(money) as money,type,5 as sort  from [dbo].[{0}OrdersDetails] with(nolock)
			  where time>'{1}' and   time<'{2}'
			  group by type 
		  union
			  select '金额<5000' as moneysection,sum(money) as  money,type ,1 as sort  from [dbo].[{0}OrdersDetails] with(nolock)
			  where time>'{1}' and   time<'{2}'
			  and money <5000
			  group by type
		  union 
			  select '5000至10000' as moneysection,sum(money) as  money,type ,2 as sort  from [dbo].[{0}OrdersDetails] with(nolock)
			  where time>'{1}' and   time<'{2}'
			  and money >5000 and money <10000
			  group by type
		  union 
		    select '10000至20000' as moneysection,sum(money)  as  money,type ,3 as sort  from [dbo].[{0}OrdersDetails] with(nolock)
			where time>'{1}' and   time<'{2}'
			and money >10000 and money <20000
			group by type
	      union
			  select '金额>20000' as moneysection,sum(money)  as  money,type ,4 as sort   from [dbo].[{0}OrdersDetails] with(nolock)
			  where time>'{1}' and   time<'{2}'
			  and money >20000 
			  group by type
		  ) a
	  group by moneysection ,sort
	) b order by sort", prifix,start.ToString("yyyy-MM-dd HH:mm"),end.ToString("yyyy-MM-dd HH:mm"));

            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                return conn.Query<BiQueryData>(sql).ToList();
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
