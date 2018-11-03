using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteData.Models
{
    public class VerticalCurveInputDto
    {
        public string VerticalCurveId { get; set; }
        public string RouteId { get; set; }
        public int? VerticalCurveType { get; set; }
        public int? GradeChangePointNumber { get; set; }
        public int? CurveNumber { get; set; }
        public double? VerticalCurveLength { get; set; }
        public double? BeginStake { get; set; }
        public double? EndStake { get; set; }
        public string Description { get; set; }
    }
}
