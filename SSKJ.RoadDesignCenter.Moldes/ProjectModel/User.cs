using System;
using System.ComponentModel.DataAnnotations;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class User
    {
        public string UserId { get; set; }
        [Required(ErrorMessage = "账户不能为空")]
        public string Account { get; set; }
        [PasswordValidator]
        public string Password { get; set; }
        public string Secretkey { get; set; }
        public string RealName { get; set; }
        [Required(ErrorMessage = "头像不能为空")]
        public string HeadIcon { get; set; }
        public int? Gender { get; set; }
        public DateTime? Birthday { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string ManagerId { get; set; }
        public string RoleId { get; set; }
        public DateTime? FirstVisit { get; set; }
        public DateTime? LastVisit { get; set; }
        public int? LogOnCount { get; set; }
        public int? DeleteMark { get; set; }
        public int? EnabledMark { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyUserId { get; set; }
    }
}
