using System.Collections.Generic;
using SSKJ.RoadDesignCenter.Models.SystemModel;

namespace SSKJ.RoadDesignCenter.API.Areas.AuthorizeManage.Data
{
    public class ColumnTreeDto
    {
        public string ParentId { get; set; }

        public string ModuleId { get; set; }

        public string ModuleColumnId { get; set; }

        public string FullName { get; set; }

        public IEnumerable<ModuleColumn> Children { get; set; }
    }
}