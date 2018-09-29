using SSKJ.RoadDesignCenter.Models.ProjectModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.API.Data
{
    public static class TreeJson
    {
        public static string RouteTreeGridJson(this List<Route> list, string ParentId = null)
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
                    strJson.Append("\"ProjectId\":\"" + entity.ProjectId + "\",");
                    strJson.Append("\"RouteName\":\"" + entity.RouteName + "\",");
                    strJson.Append("\"RouteLength\":\"" + entity.RouteLength + "\",");
                    strJson.Append("\"StartStake\":\"" + entity.StartStake + "\",");
                    strJson.Append("\"EndStake\":\"" + entity.EndStake + "\",");
                    strJson.Append("\"Description\":\"" + entity.Description + "\",");
                    strJson.Append("\"DesignSpeed\":\"" + entity.DesignSpeed + "\",");
                    strJson.Append("\"Children\":" + RouteTreeGridJson(list, entity.RouteId) + "");
                    strJson.Append("},");
                });
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }
    }
}
