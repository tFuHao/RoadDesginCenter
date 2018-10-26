using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.API.Controllers;
using SSKJ.RoadDesignCenter.API.Data;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Models.SystemModel;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteManage_RoutInfo
{
    [Route("api/RouteManage/[action]")]
    [Area("RouteManage_RoutInfo")]
    public class RouteManageController : BaseController
    {
        public readonly IRouteBusines routeBll;

        public RouteManageController(IRouteBusines routeBll)
        {
            this.routeBll = routeBll;
        }

        public async Task<IActionResult> Get()
        {
            var data = await routeBll.GetListAsync(GetUserInfo().DataBaseName);
            var result = data.OrderBy(o => o.CreateDate).ToList().RouteTreeGridJson();
            return Ok(result);
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
                    entity.CreateUserId = GetUserInfo().UserId;

                    result = await routeBll.CreateAsync(entity, GetUserInfo().DataBaseName);
                }
                else
                {
                    result = await routeBll.UpdateAsync(entity, GetUserInfo().DataBaseName);
                }
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string routeId)
        {
            try
            {
                var list = await routeBll.GetListAsync(GetUserInfo().DataBaseName);
                var routes = GetRoutes(list.ToList(), routeId);
                routes.Add(list.Single(m => m.RouteId == routeId));

                var result = await routeBll.DeleteAsync(routes, GetUserInfo().DataBaseName);
                return Ok(result);
            }
            catch (Exception)
            {
                return BadRequest(false);
            }
        }

        public List<Route> GetRoutes(List<Route> list, string pId)
        {
            var _list = list.Where(f => f.ParentId == pId).ToList();

            return _list.Concat(_list.SelectMany(t => GetRoutes(list, t.RouteId))).ToList();
        }
    }
}