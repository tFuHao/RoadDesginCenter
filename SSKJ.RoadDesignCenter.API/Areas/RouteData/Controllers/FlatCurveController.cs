using System;
using System.Collections.Generic;
using System.IO;
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
    [Route("api/FlatCurve/[action]")]
    [Area("RouteData")]
    public class FlatCurveController : BaseController
    {
        public IFlatCurveBusines FlatBus;

        public HostingEnvironment HostingEnvironmentost;

        public FlatCurveController(IFlatCurveBusines flatCurve, HostingEnvironment hostingEnvironmentost)
        {
            FlatBus = flatCurve;
            HostingEnvironmentost = hostingEnvironmentost;
        }

        public async Task<IActionResult> Get(int pageSize, int pageIndex, string routeId)
        {
            try
            {
                var result = await FlatBus.GetListAsync(e => e.RouteId == routeId, e => e.FlatCurveId, true, pageSize,
                pageIndex, UserInfo.DataBaseName);
                return SuccessData(new
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

        public async Task<IActionResult> Insert(FlatCurveDto input, string routeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (input.FlatCurveId == null)
                    {
                        input.FlatCurveId = Guid.NewGuid().ToString();
                        var entity = MapperUtils.MapTo<FlatCurveDto, FlatCurve>(input);
                        entity.RouteId = routeId;
                        var result = await FlatBus.CreateAsync(entity, UserInfo.DataBaseName);
                        if (result)
                            return SuccessMes();
                        return Fail();
                    }
                    else
                    {
                        var entity = await FlatBus.GetEntityAsync(e => e.FlatCurveId == input.FlatCurveId, UserInfo.DataBaseName);
                        entity.FlatCurveType = input.FlatCurveType;
                        entity.IntersectionNumber = input.IntersectionNumber;
                        entity.CurveNumber = input.CurveNumber;
                        entity.FlatCurveLength = input.FlatCurveLength;
                        entity.BeginStake = input.BeginStake;
                        entity.EndStake = input.EndStake;
                        entity.Description = input.Description;
                        var result = await FlatBus.UpdateAsync(entity, UserInfo.DataBaseName);
                        if (result)
                            return SuccessMes();
                        return Fail();
                    }
                }
                return Fail();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }

        }

        public async Task<IActionResult> Delete(List<FlatCurve> list)
        {
            try
            {
                if (list.Count > 0)
                {
                    var result = await FlatBus.DeleteAsync(list, UserInfo.DataBaseName);
                    if (result)
                        return SuccessMes();
                }
                return Fail();
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
                if (!string.IsNullOrEmpty(routeId))
                {
                    var content = "";
                    var list = await FlatBus.GetListAsync(e => e.RouteId == routeId, UserInfo.DataBaseName);
                    foreach (var item in list)
                    {
                        content += $"{item.FlatCurveType},{item.IntersectionNumber},{item.CurveNumber},{item.FlatCurveLength},{item.BeginStake},{item.EndStake},{item.Description},\n";
                    }

                    if (content != "")
                        content.Substring(0, content.Length - 2);
                    return SuccessMes(content);
                }
                return Fail();
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
                if (string.IsNullOrEmpty(routeId))
                    return Fail();
                var file = Request.Form.Files;
                if (file == null)
                    return Fail();
                var success = 0;
                var error = 0;
                var path = FileUtils.SaveFile(HostingEnvironmentost.WebRootPath, file[0], UserInfo.UserId);
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
                        var result = await FlatBus.CreateAsync(model, UserInfo.DataBaseName);
                        if (result)
                            success++;
                        else error++;
                    }
                }
                read.Close();
                FileUtils.DeleteFile(path);
                return SuccessMes($"平曲线数据导入成功{success}条，失败{error}条");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}