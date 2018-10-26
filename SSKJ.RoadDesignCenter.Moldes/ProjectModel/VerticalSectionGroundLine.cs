using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class VerticalSectionGroundLine
    {
        public string Id { get; set; }
        public string RouteId { get; set; }
        public int? SerialNumber { get; set; }
        [Required(ErrorMessage = "桩号不能为空")]
        public double? Stake { get; set; }
        [Required(ErrorMessage = "高程不能为空")]
        public double? H { get; set; }
    }
}
