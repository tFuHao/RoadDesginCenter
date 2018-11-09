using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteData.Models
{
    public class FlatCurveCurveElementDto
    {
        public string CurveElementId { get; set; }
        public string RouteId { get; set; }
        public int? SerialNumber { get; set; }
        [Required(ErrorMessage = "桩号不能为空")]
        public double? Stake { get; set; }
        public double? X { get; set; }
        public double? Y { get; set; }
        public double? Azimuth { get; set; }
        public int? TurnTo { get; set; }
        public double? R { get; set; }
        public string Description { get; set; }
    }
}
