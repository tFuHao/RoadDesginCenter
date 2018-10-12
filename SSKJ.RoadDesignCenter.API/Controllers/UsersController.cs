using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using SSKJ.RoadDesignCenter.API.Models;
using SSKJ.RoadDesignCenter.IBusines.Project;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Utility;

namespace SSKJ.RoadDesignCenter.API.Controllers
{
    [Route("api/Users/[action]")]
    public class UsersController : Controller
    {
        private readonly IUserBusines prjUserBll;
        private readonly IRoleBusines roleBusines;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly HostingEnvironment Host;

        public UsersController(IRoleBusines roleBusines, IUserBusines prjUserBll, IHttpContextAccessor httpContextAccessor, HostingEnvironment host)
        {
            this.prjUserBll = prjUserBll;
            this.httpContextAccessor = httpContextAccessor;
            CurrentUser.Configure(httpContextAccessor);
            this.Host = host;
            this.roleBusines = roleBusines;
        }

        public async Task<IActionResult> Index()
        {
            var connectionString = CurrentUser.UserConnectionString;
            if (connectionString == null)
            {
                return RedirectToAction("Index", "Login");
            }
            var user = await prjUserBll.GetListAsync(connectionString);
            var users = await prjUserBll.GetListAsync(u => true, u => u.UserId, false, 30, 1, connectionString);

            return View(users.Item1);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProUser(User input)
        {
            if (ModelState.IsValid)
            {
                if (input.UserId == null)
                {
                    input.UserId = Guid.NewGuid().ToString();
                    input.CreateDate = DateTime.Now;
                    input.CreateUserId = GetUserId();
                    input.HeadIcon = SaveHead(input.HeadIcon);
                    input.EnabledMark = 1;
                    var result = await prjUserBll.CreateAsync(input, GetConStr());
                    return Json(result);
                }
                else
                {
                    var entity = await prjUserBll.GetEntityAsync(e => e.UserId == input.UserId, GetConStr());
                    if (entity == null)
                        return null;
                    entity.Account = input.Account;
                    entity.RealName = input.RealName;
                    if (entity.HeadIcon != input.HeadIcon)
                    {
                        entity.HeadIcon = SaveHead(input.HeadIcon);
                    }
                    entity.Gender = input.Gender;
                    entity.Mobile = input.Mobile;
                    entity.Birthday = input.Birthday;
                    entity.Email = input.Email;
                    entity.RoleId = input.RoleId;
                    entity.ModifyDate = DateTime.Now;
                    entity.ModifyUserId = GetUserId();
                    var result = await prjUserBll.UpdateAsync(entity, GetConStr());
                    return Json(result);
                }
            }

            foreach (var data in ModelState.Values)
            {
                if (data.Errors.Count > 0)
                {
                    return Json(data.Errors[0].ErrorMessage);
                }
            }

            return null;
        }

        public async Task<IActionResult> GetProUsers(int pageSize, int pageIndex)
        {
            var result = await prjUserBll.GetListAsync(e => true, e => e.UserId, true, pageSize, pageIndex, GetConStr());
            return Json(new
            {
                data = result.Item1,
                count = result.Item2
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeletePerson(List<User> list)
        {
            var result = false;
            if (list.Any())
            {
                result = await prjUserBll.DeleteAsync(list, GetConStr());
            }
            return Json(result);
        }

        /// <summary>
        /// 验证邮箱的唯一性
        /// </summary>
        /// <param name="email">将要注册的邮箱地址</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ValidatorEmail(string email)
        {
            var result = false;
            if (!string.IsNullOrEmpty(email))
            {
                var entity = await prjUserBll.GetEntityAsync(e => e.Email == email, GetConStr());
                if (entity != null)
                {
                    result = true;
                }
            }

            return Json(!result);
        }

        /// <summary>
        /// 验证联系电话的唯一性
        /// </summary>
        /// <param name="email">将要注册的联系电话</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ValidatorMobile(string mobile)
        {
            var result = false;
            if (!string.IsNullOrEmpty(mobile))
            {
                var entity = await prjUserBll.GetEntityAsync(e => e.Mobile == mobile, GetConStr());
                if (entity != null)
                {
                    result = true;
                }
            }

            return Json(!result);
        }

        [HttpPost]
        public async Task<IActionResult> GetRoleList()
        {
            var result = await roleBusines.GetListAsync(e => true, GetConStr());
            return Json(result);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeEnable(string userId)
        {
            var entity = await prjUserBll.GetEntityAsync(e => e.UserId == userId, GetConStr());
            if (entity.EnabledMark == 1)
            {
                entity.EnabledMark = 0;
            }
            else
            {
                entity.EnabledMark = 1;
            }
            var result = await prjUserBll.UpdateAsync(entity, GetConStr());
            return Json(result);
        }


        /// <summary>
        /// 从token中获得当前登录的用户ID  
        /// </summary>
        /// <returns></returns>
        public string GetUserId()
        {
            var result = "";
            try
            {
                string strToken = "";
                if (Request.Headers.TryGetValue("x-access-token", out StringValues token))
                    strToken = token.ToString();

                var userInfo = Utility.Tools.TokenUtils.ToObject<UserInfoModel>(strToken);
                result = userInfo.UserId;
            }
            catch (Exception)
            {

            }
            return result;
        }

        /// <summary>
        /// 从token中获得当前用户数据库名称
        /// </summary>
        /// <returns></returns>
        public string GetConStr()
        {
            var result = "";
            try
            {
                string strToken = "";
                if (Request.Headers.TryGetValue("x-access-token", out StringValues token))
                    strToken = token.ToString();

                var userInfo = Utility.Tools.TokenUtils.ToObject<UserInfoModel>(strToken);

                result = userInfo.dataBaseName;
            }
            catch (Exception)
            {

            }
            return result;
        }

        /// <summary>
        /// base64代码转图片并保存，返回保存的路径
        /// </summary>
        /// <param name="base64Img">base64编码</param>
        /// <returns></returns>
        public string SaveHead(string base64Img)
        {
            var base64 = "";
            base64 = base64Img.Replace("data:image/png;base64,", "").Replace("data:image/jgp;base64,", "").Replace("data:image/jpg;base64,", "").Replace("data:image/jpeg;base64,", "");
            byte[] bytes = Convert.FromBase64String(base64);
            var path = Host.WebRootPath + "/image/head/";
            if (Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var fileName = Guid.NewGuid();
            var filePath = path + fileName;
            System.IO.File.WriteAllBytes(filePath + ".png", bytes);
            return "/image/head/" + fileName.ToString() + ".png".Replace("\\", "/");
        }
    }
}