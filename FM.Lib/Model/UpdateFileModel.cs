using System;
using System.Collections.Generic;

namespace WC.Lib.Model
{

    public class UpdatePackage
    {
        /// <summary>
        /// 更新标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 更新备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 发布时间
        /// </summary>
        public DateTime PublishTime { get; set; }
        /// <summary>
        /// 更新文件清单
        /// </summary>
        public List<UpdateFileModel> FilesList { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string Version { get; set; }
    }

    public class UpdateFileModel
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        public string FileSize { get; set; }
        /// <summary>
        /// 文件绝对路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 相对路径
        /// </summary>
        public string FileRelativePath { get; set; }
    }
}
