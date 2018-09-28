using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerMaster.DB
{
    public class TableConfig
    {
        public int id { get; set; }
        public string name { get; set; }
        public string alias { get; set; }
        public string url { get; set; }

        public bool isenable { get; set; }

        public string Bid { get; set; }

        public string sourcesite { get; set; }

        public DateTime CreateTime { get; set; }

    }
}
