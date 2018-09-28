using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteManage_RouteElement.Models
{
    public class RouteDto
    {
        public string RouteId { get; set; }
        public string ParentId { get; set; }
        public string ProjectId { get; set; }
        public string RouteName { get; set; }
        public double? RouteLength { get; set; }
        public double? StartStake { get; set; }
        public double? EndStake { get; set; }
        public int? RouteType { get; set; }
        public string Description { get; set; }
        public string DesignSpeed { get; set; }
        public IEnumerable<RouteDto> Children { get; set; }
    }
}