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
    [Route("api/GradeChangePoint/[action]")]
    [Area("RouteData")]
    public class GradeChangePointController : BaseController
    {
        public IVerticalCurve_GradeChangePointBusines GradeBus;

        public HostingEnvironment Hosting;

        public GradeChangePointController(IVerticalCurve_GradeChangePointBusines gradeBus, HostingEnvironment hosting)
        {
            GradeBus = gradeBus;
            Hosting = hosting;
        }

        public async Task<IActionResult> Get(int pageSize, int pageIndex, string routeId)
        {
            try
            {
                var result = await GradeBus.GetListAsync(e => e.RouteId == routeId, e => e.SerialNumber, true, pageSize, pageIndex, UserInfo.DataBaseName);
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

        /// <summary>
        /// 添加 或 插入 断链数据方法
        /// </summary>
        /// <param name="input">添加 或 插入 的数据</param>
        /// <param name="serialNumber">插入的序号，添加则为0</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(GradeChangePointDto input, int serialNumber, string routeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (input.GradeChangePointId == null)
                    {
                        var allList = await GradeBus.GetListAsync(e => e.RouteId == routeId, UserInfo.DataBaseName);
                        var count = allList.Count();
                        input.GradeChangePointId = Guid.NewGuid().ToString();
                        input.SerialNumber = count + 1;
                        input.RouteId = routeId;
                        if (serialNumber != 0)
                        {
                            var temp = await GradeBus.GetListAsync(e => e.SerialNumber >= serialNumber && e.RouteId == routeId, UserInfo.DataBaseName);
                            var list = temp.ToList();
                            list.ForEach(async i =>
                            {
                                i.SerialNumber++;
                                await GradeBus.UpdateAsync(i, UserInfo.DataBaseName);
                            });
                            input.SerialNumber = serialNumber;
                        }
                        var result = await GradeBus.CreateAsync(input.MapTo<GradeChangePointDto, VerticalCurve_GradeChangePoint>(), UserInfo.DataBaseName);
                        if (result)
                            return SuccessMes();
                        else return Fail();
                    }
                    else
                    {
                        var entity = await GradeBus.GetEntityAsync(e => e.GradeChangePointId == input.GradeChangePointId, UserInfo.DataBaseName);
                        if (entity == null)
                            return null;
                        entity.RouteId = input.RouteId;
                        entity.SerialNumber = input.SerialNumber;
                        entity.Stake = input.Stake;
                        entity.H = input.H;
                        entity.R = input.R;
                        var result = await GradeBus.UpdateAsync(entity, UserInfo.DataBaseName);
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

        /// <summary>
        /// 删除断链数据
        /// </summary>
        /// <param name="list">删除的实体对象列表</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(List<VerticalCurve_GradeChangePoint> list, string routeId)
        {
            try
            {
                if (list.Any())
                {
                    var result = await GradeBus.DeleteAsync(list, UserInfo.DataBaseName);
                    var temp = await GradeBus.GetListAsync(e => e.RouteId == routeId, UserInfo.DataBaseName);
                    var allItem = temp.OrderBy(e => e.SerialNumber).ToList();
                    if (allItem.Any())
                    {
                        for (var i = 0; i < allItem.Count; i++)
                        {
                            allItem[i].SerialNumber = i + 1;
                            await GradeBus.UpdateAsync(allItem[i], UserInfo.DataBaseName);
                        }
                    }
                    return SuccessMes();
                }
                return Fail();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        /// <summary>
        /// 上移和下移
        /// </summary>
        /// <param name="serialNumber">需要移动的序号</param>
        /// <param name="isUp">true为上移，false为下移</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Move(int serialNumber, bool isUp, string routeId)
        {
            try
            {
                var topNumber = 0;
                var bottomNumber = 0;
                if (isUp)
                {
                    if (serialNumber == 1)
                        return Fail("选项不能再上移");
                    topNumber = serialNumber - 1;
                    bottomNumber = serialNumber;
                }
                else
                {
                    var data = await GradeBus.GetListAsync(e => e.RouteId == routeId, UserInfo.DataBaseName);
                    if (serialNumber == data.Count())
                        return Fail("选项不能再下移");
                    topNumber = serialNumber;
                    bottomNumber = serialNumber + 1;
                }

                var top = await GradeBus.GetEntityAsync(e => e.SerialNumber == topNumber && e.RouteId == routeId, UserInfo.DataBaseName);
                var bottom = await GradeBus.GetEntityAsync(e => e.SerialNumber == bottomNumber && e.RouteId == routeId, UserInfo.DataBaseName);
                var temp = top.SerialNumber;
                top.SerialNumber = bottom.SerialNumber;
                bottom.SerialNumber = temp;
                var update = new List<VerticalCurve_GradeChangePoint>()
            {
                top, bottom
            };
                var result = await GradeBus.UpdateAsync(update, UserInfo.DataBaseName);
                if (result)
                    return SuccessMes();
                return Fail();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        /// <summary>
        /// 从文件导入数据
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Import(string routeId)
        {
            try
            {
                var file = Request.Form.Files;
                var success = 0;
                var error = 0;
                if (file != null)
                {
                    var path = FileUtils.SaveFile(Hosting.WebRootPath, file[0], UserInfo.UserId);
                    StreamReader reader = new StreamReader(path, Encoding.Default);
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var tempList = line.Split(",");
                        var list = await GradeBus.GetListAsync(e => e.RouteId == routeId, UserInfo.DataBaseName);
                        var temp = new GradeChangePointDto()
                        {
                            GradeChangePointId = Guid.NewGuid().ToString(),
                            RouteId = routeId,
                            SerialNumber = list.Count() + 1,
                        };
                        if (!string.IsNullOrEmpty(tempList[0]))
                            temp.Stake = Convert.ToDouble(tempList[0]);
                        if (!string.IsNullOrEmpty(tempList[1]))
                            temp.H = Convert.ToDouble(tempList[1]);
                        if (!string.IsNullOrEmpty(tempList[2]))
                            temp.R = Convert.ToDouble(tempList[2]);
                        var valid = TryValidateModel(temp);
                        if (valid)
                        {
                            var result = await GradeBus.CreateAsync(temp.MapTo<GradeChangePointDto, VerticalCurve_GradeChangePoint>(), UserInfo.DataBaseName);
                            if (result)
                                success++;
                            else error++;
                        }
                        else
                        {
                            error++;
                        }
                    }
                    reader.Close();
                    FileUtils.DeleteFile(path);
                    return SuccessMes($"竖曲线表交点法导入数据成功{success}条，失败{error}条");
                }
                return Fail();
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        /// <summary>
        /// 导出数据到文件
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Export(string routeId)
        {
            try
            {
                var content = "";
                var data = await GradeBus.GetListAsync(e => e.RouteId == routeId, UserInfo.DataBaseName);
                var tableData = data.OrderBy(e => e.SerialNumber).ToList();
                tableData.ForEach(i =>
                {
                    content += $"{i.Stake},{i.H},{i.R},\n";
                });
                content = content.Substring(0, content.Length - 2);
                return SuccessMes(content);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}