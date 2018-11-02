using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.API.Models
{
    public class UserInfoModel
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }
        public string DataBaseName { get; set; }
        public DateTime? TokenExpiration { get; set; }
    }
}
