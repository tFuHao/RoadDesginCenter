using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.ProjectModel
{
    public partial class Authorize
    {
        public string AuthorizeId { get; set; }
        public int? Category { get; set; }
        public string ObjectId { get; set; }
        public int? ItemType { get; set; }
        public string ItemId { get; set; }
        public int? SortCode { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateUserId { get; set; }
    }
}
