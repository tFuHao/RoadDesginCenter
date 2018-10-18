using SSKJ.RoadDesignCenter.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.API.Models
{
    public class AuthorizeModel
    {
        public string ModuleAuthorizes { get; set; }
        public string ButtonAuthorizes { get; set; }
        public IEnumerable<ModuleColumn> ColumnAuthorizes { get; set; }
    }
}
