using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class Route
    {
        public string RouteId { get; set; }
        public string ParentId { get; set; }
        public string ProjectId { get; set; }
        public string RouteName { get; set; }
        public double? RouteLength { get; set; }
        public string StartStake { get; set; }
        public string EndStake { get; set; }
        public int? RouteType { get; set; }
        public string Description { get; set; }
        public string DesignSpeed { get; set; }
    }
}
