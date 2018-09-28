using System;
using CefSharp;

namespace AlwaysOnline.Browser
{
    public class CustomResponseFilter : IResponseFilter
    {
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

            dataIn.CopyTo(dataOut);

            return FilterStatus.Done;
        }

        public bool InitFilter()
        {
            return true;
        }
    }
}
