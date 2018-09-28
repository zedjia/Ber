using System;
using System.Drawing;

namespace WC.Lib.Model
{
    /// <summary>
    /// 在线监测的消防监测设备表数据传输对象
    /// </summary>
    public  class EquipmentTbDto  {
        public Guid? Id { get; set; }
        /// <summary>
        /// 设备名称
        /// </summary>

        public string EName { get; set; }
        /// <summary>
        /// 设备类型
        /// </summary>
        public string ETypeName { get; set; }
        /// <summary>
        /// 视频IP
        /// </summary>

        public string VedioIP { get; set; }

        /// <summary>
        /// 视频端口
        /// </summary>

        public string VedioPort { get; set; }

        /// <summary>
        /// 视频用户名
        /// </summary>

        public string SPUserName { get; set; }

        /// <summary>
        /// 视频密码
        /// </summary>

        public string SPPassword { get; set; }

        /// <summary>
        /// 视频通道
        /// </summary>

        public string SPAlisle { get; set; }

        /// <summary>
        /// 设备子类型
        /// </summary>

        public string ESubTypeName { get;set; }

        /// <summary>
        /// 图片
        /// </summary>
        public Image Image { get; set; }
        /// <summary>
        /// 站点ID
        /// </summary>
        public Guid? StationID { get; set; }
        /// <summary>
        /// 消控室ID
        /// </summary>
        public Guid? FireControlRoomID { get; set; }
    }
}
