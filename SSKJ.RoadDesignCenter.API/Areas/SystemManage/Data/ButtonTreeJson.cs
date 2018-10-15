using SSKJ.RoadDesignCenter.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.API.Areas.SystemManage.Data
{
    public static class ButtonTreeJson
    {
        public static string TreeGridJson(this List<ModuleButton> list, string ParentId = "0")
        {
            StringBuilder strJson = new StringBuilder();
            List<ModuleButton> item = list.FindAll(t => t.ParentId == ParentId);
            strJson.Append("[");
            if (item.Count > 0)
            {
                item.ForEach(entity =>
                {
                    strJson.Append("{");
                    strJson.Append("\"ModuleButtonId\":\"" + entity.ModuleButtonId + "\",");
                    strJson.Append("\"ModuleId\":\"" + entity.ModuleId + "\",");
                    strJson.Append("\"ParentId\":\"" + entity.ParentId + "\",");
                    strJson.Append("\"FullName\":\"" + entity.FullName + "\",");
                    strJson.Append("\"EnCode\":\"" + entity.EnCode + "\",");
                    strJson.Append("\"SortCode\":\"" + entity.SortCode + "\",");
                    strJson.Append("\"Description\":\"" + entity.Description + "\",");
                    strJson.Append("\"children\":" + TreeGridJson(list, entity.ModuleButtonId) + "");
                    strJson.Append("},");
                });
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }
    }
}