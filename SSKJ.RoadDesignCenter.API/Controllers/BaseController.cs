using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using SSKJ.RoadDesignCenter.API.Models;

namespace SSKJ.RoadDesignCenter.API.Controllers
{
    public class BaseController : Controller
    {
        public UserInfoModel userInfo;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                string strToken = "";
                if (Request.Headers.TryGetValue("x-access-token", out StringValues token))
                    strToken = token.ToString();
                var userInfo = Utility.Tools.TokenUtils.ToObject<UserInfoModel>(strToken);

                this.userInfo = userInfo;
            }
            catch (Exception)
            {

            }
            base.OnActionExecuting(context);
        }

        public UserInfoModel GetUserInfo()
        {
            return userInfo;
        }
    }
}