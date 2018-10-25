using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.API.Areas.AuthorizeManage.Models
{
    public class PermissionModel
    {
        public List<Authorize> Authorizes { get; set; }
        public string ModulePermission { get; set; }
        public string ButtonPermission { get; set; }
        public string ColumnPermission { get; set; }
        public string RoutePermission { get; set; }
        public List<ModuleButton> ButtonList { get; set; }
        public List<ModuleColumn> ColumnList { get; set; }
        public List<string> ModuleCheckeds { get; set; }
        public List<string> ButtonCheckeds { get; set; }
        public List<string> ColumnCheckeds { get; set; }
        public List<string> RouteCheckeds { get; set; }
        public List<string> ModuleHalfCheckeds { get; set; }

    }
}
