using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace WC.Lib
{
    public class FileHelper
    {

        public static List<FileInfo> GetAllFiles(string dirPath)
        {
            DirectoryInfo dir=new DirectoryInfo(dirPath);
            List<FileInfo> fileList=new List<FileInfo>();
            var fsInfo = dir.GetFileSystemInfos();

            foreach (var fs in fsInfo)
            {
                if (fs is FileInfo)
                {
                    fileList.Add(fs as FileInfo);
                }
                else if (fs is DirectoryInfo)
                {
                     fileList.AddRange(GetAllFiles(fs.FullName));
                }
            }
            return fileList;

        }

        public static string GetFilePath(string url)
        {

            return string.Format(@"{0}result\{1}", AppDomain.CurrentDomain.BaseDirectory, url.Replace(".", "_"));
        }


        public static void SaveFile(string path,string content)
        {
            FileInfo fi = new FileInfo(path);
            if (!fi.Exists)
            {
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
            }
            File.WriteAllText(path, content, Encoding.UTF8);
        }

    }
}
