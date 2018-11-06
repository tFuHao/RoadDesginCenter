using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class VerticalCurve_CurveElement
    {
        public string CurveElementId { get; set; }
        public string RouteId { get; set; }
        public int? SerialNumber { get; set; }
        public double? Stake { get; set; }
        public double? H { get; set; }
        public double? I { get; set; }
        public double? R { get; set; }
        public double? Length { get; set; }
    }
}
