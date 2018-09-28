using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerMaster.DB
{
    public class OrderEntity
    {
        public int id { get; set; }
        public decimal max { get; set; }
        public decimal min { get; set; }
        public decimal sum { get; set; }
        public decimal volume { get; set; }
        public string data { get; set; }
        public string[][] d { get; set; }
        public string sourcesite { get; set; }
        
        public System.DateTime CreateTime { get; set; }

    }
}
