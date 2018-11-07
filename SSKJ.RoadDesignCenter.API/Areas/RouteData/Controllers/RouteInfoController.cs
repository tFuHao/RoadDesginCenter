using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.API.Controllers;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Models.SystemModel;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteData.Controllers
{
    [Route("api/RouteInfo/[action]")]
    [Area("RouteData")]
    public class RouteInfoController : BaseController
    {
        public readonly IRouteBusines routeBll;

        public RouteInfoController(IRouteBusines routeBll)
        {
            this.routeBll = routeBll;
        }

        public async Task<IActionResult> Get()
        {
            try
            {
                var data = await routeBll.GetRouteAuthorizes(2, UserInfo.RoleId, UserInfo.DataBaseName);
                return SuccessData(data);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveEntity(Route entity)
        {
            try
            {
                entity.RouteLength = entity.EndStake - entity.StartStake;
                var result = false;
                if (string.IsNullOrEmpty(entity.RouteId))
                {
                    entity.RouteId = Guid.NewGuid().ToString();
                    entity.CreateDate = DateTime.Now;
                    entity.CreateUserId = UserInfo.UserId;

                    result = await routeBll.CreateAsync(entity, UserInfo.DataBaseName);
                }
                else
                {
                    var model = await routeBll.GetEntityAsync(entity.RouteId, UserInfo.DataBaseName);
                    model.ParentId = entity.ParentId;
                    model.RouteName = entity.RouteName;
                    model.RouteType = entity.RouteType;
                    model.DesignSpeed = entity.DesignSpeed;
                    model.Description = entity.Description;
                    result = await routeBll.UpdateAsync(model, UserInfo.DataBaseName);
                }
                if (result)
                    return SuccessMes();
                return Fail();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string routeId)
        {
            try
            {
                var list = await routeBll.GetListAsync(UserInfo.DataBaseName);
                var routes = GetRoutes(list.ToList(), routeId);
                routes.Add(list.Single(m => m.RouteId == routeId));

                var result = await routeBll.DeleteAsync(routes, UserInfo.DataBaseName);
                if (result)
                    return SuccessMes();
                return Fail();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        public List<Route> GetRoutes(List<Route> list, string pId)
        {
            var _list = list.Where(f => f.ParentId == pId).ToList();

            return _list.Concat(_list.SelectMany(t => GetRoutes(list, t.RouteId))).ToList();
        }
    }
}