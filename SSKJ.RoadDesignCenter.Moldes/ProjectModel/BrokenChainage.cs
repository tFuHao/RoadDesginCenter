using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class BrokenChainage
    {
        public string BrokenId { get; set; }
        public string RouteId { get; set; }
        public int SerialNumber { get; set; }
        public int BrokenType { get; set; }
        public string FrontStake { get; set; }
        public string AfterStake { get; set; }
        public string Description { get; set; }
        public string RawStakeBack { get; set; }
    }
}
