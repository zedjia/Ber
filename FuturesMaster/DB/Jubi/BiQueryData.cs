using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuturesMaster.DB
{
    public class AmountQueryData
    {
        public  string moneysection { get; set; }

        public int buyup { get; set; }
        public int sellup { get; set; }
        public int updiff { get; set; }
        public int buydown { get; set; }
        public int selldown { get; set; }
        public int downdiff { get; set; }
        public int totaldiff { get; set; }

    }
}
