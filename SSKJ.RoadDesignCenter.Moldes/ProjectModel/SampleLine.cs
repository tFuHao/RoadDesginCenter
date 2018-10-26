using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class SampleLine
    {
        public string SampleLineId { get; set; }
        public int? SerialNumber { get; set; }
        [Required(ErrorMessage = "桩号不能为空")]
        public double? Stake { get; set; }
        [Required(ErrorMessage = "左偏距不能为空")]
        public double? LeftOffset { get; set; }
        [Required(ErrorMessage = "右偏距不能为空")]
        public double? RightOffset { get; set; }
    }
}
