using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CefSharp;

namespace BerMaster.Browser
{
    public class FuturesResponseFilter : IResponseFilter
    {
        //public event Action<string> NotifyData;
        //private int contentLength = 0;
        public List<byte> dataAll = new List<byte>();

        //public void SetContentLength(int contentLength)
        //{
        //    this.contentLength = contentLength;
        //}



        public void Dispose()
        {
        }

        public FilterStatus Filter(System.IO.Stream dataIn, out long dataInRead, System.IO.Stream dataOut, out long dataOutWritten)
        {

            try
            {
                if (dataIn == null || dataIn.Length == 0)
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
                dataAll.AddRange(bs);
                dataInRead = dataIn.Length;
                dataOutWritten = dataIn.Length;
                return FilterStatus.NeedMoreData;
            }
            catch (Exception ex)
            {
                LoggerFactory.GetLog().Error("FuturesResponseFilter-Filter  出错！", ex);

                dataInRead = dataIn.Length;
                dataOutWritten = dataIn.Length;
                return FilterStatus.Done;
            }
            
        }

        public bool InitFilter()
        {
            return true;
        }
    }


    public class FilterManager
    {
        private static Dictionary<string, IResponseFilter> dataList = new Dictionary<string, IResponseFilter>();

        public static IResponseFilter CreateFilter(string guid)
        {
            lock (dataList)
            {
                var filter = new FuturesResponseFilter();
                dataList.Add(guid, filter);

                return filter;
            }
        }

        public static IResponseFilter GetFileter(string guid)
        {
            lock (dataList)
            {
                return dataList[guid];
            }
        }
    }
}
