using System.Collections.Generic;
using System.Text;
using SSKJ.RoadDesignCenter.Models.SystemModel;

namespace SSKJ.RoadDesignCenter.API.Areas.AuthorizeManage.Data
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
                    strJson.Append("\"FullName\":\"" + entity.FullName + "\",");
                    strJson.Append("\"SortCode\":\"" + entity.SortCode + "\",");
                    strJson.Append("\"Children\":" + TreeGridJson(list, entity.ModuleId) + "");
                    strJson.Append("},");
                });
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }
    }
}
