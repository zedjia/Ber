using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using CefSharp.OffScreen;

namespace BerMaster.Browser
{
    public class CustomWebBrowser: ChromiumWebBrowser
    {
        public CustomWebBrowser(string url,string prefix) : base(url)
        {
            Prefix = prefix;
        }
        public DateTime LastCatchDataTime { get; set; }

        public string Prefix { get; set; }

    }
}
