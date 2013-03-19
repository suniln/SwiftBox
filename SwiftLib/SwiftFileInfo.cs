using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftLib
{
    public class SwiftFileInfo
    {
        public String hash { get; set; }
        public String last_modified { get; set; }
        public String bytes { get; set; }
        public String name { get; set; }
        public String content_type { get; set; }
    }
}
