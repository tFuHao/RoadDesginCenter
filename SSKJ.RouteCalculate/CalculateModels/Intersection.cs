using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RouteCalculate.CalculateModels
{
    public class Intersection
    {
        public string IntersectionPointId { get; set; }
        public string RouteId { get; set; }
        public int? SerialNumber { get; set; }
        public string IntersectionName { get; set; }
        public double? Stake { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? R { get; set; }
        public double? Ls1 { get; set; }
        public double? Ls2 { get; set; }
        public double? Ls1R { get; set; }
        public double? Ls2R { get; set; }
    }
}
