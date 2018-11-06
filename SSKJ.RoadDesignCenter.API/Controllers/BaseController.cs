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
        public UserInfoModel UserInfo;
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string strToken = "";
            if (Request.Headers.TryGetValue("x-access-token", out StringValues token))
                strToken = token.ToString();

            if (string.IsNullOrEmpty(strToken))
            {
                context.Result = new BadRequestObjectResult(new { type = -2, message = "登录超时，请重新登录!" });
                return;
            }

            var userInfo = Utility.Tools.TokenUtils.ToObject<UserInfoModel>(strToken);
            if (string.IsNullOrEmpty(userInfo.UserId))
            {
                context.Result = new BadRequestObjectResult(new { type = -2, message = "登录超时，请重新登录!" });
                return;
            }
            UserInfo = userInfo;
            base.OnActionExecuting(context);
        }

        //成功
        public IActionResult SuccessData(object data)
        {
            return Ok(new { type = 1, data });
        }
        public IActionResult SuccessMes(string message = "")
        {
            if (string.IsNullOrEmpty(message))
                return Ok(new { type = 1, message = "操作成功!" });
            else
                return Ok(new { type = 1, message });           
        }
        //失败
        public IActionResult Fail(string message = "")
        {
            if (string.IsNullOrEmpty(message))
                return Ok(new { type = 0, message = "操作失败!" });
            else
                return Ok(new { type = 0, message });
        }
        //错误
        public IActionResult Error(string message)
        {
            return BadRequest(new { type = -1, message });
        }
    }
}