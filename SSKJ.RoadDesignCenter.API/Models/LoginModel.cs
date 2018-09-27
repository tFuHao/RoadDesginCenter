using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.API.Models
{
    public class LoginModel
    {
        public string ProjectCode { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
