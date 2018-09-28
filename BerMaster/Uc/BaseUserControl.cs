using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WC.Lib.Model;

namespace BerMaster.Uc
{
    public class BaseUserControl: UserControl
    {
        public ConfigEntity CurrentConfig { get; set; }

        public BaseUserControl()
        {
            CurrentConfig = new ConfigEntity();
        }

    }
}
