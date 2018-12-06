using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RouteCalculate.CalculateModels
{
    public class CenterCoord
    {
        public string Stake { get; set; }

        public double X { get; set; }

        public double Y { get; set; }
    }
    public class SideCoord
    {
        public string Stake { get; set; }
        public double Dist { get; set; }

        public double X { get; set; }

        public double Y { get; set; }
    }
    public class CenterH
    {
        public string Stake { get; set; }

        public double H { get; set; }
    }

    public class BackCalcStake
    {
        public string Stake { get; set; }

        public double Dist { get; set; }
    }
}
