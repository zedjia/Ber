using System;

namespace WC.Lib.Model
{
    public class VideoFrameDto
    {


        /// <summary>
        /// 描述
        /// </summary>
        //[Display(Name = "描述")]
        public string Remark { get; set; }
        /// <summary>
        /// 消控室名称
        /// </summary>
        //[Display(Name = "消控室名称")]
        public string FireControlRoomName { get; set; }
        //[Display(Name = "消控室编号")]
        public string FireControlRoomCode { get; set; }

        /// <summary>
        /// 站点ID
        /// </summary>
        //[Display(Name = "站点ID")]
        public Guid? StationID { get; set; }


        public Guid InChargeUser { get; set; }
        


        //[Display(Name = "站点名称")]
        public string StationName { get; set; }
        /// <summary>
        /// 视频类型
        /// </summary>
        //[Display(Name = "视频类型")]
        public string VideoType { get; set; }

        public string VideoName { get; set; }

        /// <summary>
        /// 视频端口
        /// </summary>
        //[Display(Name = "视频端口")]
        public int? VideoPort { get; set; }

        /// <summary>
        /// 视频连接的时候的用户名
        /// </summary>
        //[Display(Name = "视频用户名")]
        public string VideoUserName { get; set; }

        /// <summary>
        /// 视频连接的时候用的密码
        /// </summary>
        //[Display(Name = "视频密码")]
        public string VideoPassword { get; set; }

        /// <summary>
        /// 视频连接的时候的通道
        /// </summary>
        //[Display(Name = "视频通道")]
        public string VideoAisle { get; set; }

        /// <summary>
        /// 视频连接的时候的IP
        /// </summary>
        //[Display(Name = "视频IP")]
        public string VideoIP { get; set; }

        ///// <summary>
        ///// 消防的维修保养责任人
        ///// </summary>
        ////[Display(Name = "维修保养责任人")]
        //public string MaintPeople { get; set; }

        ///// <summary>
        ///// 开房// </summary>
        ////[Display(Name = "维修保养联系电话")]
        //public string MaintPhone { get; set; }

        public string RoomId { get; set; }


    }
}
