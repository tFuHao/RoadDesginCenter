using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.SystemModel
{
    public partial class ModuleColumn
    {
        public string ModuleColumnId { get; set; }
        public string ModuleId { get; set; }
        public string ParentId { get; set; }
        public string FullName { get; set; }
        public int? SortCode { get; set; }
        public string Description { get; set; }
    }
}
