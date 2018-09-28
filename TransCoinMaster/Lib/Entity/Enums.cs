using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TransCoinMaster.Lib.Entity
{
    [Flags]
    public enum Priority
    {
        Low=1,
        Normal=2,
        High=4,
        Urgent=7
    }

    [Flags]
    public enum MessageType
    {
        Debug = 1,
        Info = 2,
        Warnning = 4,
        Error = 7
    }


}
