﻿using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class SampleLine
    {
        public string SampleLineId { get; set; }
        public string Stake { get; set; }
        public double? LeftOffset { get; set; }
        public double? RightOffset { get; set; }
    }
}