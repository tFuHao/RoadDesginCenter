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
        public string PrjName { get; set; }
        public string Description { get; set; }
        public string OwnerUnit { get; set; }
        public string DesignUnit { get; set; }
        public string SupervisoryUnit { get; set; }
        public string ConstructionUnit { get; set; }
        public DateTime? ModifyDate { get; set; }
    }
}
