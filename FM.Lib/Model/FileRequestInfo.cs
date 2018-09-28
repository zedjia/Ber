using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WC.Lib.Model
{
    [Serializable]
    public class FileRequestInfo
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件GUID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 文件包数量
        /// </summary>
        public int PackageCount { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long FileSize { get; set; }

        /// <summary>
        /// 包序列号
        /// </summary>
        public int Seq { get; set; }

        /// <summary>
        /// 文件包数据
        /// </summary>
        public byte[] PackData;

        /// <summary>
        /// 是否已经完成
        /// </summary>
        public bool IsEnd
        {
            get { return PackageCount == (Seq + 1); }
        }
    }
}
