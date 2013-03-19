using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SwiftLib;
using System.Configuration;
using System.Diagnostics;

namespace SwiftSync
{
    public partial class Form1 : Form
    {
        SwiftConfig cfg = new SwiftConfig();
        SwiftClient client;

        public Form1()
        {
            InitializeComponent();
            InitConfig();
            BuildClient();
        }

        private void InitConfig()
        {
            cfg.Url = System.Configuration.ConfigurationManager.AppSettings.Get("SwiftUrl");
            cfg.User = System.Configuration.ConfigurationManager.AppSettings.Get("User");
            cfg.Authkey = System.Configuration.ConfigurationManager.AppSettings.Get("AuthKey");
            cfg.BoxFolder = System.Configuration.ConfigurationManager.AppSettings.Get("BoxFolder");
            cfg.DownloadFolder = System.Configuration.ConfigurationManager.AppSettings.Get("DownloadFolder");
        }

        // build Swift client using the authentication
        // information in the configuration file
        private void BuildClient()
        {
            if (client == null)
            {
                client = new SwiftClient(cfg);
            }
        }

        private void btnUploadFiles_Click(object sender, EventArgs e)
        {
            txtLog.Text += "Uploading...\r\n";
            foreach (String file in FileUtil.GetFiles(cfg.BoxFolder))
            {
                System.IO.FileInfo fi = new System.IO.FileInfo(file);
                txtLog.Text += "Uploading file " + file + "\r\n";
                client.CreateObject("box",fi.Name, fi.DirectoryName.Substring(cfg.BoxFolder.Length));
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            txtLog.Text += "Downloading...\r\n";
            List<SwiftFileInfo> swiftFileList = client.GetObjectList("box");
            foreach (SwiftFileInfo swiftFileInfo in swiftFileList)
            {
                if (!String.IsNullOrEmpty(swiftFileInfo.name))
                {
                    txtLog.Text += "downloading " + swiftFileInfo.name + "\r\n";
                    client.GetObject("box/" + swiftFileInfo.name);
                }
            }
        }
    }
}
