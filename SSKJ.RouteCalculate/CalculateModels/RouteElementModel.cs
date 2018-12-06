using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RouteCalculate.CalculateModels
{
   public class RouteElementModel
    {
        public IEnumerable<BrokenChainage> BrokenChainages { get; set; }
        public IEnumerable<Intersection> Intersections { get; set; }
        public IEnumerable<GradeChangePoint> GradeChangePoints { get; set; }
    }
}
