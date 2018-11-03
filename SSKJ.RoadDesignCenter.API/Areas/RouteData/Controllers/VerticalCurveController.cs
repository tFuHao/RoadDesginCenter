using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.API.Areas.RouteData.Models;
using SSKJ.RoadDesignCenter.API.Controllers;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Utility.Tools;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteData.Controllers
{
    [Route("api/VerticalCurve/[action]")]
    [Area("RouteData")]
    public class VerticalCurveController : BaseController
    {
        private readonly IVerticalCurveBusines VerticalBus;

        private readonly HostingEnvironment Host;

        public VerticalCurveController(IVerticalCurveBusines verticalBus, HostingEnvironment host)
        {
            VerticalBus = verticalBus;
            Host = host;
        }

        public async Task<IActionResult> Get(int pageSize, int pageIndex, string routeId)
        {
            try
            {
                var result = await VerticalBus.GetListAsync(e => e.RouteId == routeId, e => e.VerticalCurveId, true, pageSize, pageIndex, UserInfo.DataBaseName);
                return Success(new
                {
                    data = result.Item1,
                    count = result.Item2
                });
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Insert(VerticalCurveInputDto input, string routeId)
        {
            try
            {
                if (input.VerticalCurveId == null)
                {
                    input.VerticalCurveId = Guid.NewGuid().ToString();
                    input.RouteId = routeId;
                    var entity = MapperUtils.MapTo<VerticalCurveInputDto, VerticalCurve>(input);
                    var result = await VerticalBus.CreateAsync(entity, UserInfo.DataBaseName);
                    return Success(result);
                }
                else
                {
                    var entity = await VerticalBus.GetEntityAsync(e => e.VerticalCurveId == input.VerticalCurveId, UserInfo.DataBaseName);
                    entity.VerticalCurveType = input.VerticalCurveType;
                    entity.GradeChangePointNumber = input.GradeChangePointNumber;
                    entity.CurveNumber = input.CurveNumber;
                    entity.VerticalCurveLength = input.VerticalCurveLength;
                    entity.BeginStake = input.BeginStake;
                    entity.EndStake = input.EndStake;
                    entity.Description = input.Description;
                    var result = await VerticalBus.UpdateAsync(entity, UserInfo.DataBaseName);
                    return Success(result);
                }
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(List<VerticalCurve> list)
        {
            try
            {
                if (!list.Any())
                    return Fail();
                var result = await VerticalBus.DeleteAsync(list, UserInfo.DataBaseName);
                return Success(result);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        public async Task<IActionResult> Export(string routeId)
        {
            try
            {
                if (string.IsNullOrEmpty(routeId))
                    return Fail();
                var content = "";
                var list = await VerticalBus.GetListAsync(e => e.RouteId == routeId, UserInfo.DataBaseName);
                foreach (var item in list)
                {
                    content += $"{item.VerticalCurveType},{item.GradeChangePointNumber},{item.CurveNumber},{item.VerticalCurveLength},{item.BeginStake},{item.EndStake},{item.Description},\n";
                }
                if (content == "")
                    return Error("当前没有数据");
                content = content.Substring(0, content.Length - 2);
                return Success(content);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        public async Task<IActionResult> Import(string routeId)
        {
            try
            {
                var files = Request.Form.Files;
                if (files == null || string.IsNullOrEmpty(routeId))
                    return Fail();
                var success = 0;
                var error = 0;
                var path = FileUtils.SaveFile(Host.WebRootPath, files[0], UserInfo.UserId);
                var read = new StreamReader(path, Encoding.Default);
                string line;
                while ((line = read.ReadLine()) != null)
                {
                    var array = line.Split(",");
                    var entity = new VerticalCurve()
                    {
                        VerticalCurveId = Guid.NewGuid().ToString(),
                        RouteId = routeId,
                        VerticalCurveType = Convert.ToInt32(array[0]),
                        GradeChangePointNumber = Convert.ToInt32(array[1]),
                        CurveNumber = Convert.ToInt32(array[2]),
                        VerticalCurveLength = Convert.ToDouble(array[3]),
                        BeginStake = Convert.ToDouble(array[4]),
                        EndStake = Convert.ToDouble(array[5]),
                        Description = array[6]
                    };
                    var valid = TryValidateModel(entity);
                    if (!valid)
                        return Fail();
                    var result = await VerticalBus.CreateAsync(entity, UserInfo.DataBaseName);
                    if (result)
                        success++;
                    else
                        error++;
                    read.Close();
                    FileUtils.DeleteFile(path);
                    return Success($"导入成功{success}条，失败{error}条");
                }
                return Fail();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}