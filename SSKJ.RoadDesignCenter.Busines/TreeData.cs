using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Busines
{
    public static class TreeData
    {
        public static string ModuleTreeJson(this List<Module> list, string ParentId = "0")
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
                    strJson.Append("\"EnCode\":\"" + entity.EnCode + "\",");
                    strJson.Append("\"FullName\":\"" + entity.FullName + "\",");
                    strJson.Append("\"Target\":\"" + entity.Target + "\",");
                    strJson.Append("\"IsMenu\":\"" + entity.IsMenu + "\",");
                    strJson.Append("\"Icon\":\"" + entity.Icon + "\",");
                    strJson.Append("\"AllowCache\":\"" + entity.AllowCache + "\",");
                    strJson.Append("\"EnabledMark\":\"" + entity.EnabledMark + "\",");
                    strJson.Append("\"SortCode\":\"" + entity.SortCode + "\",");
                    strJson.Append("\"UrlAddress\":\"" + entity.UrlAddress + "\",");
                    strJson.Append("\"Description\":\"" + entity.Description + "\",");
                    strJson.Append("\"children\":" + ModuleTreeJson(list, entity.ModuleId) + "");
                    strJson.Append("},");
                });
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }

        public static string ButtonTreeJson(this List<ModuleButton> list, string ParentId = "0")
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
                    strJson.Append("\"children\":" + ButtonTreeJson(list, entity.ModuleButtonId) + "");
                    strJson.Append("},");
                });
                strJson = strJson.Remove(strJson.Length - 1, 1);
            }
            strJson.Append("]");
            return strJson.ToString();
        }
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
