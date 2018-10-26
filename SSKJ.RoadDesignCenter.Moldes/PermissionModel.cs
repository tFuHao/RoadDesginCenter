using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Models
{
    public class PermissionModel
    {
        public List<ProjectModel.Authorize> Authorizes { get; set; }
        public string ModulePermission { get; set; }
        public string ButtonPermission { get; set; }
        public string ColumnPermission { get; set; }
        public string RoutePermission { get; set; }
        public List<SystemModel.ModuleButton> ButtonList { get; set; }
        public List<SystemModel.ModuleColumn> ColumnList { get; set; }
        public List<TreeEntity> ModuleCheckeds { get; set; }
        public List<TreeEntity> ButtonCheckeds { get; set; }
        public List<TreeEntity> ColumnCheckeds { get; set; }
        public List<TreeEntity> RouteCheckeds { get; set; }
        public List<string> ModuleHalfCheckeds { get; set; }

    }
}
