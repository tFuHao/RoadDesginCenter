using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Utility.CalculateModels
{
   public class GradeChangePoint
    {
        public string GradeChangePointId { get; set; }
        public string RouteId { get; set; }
        public int? SerialNumber { get; set; }
        public double? Stake { get; set; }
        public double? H { get; set; }
        public double? R { get; set; }
    }
}
