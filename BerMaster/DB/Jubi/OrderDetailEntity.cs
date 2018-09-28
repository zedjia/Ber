using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerMaster.DB
{
    public class OrderDetailEntity
    {
        public int Id { get; set; }
        public int pid { get; set; }
        public bool type { get; set; }
        public System.DateTime time { get; set; }
        public double price { get; set; }
        public double amount { get; set; }
        public double money { get; set; }
        public string sourcesite { get; set; }

        public System.DateTime CreateTime { get; set; }

       

    }
}
