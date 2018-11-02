using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.API.Areas.RouteManage_RouteElement.Models;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Utility.Tools;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteManage_RouteElement.Controllers
{
    [Route("api/FlatCurve/[action]")]
    [Area("RouteManage_RouteElement")]
    public class FlatCurveController : BaseController
    {
        private readonly IFlatCurveBusines FlatBus;

        public HostingEnvironment HostingEnvironmentost;

        public FlatCurveController(IFlatCurveBusines flatBus, HostingEnvironment hostingEnvironmentost)
        {
            FlatBus = flatBus;
            HostingEnvironmentost = hostingEnvironmentost;
        }

        public async Task<IActionResult> Get(int pageSize, int pageIndex, string routeId)
        {
            var result = await FlatBus.GetListAsync(e => e.RouteId == routeId, e => e.FlatCurveId, true, pageSize,
                pageIndex, GetConStr());
            return Json(new
            {
                data = result.Item1,
                count = result.Item2
            });
        }

        public async Task<IActionResult> Insert(FlatCurveDto input)
        {
            if (ModelState.IsValid)
            {
                if (input.FlatCurveId == null)
                {
                    input.FlatCurveId = Guid.NewGuid().ToString();
                    var entity = MapperUtils.MapTo<FlatCurveDto, FlatCurve>(input);
                    var result = await FlatBus.CreateAsync(entity, GetConStr());
                    return Ok(result);
                }
                else
                {
                    var entity = await FlatBus.GetEntityAsync(e => e.FlatCurveId == input.FlatCurveId, GetConStr());
                    entity.FlatCurveType = input.FlatCurveType;
                    entity.IntersectionNumber = input.IntersectionNumber;
                    entity.CurveNumber = input.CurveNumber;
                    entity.FlatCurveLength = input.FlatCurveLength;
                    entity.BeginStake = input.BeginStake;
                    entity.EndStake = input.EndStake;
                    entity.Description = input.Description;
                    var result = await FlatBus.UpdateAsync(entity, GetConStr());
                    return Ok(result);
                }
            }

            foreach (var data in ModelState.Values)
            {
                if (data.Errors.Count > 0)
                {
                    return BadRequest(data.Errors[0].ErrorMessage);
                }
            }

            return BadRequest();
        }

        public async Task<IActionResult> Delete(List<FlatCurve> list)
        {
            if (list.Count > 0)
            {
                var result = await FlatBus.DeleteAsync(list, GetConStr());
                return Ok(result);
            }

            return BadRequest();
        }

        public async Task<IActionResult> Export(string routeId)
        {
            if (!string.IsNullOrEmpty(routeId))
            {
                var content = "";
                var list = await FlatBus.GetListAsync(e => e.RouteId == routeId, GetConStr());
                foreach (var item in list)
                {
                    content += $"{item.FlatCurveType},{item.IntersectionNumber},{item.CurveNumber},{item.FlatCurveLength},{item.BeginStake},{item.EndStake},{item.Description},\n";
                }

                if (content != "")
                    content.Substring(0, content.Length - 2);
                return Content(content);
            }

            return BadRequest("无数据");
        }

        public async Task<IActionResult> Import(string routeId)
        {
            if (string.IsNullOrEmpty(routeId))
                return BadRequest();
            var file = Request.Form.Files;
            if (file == null)
                return BadRequest();
            var success = 0;
            var error = 0;
            var path = FileUtils.SaveFile(HostingEnvironmentost.WebRootPath, file[0]);
            var read = new StreamReader(path, Encoding.Default);
            string line;
            while ((line = read.ReadLine()) != null)
            {
                var tempList = line.Split(',');
                var entity = new FlatCurveDto()
                {
                    FlatCurveId = Guid.NewGuid().ToString(),
                    RouteId = routeId,
                    FlatCurveType = Convert.ToInt32(tempList[0]),
                    IntersectionNumber = Convert.ToInt32(tempList[1]),
                    CurveNumber = Convert.ToInt32(tempList[2]),
                    FlatCurveLength = Convert.ToDouble(tempList[3]),
                    BeginStake = Convert.ToDouble(tempList[4]),
                    EndStake = Convert.ToDouble(tempList[5]),
                    Description = tempList[6]
                };
                var valid = TryValidateModel(entity);
                if (!valid)
                {
                    error++;
                }
                else
                {
                    var model = MapperUtils.MapTo<FlatCurveDto, FlatCurve>(entity);
                    var result = await FlatBus.CreateAsync(model, GetConStr());
                    if (result)
                        success++;
                    else error++;
                }
            }

            return Ok($"平曲线数据导入成功{success}条，失败{error}条");
        }
    }
}