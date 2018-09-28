using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WC.Lib.Extentions;
using WC.Lib.Model;

namespace WC.Lib.Model
{
    public static class ModelConvertExtentions
    {
        private const string CS = "HT";
        //public static CommonRequestInfo ToRequestInfo(this DataPackage data)
        //{
        

        //}

        public static byte[] ToSendData(this string content, CommonCommands CommandKey)
        {
            var commandData = BitConverter.GetBytes((int)CommandKey);//协议命令只占4位,如果占的位数长过协议，那么协议解析肯定会出错的

            var dataBody = Encoding.UTF8.GetBytes(content);

            var cs = Encoding.UTF8.GetBytes(CS);

            var dataLen = BitConverter.GetBytes(content.Length);//int类型占4位，根据协议这里也只能4位，否则会出错

            var seq = BitConverter.GetBytes(0);

            var sendData = new byte[14 + dataBody.Length+2];//命令加内容长度为8

            // +----+-------+---+----+-------------------------+-----+
            // |  C | KEY   | l |  S |                         |  C  |
            // |    | name  | e |  e |    request body         |     |
            // |  S |  (4)  | n |  q |                         |  S  |
            // | (2)|       |(4)| (4)|                         | (2) |
            // +----+-------+---+----+-------------------------+-----+

            Array.ConstrainedCopy(cs, 0, sendData, 0, 2);
            Array.ConstrainedCopy(commandData, 0, sendData, 2, 4);
            Array.ConstrainedCopy(dataLen, 0, sendData, 6, 4);
            Array.ConstrainedCopy(seq, 0, sendData, 10, 4);
            Array.ConstrainedCopy(dataBody, 0, sendData, 14, dataBody.Length);
            Array.ConstrainedCopy(cs, 0, sendData, sendData.Length - 2, 2);
            return sendData;

        }

        public static byte[] ToSendData<T>(this T content, CommonCommands CommandKey,int Seq=0) where T:class 
        {
            var commandData = BitConverter.GetBytes((int)CommandKey);//协议命令只占4位,如果占的位数长过协议，那么协议解析肯定会出错的

            var dataBody = content.ObjectToBytes();

            var cs = Encoding.UTF8.GetBytes(CS);

            var dataLen = BitConverter.GetBytes(dataBody.Length);//int类型占4位，根据协议这里也只能4位，否则会出错

            var seq = BitConverter.GetBytes(Seq);

            var sendData = new byte[14 + dataBody.Length + 2];//命令加内容长度为8

            // +----+-------+---+----+-------------------------+-----+
            // |  C | KEY   | l |  S |                         |  C  |
            // |    | name  | e |  e |    request body         |     |
            // |  S |  (4)  | n |  q |                         |  S  |
            // | (2)|       |(4)| (4)|                         | (2) |
            // +----+-------+---+----+-------------------------+-----+

            Array.ConstrainedCopy(cs, 0, sendData, 0, 2);
            Array.ConstrainedCopy(commandData, 0, sendData, 2, 4);
            Array.ConstrainedCopy(dataLen, 0, sendData, 6, 4);
            Array.ConstrainedCopy(seq, 0, sendData, 10, 4);
            Array.ConstrainedCopy(dataBody, 0, sendData, 14, dataBody.Length);
            Array.ConstrainedCopy(cs, 0, sendData, sendData.Length - 2, 2);
            return sendData;

        }


        //public static string GetString(this CommonRequestInfo requestInfo)
        //{
        //    if (!requestInfo.IsValid)
        //    {
        //        return string.Empty;
        //    }
        //    return Encoding.UTF8.GetString(requestInfo.Body);
        //}


        //public static byte[] ToDataPackage(this string content, Commands CommandKey)
        //{
        //    var commandData = Encoding.UTF8.GetBytes(CommandKey.ToString());//协议命令只占4位,如果占的位数长过协议，那么协议解析肯定会出错的

        //    var dataBody = data.ObjectToBytes();

        //    var dataTotal = BitConverter.GetBytes(data.Total);

        //    var dataLen = BitConverter.GetBytes(dataBody.Length);//int类型占4位，根据协议这里也只能4位，否则会出错

        //    var sendData = new byte[12 + dataBody.Length];//命令加内容长度为8

        //    // +----+-------+---+----+-------------------------+-----+
        //    // |  C | KEY   | l |  S |                         |  C  |
        //    // |    | name  | e |  e |    request body         |     |
        //    // |  S |  (4)  | n |  q |                         |  S  |
        //    // | (2)|       |(4)| (4)|                         | (2) |
        //    // +----+-------+---+----+-------------------------+-----+


        //    Array.ConstrainedCopy(commandData, 0, sendData, 0, 4);
        //    Array.ConstrainedCopy(dataLen, 0, sendData, 4, 4);
        //    Array.ConstrainedCopy(dataTotal, 0, sendData, 8, 4);
        //    Array.ConstrainedCopy(dataBody, 0, sendData, 12, dataBody.Length);
        //    return sendData;


        //}

    }
}
