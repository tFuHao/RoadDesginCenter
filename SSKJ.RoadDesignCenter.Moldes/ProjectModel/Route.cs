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
        public double? StartStake { get; set; }
        public double? EndStake { get; set; }
        public int? RouteType { get; set; }
        public string Description { get; set; }
        public string DesignSpeed { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public int? IntersectionNumber { get; set; }
        public int? CureNumber { get; set; }
        public int? GradeChangeNumber { get; set; }
    }
}
