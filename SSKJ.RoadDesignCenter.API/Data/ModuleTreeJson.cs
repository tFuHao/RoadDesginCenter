using SSKJ.RoadDesignCenter.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.API.Data
{
    public static class ModuleTreeJson
    {
        public static string TreeGridJson(this List<Module> list, string ParentId = "0")
        {
            StringBuilder strJson = new StringBuilder();
            List<Module> item = list.FindAll(t => t.ParentId == ParentId);
            strJson.Append("[");
            if (item.Count > 0)
            {
                item.ForEach(entity =>
                {
                    strJson.Append("{");
                    strJson.Append("\"ModuleId\":\"" + entity.ModuleId + "\",");
                    strJson.Append("\"ParentId\":\"" + entity.ParentId + "\",");
                    strJson.Append("\"FullName\":\"" + entity.FullName + "\",");
                    strJson.Append("\"Target\":\"" + entity.Target + "\",");
                    strJson.Append("\"IsMenu\":\"" + entity.IsMenu + "\",");
                    strJson.Append("\"AllowExpand\":\"" + entity.AllowExpand + "\",");
                    strJson.Append("\"EnabledMark\":\"" + entity.EnabledMark + "\",");
                    strJson.Append("\"SortCode\":\"" + entity.SortCode + "\",");
                    strJson.Append("\"Description\":\"" + entity.Description + "\",");
                    strJson.Append("\"children\":" + TreeGridJson(list, entity.ModuleId) + "");
                    strJson.Append("},");
                });
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }
    }
}
