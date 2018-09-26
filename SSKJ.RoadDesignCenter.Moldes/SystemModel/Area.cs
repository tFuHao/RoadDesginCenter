using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.SystemModel
{
    public partial class Area
    {
        public string AreaId { get; set; }
        public string ParentId { get; set; }
        public string AreaCode { get; set; }
        public string AreaName { get; set; }
        public string QuickQuery { get; set; }
        public string SimpleSpelling { get; set; }
        public int? Layer { get; set; }
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
