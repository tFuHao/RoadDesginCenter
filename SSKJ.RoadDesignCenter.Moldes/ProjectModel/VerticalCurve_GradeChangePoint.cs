using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class VerticalCurve_GradeChangePoint
    {
        public string GradeChangePointId { get; set; }
        public string RouteId { get; set; }
        public int? SerialNumber { get; set; }
        public double? Stake { get; set; }
        public double? H { get; set; }
        public double? R { get; set; }
    }
}
