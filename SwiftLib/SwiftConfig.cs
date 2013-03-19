using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SwiftLib
{
    public class SwiftConfig
    {
        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }
        private string user;

        public string User
        {
            get { return user; }
            set { user = value; }
        }
        private string authkey;

        public string Authkey
        {
            get { return authkey; }
            set { authkey = value; }
        }

        private string boxFolder;

        public string BoxFolder
        {
            get { return boxFolder; }
            set { boxFolder = value; }
        }

        private string downloadFolder;

        public string DownloadFolder
        {
            get { return downloadFolder; }
            set { downloadFolder = value; }
        }

    }
}
