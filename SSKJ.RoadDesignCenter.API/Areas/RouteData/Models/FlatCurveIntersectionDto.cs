﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteData.Models
{
    public class FlatCurveIntersectionDto
    {
        public string IntersectionPointId { get; set; }
        public string RouteId { get; set; }
        public int? SerialNumber { get; set; }
        [Required(ErrorMessage = "交点名不能为空")]
        public string IntersectionName { get; set; }
        public double? Stake { get; set; }
        [Required(ErrorMessage = "X坐标不能为空")]
        public double? X { get; set; }
        [Required(ErrorMessage = "Y坐标不能为空")]
        public double? Y { get; set; }
        public double? R { get; set; }
        public double? Ls1 { get; set; }
        public double? Ls2 { get; set; }
        public double? Ls1R { get; set; }
        public double? Ls2R { get; set; }
    }
}