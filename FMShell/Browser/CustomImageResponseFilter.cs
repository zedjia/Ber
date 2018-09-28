using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using WC.Lib;

namespace WC.Browser
{
    public class CustomImageResponseFilter :BaseFileHandler, IResponseFilter
    {
        public ResourceEntity Entity;

        public CustomImageResponseFilter(ResourceEntity entity)
        {
            Entity = entity;
            Entity.BasePath = string.Format("{0}/images/{1}", FileHelper.GetFilePath(Entity.SiteUrl), Entity.FileName);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
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

            //string s = System.Text.Encoding.UTF8.GetString(buffer);
            SaveImage(Entity, buffer);//写入文件
            return FilterStatus.Done;
        }

        public bool InitFilter()
        {
            return true;
        }
    }
}
