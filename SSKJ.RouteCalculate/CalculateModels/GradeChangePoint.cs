using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RouteCalculate.CalculateModels
{
    public class GradeChangePoint
    {
        public string GradeChangePointId { get; set; }
        public string RouteId { get; set; }
        public int? SerialNumber { get; set; }
        public string Stake { get; set; }
        public double? H { get; set; }
        public double? R { get; set; }
    }
}
