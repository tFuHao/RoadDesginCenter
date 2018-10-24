using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Models
{
    public class TreeEntity
    {
        public string Id { get; set; }
        public string ParentId { get; set; }
        public string Name { get; set; }
        public int? CheckState { get; set; }
    }
}
