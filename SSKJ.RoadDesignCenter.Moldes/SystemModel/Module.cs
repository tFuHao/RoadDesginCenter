using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.SystemModel
{
    public partial class Module
    {
        public string ModuleId { get; set; }
        public string ParentId { get; set; }
        public string EnCode { get; set; }
        public string FullName { get; set; }
        public string Icon { get; set; }
        public string UrlAddress { get; set; }
        public string Target { get; set; }
        public int? IsMenu { get; set; }
        public int? AllowCache { get; set; }
        public int? SortCode { get; set; }
        public int? DeleteMark { get; set; }
        public int? EnabledMark { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? ModifyDate { get; set; }
        public string ModifyUserId { get; set; }       
    }
}
