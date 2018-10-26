using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class Stake
    {
        public string StakeId { get; set; }
        public int? SerialNumber { get; set; }
        [Required(ErrorMessage = "桩号不能为空")]
        public double? StakeName { get; set; }
        [Required(ErrorMessage = "偏距不能为空")]
        public double? Offset { get; set; }
        [Required(ErrorMessage = "右转角不能为空")]
        public double? RightCorner { get; set; }
    }
}
