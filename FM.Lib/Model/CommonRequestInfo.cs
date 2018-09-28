using System;
using System.Linq;
using System.Text;
using SuperSocket.ProtoBase;
using SuperSocket.SocketBase.Protocol;

namespace WC.Lib.Model
{
    // +----+-------+---+----+-------------------------+-----+
    // |  C | KEY   | l |  S |                         |  C  |
    // |    | name  | e |  e |    request body         |     |
    // |  S |  (4)  | n |  q |                         |  S  |
    // | (2)|       |(4)| (4)|                         | (2) |
    // +----+-------+---+----+-------------------------+-----+

    public class CommonRequestInfo : IRequestInfo, IPackageInfo
    {
        private const string CS = "HT";

        public CommonRequestInfo(ArraySegment<byte> header , byte[] bodyBuffer, int offset, int length)
        {

            var body = bodyBuffer.CloneRange(offset, length);
            Cs = Encoding.UTF8.GetString(header.Array, header.Offset, 2);
            //Key = Encoding.UTF8.GetString(header.Array, header.Offset+2, 4);
            Key = ((CommonCommands)BitConverter.ToInt32(header.Skip(2).ToArray(), 0)).ToString();
            Length = BitConverter.ToInt32(header.Skip(6).Take(4).ToArray(), 0);
            Body = body.Take(Length).ToArray();
            Seq = BitConverter.ToInt32(header.Skip(10).Take(4).ToArray(), 0);
            string Cs_val = Encoding.UTF8.GetString(body, Length, 2);
            validCsCode(Cs, Cs_val);

            //Encoding.UTF8.GetString(header.Array, header.Offset, 4); , bodyBuffer.CloneRange(offset, length)
        }

        public CommonRequestInfo(byte[] contentBuffer)
        {

            //var body = bodyBuffer.CloneRange(offset, length);
            Cs = Encoding.UTF8.GetString(contentBuffer, 0, 2);
            Key = ((CommonCommands)BitConverter.ToInt32(contentBuffer.Skip(2).ToArray(), 0)).ToString();
            Length = BitConverter.ToInt32(contentBuffer.Skip(6).Take(4).ToArray(), 0);
            Seq = BitConverter.ToInt32(contentBuffer.Skip(10).Take(4).ToArray(), 0);
            Body = contentBuffer.Skip(14).Take(Length).ToArray();
            string Cs_val = Encoding.UTF8.GetString(contentBuffer, 14+Length, 2);  //BitConverter.ToString(contentBuffer.Skip(contentBuffer.Length - 2).Take(2).ToArray(), 0);
            validCsCode(Cs, Cs_val);

            //Encoding.UTF8.GetString(header.Array, header.Offset, 4); , bodyBuffer.CloneRange(offset, length)
        }


        #region 属性

        /// <summary>
        /// 命令
        /// </summary>
        public string Key { get; set; }
        
        /// <summary>
        /// 序号
        /// </summary>
        public int Seq { get; set; }
        /// <summary>
        /// 内容长度
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public byte[] Body { get; set; }

        /// <summary>
        /// 校验码
        /// </summary>
        public string Cs { get; set; }

        ///// <summary>
        ///// 设备逻辑地址
        ///// </summary>
        //public string DeviceLogicalCode { get; set; }

        /// <summary>
        /// 判断该数据包是否完整
        /// </summary>
        public bool IsValid { get; 
            private set; }


        #endregion

        void validCsCode(string cs1,string cs2)
        {
            IsValid = (cs1 == cs2 && cs2 == CS);
        }

    }
}
