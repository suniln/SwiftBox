using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SwiftLib
{
    public static class FileUtil
    {
        private static List<string> fileList = new List<string>();
        public static List<String> GetFiles(string folderName)
        {
            DirectoryInfo di = new DirectoryInfo(folderName);
            // iterate through all subfolders
            foreach (DirectoryInfo directory in di.GetDirectories())
            {
                GetFiles(directory.FullName);
            }

            // list all files
            foreach (FileInfo fi in di.GetFiles())
            {
                fileList.Add(fi.FullName);
            }
            return fileList;
        }

        public static List<SwiftFileInfo> GetSwiftFileInfoList(String jsonString)
        {
            List<SwiftFileInfo> fileList = new List<SwiftFileInfo>();
            String[] fileObjects = jsonString.Split("}".ToCharArray());
            foreach (String fileObj in fileObjects)
            {
                fileList.Add(ParseSwiftFileObject(fileObj));
            }

            return fileList;
        }

        private static SwiftFileInfo ParseSwiftFileObject(String fileObj)
        {
            SwiftFileInfo sfi = new SwiftFileInfo();
            String[] tokens = fileObj.Split(",".ToCharArray());
            foreach (String token in tokens)
            {
                String tmpToken = token;
                tmpToken = tmpToken.Replace("\"", "");
                tmpToken = tmpToken.Replace(" ", "");
                tmpToken = tmpToken.Replace("{", "");
                tmpToken = tmpToken.Replace("[", "");
                String[] keyVal = tmpToken.Split(':');

                if (keyVal[0].Equals("hash"))
                    sfi.hash = keyVal[1];
                else if (keyVal[0].Equals("last_modified"))
                    sfi.last_modified = keyVal[1];
                else if (keyVal[0].Equals("bytes"))
                    sfi.bytes = keyVal[1];
                else if (keyVal[0].Equals("name"))
                    sfi.name = keyVal[1];
                else if (keyVal[0].Equals("content_type"))
                    sfi.content_type = keyVal[1];
            }

            return sfi;
        }
    }
}
