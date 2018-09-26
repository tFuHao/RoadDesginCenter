using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class VerticalCurve
    {
        public string VerticalCurveId { get; set; }
        public string RouteId { get; set; }
        public int? VerticalCurveType { get; set; }
        public int? GradeChangePointNumber { get; set; }
        public int? CurveNumber { get; set; }
        public double? VerticalCurveLength { get; set; }
        public string BeginStake { get; set; }
        public string EndStake { get; set; }
        public string Description { get; set; }
    }
}
