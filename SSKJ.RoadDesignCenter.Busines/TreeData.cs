using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Busines
{
    public static class TreeData
    {
        public static string RouteTreeJson(this List<Route> list, string ParentId = "0")
        {
            StringBuilder strJson = new StringBuilder();
            List<Route> item = list.FindAll(t => t.ParentId == ParentId);
            strJson.Append("[");
            if (item.Count > 0)
            {
                item.ForEach(entity =>
                {
                    strJson.Append("{");
                    strJson.Append("\"RouteId\":\"" + entity.RouteId + "\",");
                    strJson.Append("\"ParentId\":\"" + entity.ParentId + "\",");
                    strJson.Append("\"RouteName\":\"" + entity.RouteName + "\",");
                    strJson.Append("\"RouteType\":\"" + entity.RouteType + "\",");
                    strJson.Append("\"RouteLength\":\"" + entity.RouteLength + "\",");
                    strJson.Append("\"StartStake\":\"" + entity.StartStake + "\",");
                    strJson.Append("\"EndStake\":\"" + entity.EndStake + "\",");
                    strJson.Append("\"Description\":\"" + entity.Description + "\",");
                    strJson.Append("\"CreateDate\":\"" + entity.CreateDate + "\",");
                    strJson.Append("\"DesignSpeed\":\"" + entity.DesignSpeed + "\",");
                    strJson.Append("\"children\":" + RouteTreeJson(list, entity.RouteId) + "");
                    strJson.Append("},");
                });
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }
    }
}
