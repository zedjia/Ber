using System;

namespace WC.Lib.Model
{
    [Serializable]
    public class UserDto
    {
        public long ConnectionID { get; set; }
        public string IpAddress { get; set; }
        public DateTime ConnectTime { get; set; }
        public DateTime BeatTime { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 
        /// </summary>
        //public string RoleName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DeptName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DutyName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string StaffName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid StaffId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSuper { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid DeptId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid DutyId { get; set; }

        public Guid LoginID { get; set; }

        public Guid? StationID { get; set; }


        public bool IsLogin { get; set; }

    }
}
