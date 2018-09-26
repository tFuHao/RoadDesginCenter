using System;
using System.Collections.Generic;

namespace SSKJ.RoadDesignCenter.Models.SystemModel
{
    public partial class SysLog
    {
        public string LogId { get; set; }
        public int? CategoryId { get; set; }
        public string SourceObjectId { get; set; }
        public string SourceContentJson { get; set; }
        public DateTime? OperateTime { get; set; }
        public string OperateUserId { get; set; }
        public string OperateAccount { get; set; }
        public string OperateTypeId { get; set; }
        public string OperateType { get; set; }
        public string ModuleId { get; set; }
        public string Module { get; set; }
        public string Ipaddress { get; set; }
        public string IpaddressName { get; set; }
        public string Host { get; set; }
        public string Browser { get; set; }
        public int? ExecuteResult { get; set; }
        public string ExecuteResultJson { get; set; }
        public string Description { get; set; }
        public int? DeleteMark { get; set; }
        public int? EnabledMark { get; set; }
    }
}
