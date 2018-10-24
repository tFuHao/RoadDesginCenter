using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Models
{
    public static class TreeJson
    {
        public static string TreeToJson(this List<TreeEntity> list, string parentId = "0")
        {
            StringBuilder strJson = new StringBuilder();
            List<TreeEntity> item = list.FindAll(t => t.ParentId == parentId);
            strJson.Append("[");
            if (item.Count > 0)
            {
                foreach (TreeEntity entity in item)
                {
                    strJson.Append("{");
                    strJson.Append("\"Id\":\"" + entity.Id + "\",");
                    strJson.Append("\"ParentId\":\"" + entity.ParentId + "\",");
                    strJson.Append("\"Name\":\"" + entity.Name + "\",");
                    strJson.Append("\"children\":" + TreeToJson(list, entity.Id) + "");
                    strJson.Append("},");
                }
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }
    }
}
