using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RouteCalculate.CalculateModels
{
   public class BrokenChainage
    {
        public string BrokenId { get; set; }
        public string RouteId { get; set; }
        public int? SerialNumber { get; set; }
        public int? BrokenType { get; set; }
        public double? FrontStake { get; set; }
        public double? AfterStake { get; set; }
        public string Description { get; set; }
        public string RawStakeBack { get; set; }
    }
}
