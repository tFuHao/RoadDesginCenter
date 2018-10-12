using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class BrokenChainage
    {
        public string BrokenId { get; set; }
        [Required(ErrorMessage = "所属路线不能为空")]
        public string RouteId { get; set; }
        public int? SerialNumber { get; set; }
        public int? BrokenType { get; set; }
        [Required(ErrorMessage = "来向（断前）桩号不能为空")]
        public double? FrontStake { get; set; }
        [Required(ErrorMessage = "去向（断后）桩号不能为空")]
        public double? AfterStake { get; set; }
        public string Description { get; set; }
        public string RawStakeBack { get; set; }
    }
}
