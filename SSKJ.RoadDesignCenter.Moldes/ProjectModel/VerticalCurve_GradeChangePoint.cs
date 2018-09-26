using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class VerticalCurve_GradeChangePoint
    {
        public string GradeChangePointId { get; set; }
        public string VerticalCurveId { get; set; }
        public int SerialNumber { get; set; }
        public string Stake { get; set; }
        public double? H { get; set; }
        public double? R { get; set; }
    }
}
