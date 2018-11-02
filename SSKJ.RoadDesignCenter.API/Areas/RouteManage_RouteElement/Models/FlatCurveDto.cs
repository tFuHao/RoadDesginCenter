using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteManage_RouteElement.Models
{
    public class FlatCurveDto
    {
        public string FlatCurveId { get; set; }
        public string RouteId { get; set; }
        public int? FlatCurveType { get; set; }
        public int? IntersectionNumber { get; set; }
        public int? CurveNumber { get; set; }
        public double? FlatCurveLength { get; set; }
        public double? BeginStake { get; set; }
        public double? EndStake { get; set; }
        public string Description { get; set; }
    }
}
