using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class CrossSectionGroundLine
    {
        public string CrossSectionGroundLineId { get; set; }
        public string RouteId { get; set; }
        public double? Stake { get; set; }
    }
}
