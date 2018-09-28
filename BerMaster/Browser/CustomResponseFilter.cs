using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;
using WC.Lib.Extentions;

namespace BerMaster.Browser
{
    public class CustomResponseFilter : IResponseFilter
    {
        public event Action<string> NotifyData;

        public void Dispose()
        {
        }

        public FilterStatus Filter(System.IO.Stream dataIn, out long dataInRead, System.IO.Stream dataOut, out long dataOutWritten)
        {
            try
            {
                if (dataIn == null)
                {
                    dataInRead = 0;
                    dataOutWritten = 0;

                    return FilterStatus.Done;
                }

                dataInRead = dataIn.Length;
                dataOutWritten = Math.Min(dataInRead, dataOut.Length);

                dataIn.CopyTo(dataOut);
                dataIn.Seek(0, SeekOrigin.Begin);
                byte[] bs = new byte[dataIn.Length];
                dataIn.Read(bs, 0, bs.Length);
                string str = System.Text.Encoding.Default.GetString(bs);
                NotifyData(str);

                //if (dataAll.Count == this.contentLength)
                //{
                //    // 通过这里进行通知  
                //    NotifyData(dataAll.ToArray());

                //    return FilterStatus.Done;
                //}
                //else if (dataAll.Count < this.contentLength)
                //{
                //    dataInRead = dataIn.Length;
                //    dataOutWritten = dataIn.Length;

                //    return FilterStatus.NeedMoreData;
                //}
                //else
                //{
                //    return FilterStatus.Error;
                //}
            }
            catch (Exception ex)
            {
                LoggerFactory.GetLog().Error("CustomResponseFilter-Filter  出错！", ex);

                dataInRead = dataIn.Length;
                dataOutWritten = dataIn.Length;

                return FilterStatus.Done;
            }



            //if (dataIn == null)
            //{
            //    dataInRead = 0;
            //    dataOutWritten = 0;

            //    return FilterStatus.Done;
            //}

            //dataInRead = dataIn.Length;
            //dataOutWritten = Math.Min(dataInRead, dataOut.Length);

            //dataIn.CopyTo(dataOut);

            return FilterStatus.Done;
        }

        public bool InitFilter()
        {
            return true;
        }
    }
}
