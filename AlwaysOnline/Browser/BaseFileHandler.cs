using System.IO;

namespace AlwaysOnline.Browser
{
    public class BaseFileHandler
    {

        protected void SaveContent(ResourceEntity entity, string content)
        {
            //todo:此处尚未解决分块读取文件的情况！！！！
            FileInfo fi = new FileInfo(entity.BasePath);
            //fi.
            if (!fi.Exists)
            {
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }

                File.WriteAllText(entity.BasePath, content);
            }
            else
            {
                File.AppendAllText(entity.BasePath, content);
            }

        }
        protected void SaveImage(ResourceEntity entity, byte[] buffer)
        {
            FileInfo fi = new FileInfo(entity.BasePath);
            if (!fi.Exists)
            {
                if (!fi.Directory.Exists)
                {
                    fi.Directory.Create();
                }
                File.WriteAllBytes(entity.BasePath, buffer);
            }
            else
            {
                if (fi.Length < entity.FileSize)
                {
                    byte[] total = new byte[fi.Length + buffer.Length];
                    byte[] currt= File.ReadAllBytes(entity.BasePath);
                    currt.CopyTo(total, 0);
                    buffer.CopyTo(total, currt.Length);
                    File.WriteAllBytes(entity.BasePath, total);
                }

            }

        }

        //private Image BytesToImage(byte[] buffer)
        //{
        //    MemoryStream ms = new MemoryStream(buffer);
        //    Image image = System.Drawing.Image.FromStream(ms);
        //    return image;
        //}
    }
}
