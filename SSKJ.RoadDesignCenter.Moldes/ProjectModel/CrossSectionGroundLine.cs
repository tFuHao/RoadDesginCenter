using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class CrossSectionGroundLine
    {
        public string CrossSectionGroundLineId { get; set; }
        public string RouteId { get; set; }
        [Required(ErrorMessage = "桩号不能为空")]
        public double? Stake { get; set; }
    }
}
