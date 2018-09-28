using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace BerMaster
{
    public static class LoggerFactory
    {
        private static ILog _log = LogManager.GetLogger("LogFileAppender");
        public static ILog GetLog()
        {
            return _log;
        }
    }
}
