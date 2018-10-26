using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class FlatCurve_CurveElement
    {
        public string CurveElementId { get; set; }
        public string FlatCurveId { get; set; }
        public int? SerialNumber { get; set; }
        [Required(ErrorMessage = "桩号不能为空")]
        public double? Stake { get; set; }
        [Required(ErrorMessage = "X坐标不能为空")]
        public double? X { get; set; }
        [Required(ErrorMessage = "Y坐标不能为空")]
        public double? Y { get; set; }
        [Required(ErrorMessage = "方位角不能为空")]
        public double? Azimuth { get; set; }
        [Required(ErrorMessage = "线型及转向不能为空")]
        public int? TurnTo { get; set; }
        [Required(ErrorMessage = "半径不能为空")]
        public double? R { get; set; }
        public string Description { get; set; }
    }
}
