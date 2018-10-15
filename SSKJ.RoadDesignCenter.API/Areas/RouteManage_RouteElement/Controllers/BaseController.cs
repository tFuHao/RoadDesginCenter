using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using SSKJ.RoadDesignCenter.API.Models;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteManage_RouteElement.Controllers
{
    public class BaseController : Controller
    {
        private string conStr;

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string strToken = "";
                if (Request.Headers.TryGetValue("x-access-token", out StringValues token))
                    strToken = token.ToString();

                var userInfo = Utility.Tools.TokenUtils.ToObject<UserInfoModel>(strToken);

                conStr = userInfo.DataBaseName;
            }
            catch (Exception)
            {
                
            }
            base.OnActionExecuting(context);
        }

        public string GetConStr()
        {
            return conStr;
        }
    }
}