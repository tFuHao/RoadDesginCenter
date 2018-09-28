using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.API.Areas.RouteManage_RouteElement.Models;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteManage_RoutInfo
{
    [Route("api/RouteManage/[action]")]
    [Area("RouteManage_RoutInfo")]
    public class RouteManageController : Controller
    {
        public IRouteBusines RouteBus;

        public HostingEnvironment HostingEnvironmentost;

        public string ConStr = "server=139.224.200.194;port=3306;database=road_project_001;user id=root;password=SSKJ*147258369";

        public RouteManageController(IRouteBusines routeBus, HostingEnvironment hostingEnvironmentost)
        {
            RouteBus = routeBus;
            HostingEnvironmentost = hostingEnvironmentost;
        }

        public enum WayType
        {
            Parent, Child
        }

        public async Task<IActionResult> Get(int pageSize, int pageIndex)
        {
            var list = await RouteBus.GetListAsync(e => e.ParentId == null, e => e.RouteId, true, pageSize, pageIndex, ConStr);
            var result = new List<RouteDto>();
            var test = new RouteDto()
            {
                RouteId = "test",
                ParentId = list.Item1.ToList()[0].RouteId,
                RouteName = "Test"
            };
            list.Item1.ToList().ForEach(i =>
            {
                var temp = Mapper.Map<Route, RouteDto>(i);
                temp.Children = new List<RouteDto>(){test};
                result.Add(temp);
            });
            return Json(new
            {
                data = result,
                count = list.Item2
            });
        }

        public async Task<IActionResult> GetChildren(string parentId)
        {
            if (!string.IsNullOrEmpty(parentId))
            {
                var result = await RouteBus.GetListAsync(e => e.ParentId == parentId, ConStr);
                return Json(result);
            }
            return null;
        }

        /// <summary>
        /// 添加 或 插入 断链数据方法
        /// </summary>
        /// <param name="input">添加 或 插入 的数据</param>
        /// <param name="wayType">线路 或 线路组</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(Route input, WayType wayType)
        {
            if (ModelState.IsValid)
            {
                //添加 路线 或 线路组
                if (input.RouteId == null)
                {
                    var result = false;
                    //添加线路组
                    if (wayType == WayType.Parent)
                    {
                        var insert = new Route()
                        {
                            RouteId = Guid.NewGuid().ToString(),
                            ProjectId = null,
                            RouteLength = null,
                            RouteName = input.RouteName,
                            Description = input.Description
                        };
                        result = await RouteBus.CreateAsync(input, ConStr);
                    }
                    //添加线路
                    else if (wayType == WayType.Child)
                    {
                        input.RouteId = Guid.NewGuid().ToString();
                        result = await RouteBus.CreateAsync(input, ConStr);
                    }
                    return Json(result);
                }
                else
                {
                    //更新
                    var entity = await RouteBus.GetEntityAsync(e => e.RouteId == input.RouteId, ConStr);
                    if (entity == null)
                        return null;
                    if (wayType == WayType.Parent)
                    {
                        entity.ParentId = input.ParentId;
                        entity.ProjectId = input.ProjectId;
                        entity.RouteName = input.RouteName;
                        entity.Description = input.Description;
                    }else if (wayType == WayType.Child)
                    {
                        entity.ParentId = input.ParentId;
                        entity.RouteName = input.RouteName;
                        entity.RouteLength = input.RouteLength;
                        entity.StartStake = input.StartStake;
                        entity.EndStake = input.EndStake;
                        entity.RouteType = input.RouteType;
                        entity.Description = input.Description;
                        entity.DesignSpeed = input.DesignSpeed;
                    }
                    var result = await RouteBus.UpdateAsync(entity, ConStr);
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

        /// <summary>
        /// 删除断链数据
        /// </summary>
        /// <param name="list">删除的实体对象列表</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(List<Route> list)
        {
            if (list.Any())
            {
                var result = await RouteBus.DeleteAsync(list, ConStr);
                return Json(result);
            }
            return Json(false);
        }
    }
}