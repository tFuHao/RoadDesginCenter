using System.Collections.Generic;
using SSKJ.RoadDesignCenter.Models.SystemModel;

namespace SSKJ.RoadDesignCenter.API.Areas.AuthorizeManage.Data
{
    public class ModuleConvertButton
    {
        public string ModuleButtonId { get; set; }
        public string FullName { get; set; }
        public IEnumerable<ModuleButton> Children { get; set; }
    }
}