using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RouteCalculate.CalculateModels
{
    public class CalcParams
    {
        public string RouteId { get; set; }
        public double? BeginStake { get; set; }
        public double? EndStake { get; set; }
        public double? Interval { get; set; }
        public double? Dist { get; set; }
        public double? RightCorner { get; set; }
        public List<Coord> Coords { get; set; }
        public List<double> Stakes { get; set; }
        public bool FlatCurveMainPoint { get; set; }
        public bool VerticalCurveMainPoint { get; set; }
        public bool AddStake { get; set; }
    }

    public class Coord
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
