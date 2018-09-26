using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class AddStake
    {
        public string AddStakeId { get; set; }
        public string RouteId { get; set; }
        public int? SerialNumber { get; set; }
        public double? Stake { get; set; }
        public string Description { get; set; }
    }
}
