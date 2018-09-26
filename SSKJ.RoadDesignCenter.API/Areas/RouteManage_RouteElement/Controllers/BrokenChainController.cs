using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteManage_RouteElement.Controllers
{
    [Route("api/BrokenChain/[action]")]
    [Area("RouteManage_RouteElement")]
    public class BrokenChainController : Controller
    {
        public IBrokenChainageBusines BrokenBus;

        public string ConStr = "server=139.224.200.194;port=3306;database=road_project_001;user id=root;password=SSKJ*147258369";

        public BrokenChainController(IBrokenChainageBusines brokenBus)
        {
            BrokenBus = brokenBus;
        }

        public async Task<IActionResult> Get(int pageSize, int pageIndex)
        {
            var result = await BrokenBus.GetListAsync(e => true, e => e.BrokenId, true, pageSize, pageIndex, ConStr);
            return Json(new
            {
                data = result.Item1,
                count = result.Item2
            });
        }

        [HttpPost]
        public async Task<IActionResult> Insert(BrokenChainage input, int serialNumber)
        {
            if (ModelState.IsValid)
            {
                if (input.BrokenId == null)
                {
                    var allList = await BrokenBus.GetListAsync(ConStr);
                    var count = allList.Count();
                    input.BrokenId = Guid.NewGuid().ToString();
                    input.SerialNumber = count + 1;
                    if (serialNumber != 0)
                    {
                        var temp = await BrokenBus.GetListAsync(ConStr);
                        var list = temp.ToList();
                        list.ForEach(i =>
                        {
                            i.SerialNumber++;
                            BrokenBus.UpdateAsync(i);
                        });
                        input.SerialNumber = serialNumber;
                    }
                    return Json(await BrokenBus.CreateAsync(input));
                }
                else
                {
                    var entity = await BrokenBus.GetEntityAsync(e => e.BrokenId == input.BrokenId);
                    if (entity == null)
                        return null;
                    entity.FrontStake = input.FrontStake;
                    entity.AfterStake = input.AfterStake;
                    entity.SerialNumber = input.SerialNumber;
                    return Json(await BrokenBus.UpdateAsync(entity));
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
    }
}