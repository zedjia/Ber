using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BerMaster.DB.Jubi
{
    public class BiQueryData
    {
        public  string moneysection { get; set; }

        private decimal _buymoney;
        public decimal buymoney {
            get { return Math.Round(_buymoney/10000, 3); }
            set { _buymoney = value; } }
        public string buyscal { get; set; }


        private decimal _sellmoney;
        public decimal sellmoney {
            get { return Math.Round(_sellmoney/10000, 3); }
            set { _sellmoney = value; }
        }
        
        public string sellscal { get; set; }


        private decimal _diffmoney;
        public decimal diffmoney {
            get { return Math.Round(_diffmoney/10000, 3); }
            set { _diffmoney = value; }
        }


        private decimal _totalmoney;
        public decimal totalmoney
        {
            get { return Math.Round(_totalmoney/10000, 3); }
            set { _totalmoney = value; }
        }

    }
}
