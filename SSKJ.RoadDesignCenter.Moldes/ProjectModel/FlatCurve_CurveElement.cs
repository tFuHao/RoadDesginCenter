using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class FlatCurve_CurveElement
    {
        public string CurveElementId { get; set; }
        public string FlatCurveId { get; set; }
        public int SerialNumber { get; set; }
        public string Stake { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Azimuth { get; set; }
        public int? TurnTo { get; set; }
        public double? R { get; set; }
        public string Description { get; set; }
    }
}
