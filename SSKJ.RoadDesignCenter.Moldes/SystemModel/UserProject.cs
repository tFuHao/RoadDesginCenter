using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.SystemModel
{
    public partial class UserProject
    {
        public string UserPrjId { get; set; }
        public string UserId { get; set; }
        public string ProjectId { get; set; }
        public string PrjIdentification { get; set; }
        public string PrjDataBase { get; set; }
        public int? SerialNumber { get; set; }
    }
}
