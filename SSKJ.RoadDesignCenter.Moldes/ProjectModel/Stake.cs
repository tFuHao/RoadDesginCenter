using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class Stake
    {
        public string StakeId { get; set; }
        public string RouteId { get; set; }
        public int? SerialNumber { get; set; }
        public double? StakeName { get; set; }
        public double? Offset { get; set; }
        public double? RightCorner { get; set; }
    }
}
