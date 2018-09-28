using System;
using CefSharp;
using WC.Lib;

namespace AlwaysOnline.Browser
{
    public class CustomCssResponseFilter :BaseFileHandler, IResponseFilter
    {
        public ResourceEntity Entity;

        public CustomCssResponseFilter(ResourceEntity entity)
        {
            Entity = entity;

            Entity.BasePath = string.Format("{0}/css/{1}", FileHelper.GetFilePath(Entity.SiteUrl), Entity.FileName);
        }



        public FilterStatus Filter(System.IO.Stream dataIn, out long dataInRead, System.IO.Stream dataOut, out long dataOutWritten)
        {

            if (dataIn == null)
            {
                dataInRead = 0;
                dataOutWritten = 0;

                return FilterStatus.Done;
            }

            dataInRead = dataIn.Length;
            dataOutWritten = Math.Min(dataInRead, dataOut.Length);

            byte[] buffer = new byte[dataOutWritten];
            int bytesRead = dataIn.Read(buffer, 0, (int)dataOutWritten);
            dataOut.Write(buffer, 0, bytesRead);

            string s = System.Text.Encoding.UTF8.GetString(buffer);
            SaveContent(Entity, s);//写入文件
            return FilterStatus.Done;
        }

        public bool InitFilter()
        {
            return true;
        }
    }
}
