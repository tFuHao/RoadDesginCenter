using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.SystemModel
{
    public partial class ModuleForm
    {
        public string FormId { get; set; }
        public string ModuleId { get; set; }
        public string FullName { get; set; }
        public string FormJson { get; set; }
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
