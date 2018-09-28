﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WC.Browser
{
    public class ResourceEntity
    {
        public ulong Identifier { get; set; }
        public string FileName { get; set; }
        public string SiteUrl { get; set; }
        public string MimeType { get; set; }
        public long FileSize { get; set; }
        public string BasePath { get; set; }

    }
}
