using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class CrossSectionGroundLineData
    {
        public string CrossSectionGroundLineDataId { get; set; }
        public string CrossSectionGroundLineId { get; set; }
        public int SerialNumber { get; set; }
        public double? Dist { get; set; }
        public double? H { get; set; }
    }
}
