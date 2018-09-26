using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class VerticalSectionGroundLine
    {
        public string Id { get; set; }
        public string RouteId { get; set; }
        public int? SerialNumber { get; set; }
        public double? Stake { get; set; }
        public double? H { get; set; }
    }
}
