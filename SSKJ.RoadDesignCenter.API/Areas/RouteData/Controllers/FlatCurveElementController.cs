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
    [Route("api/FlatCurveElement/[action]")]
    [Area("RouteData")]
    public class FlatCurveElementController : BaseController
    {
        public IFlatCurve_CurveElementBusines FlatCurveBus;

        public HostingEnvironment Hosting;

        public FlatCurveElementController(IFlatCurve_CurveElementBusines flatCurveBus, HostingEnvironment hosting)
        {
            FlatCurveBus = flatCurveBus;
            Hosting = hosting;
        }

        public async Task<IActionResult> Get(int pageSize, int pageIndex, string routeId)
        {
            try
            {
                var result = await FlatCurveBus.GetListAsync(e => e.RouteId == routeId, e => e.SerialNumber, true, pageSize, pageIndex, UserInfo.DataBaseName);
                return SuccessData(new
                {
                    data = result.Item1.MapToList<FlatCurve_CurveElement, FlatCurveCurveElementDto>(),
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
        public async Task<IActionResult> Insert(FlatCurveCurveElementDto input, int serialNumber, string routeId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (input.CurveElementId == null)
                    {
                        var allList = await FlatCurveBus.GetListAsync(e => e.RouteId == routeId, UserInfo.DataBaseName);
                        var count = allList.Count();
                        input.CurveElementId = Guid.NewGuid().ToString();
                        input.SerialNumber = count + 1;
                        input.RouteId = routeId;
                        if (serialNumber != 0)
                        {
                            var temp = await FlatCurveBus.GetListAsync(e => e.SerialNumber >= serialNumber && e.RouteId == routeId, UserInfo.DataBaseName);
                            var list = temp.ToList();
                            list.ForEach(async i =>
                            {
                                i.SerialNumber++;
                                await FlatCurveBus.UpdateAsync(i, UserInfo.DataBaseName);
                            });
                            input.SerialNumber = serialNumber;
                        }

                        var result = await FlatCurveBus.CreateAsync(input.MapTo<FlatCurveCurveElementDto, FlatCurve_CurveElement>(), UserInfo.DataBaseName);
                        if (result)
                            return SuccessMes();
                        return Fail();
                    }
                    else
                    {
                        var entity = await FlatCurveBus.GetEntityAsync(e => e.CurveElementId == input.CurveElementId, UserInfo.DataBaseName);
                        if (entity == null)
                            return null;
                        entity.Stake = input.Stake;
                        entity.X = input.X;
                        entity.Y = input.Y;
                        entity.Azimuth = input.Azimuth;
                        entity.TurnTo = input.TurnTo;
                        entity.R = input.R;
                        entity.Description = input.Description;
                        var result = await FlatCurveBus.UpdateAsync(entity, UserInfo.DataBaseName);
                        if (result)
                            return SuccessMes();
                        return Fail();
                    }
                }
                else
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
        public async Task<IActionResult> Delete(List<FlatCurve_CurveElement> list, string routeId)
        {
            try
            {
                if (list.Any())
                {
                    var result = await FlatCurveBus.DeleteAsync(list, UserInfo.DataBaseName);
                    var temp = await FlatCurveBus.GetListAsync(e => e.RouteId == routeId, UserInfo.DataBaseName);
                    var allItem = temp.OrderBy(e => e.SerialNumber).ToList();
                    if (allItem.Any())
                    {
                        for (var i = 0; i < allItem.Count; i++)
                        {
                            allItem[i].SerialNumber = i + 1;
                            await FlatCurveBus.UpdateAsync(allItem[i], UserInfo.DataBaseName);
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
                    var data = await FlatCurveBus.GetListAsync(e => e.RouteId == routeId, UserInfo.DataBaseName);
                    if (serialNumber == data.Count())
                        return Fail("选项不能再下移");
                    topNumber = serialNumber;
                    bottomNumber = serialNumber + 1;
                }

                var top = await FlatCurveBus.GetEntityAsync(e => e.SerialNumber == topNumber && e.RouteId == routeId, UserInfo.DataBaseName);
                var bottom = await FlatCurveBus.GetEntityAsync(e => e.SerialNumber == bottomNumber && e.RouteId == routeId, UserInfo.DataBaseName);
                var temp = top.SerialNumber;
                top.SerialNumber = bottom.SerialNumber;
                bottom.SerialNumber = temp;
                var update = new List<FlatCurve_CurveElement>()
            {
                top, bottom
            };
                var result = await FlatCurveBus.UpdateAsync(update, UserInfo.DataBaseName);
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
                        var list = await FlatCurveBus.GetListAsync(e => e.RouteId == routeId, UserInfo.DataBaseName);
                        var temp = new FlatCurveCurveElementDto()
                        {
                            CurveElementId = Guid.NewGuid().ToString(),
                            RouteId = routeId,
                            SerialNumber = list.Count() + 1,
                        };
                        if (!string.IsNullOrEmpty(tempList[0]))
                            temp.Stake = Convert.ToDouble(tempList[0]);
                        if (!string.IsNullOrEmpty(tempList[1]))
                            temp.X = Convert.ToDouble(tempList[1]);
                        if (!string.IsNullOrEmpty(tempList[2]))
                            temp.Y = Convert.ToDouble(tempList[2]);
                        if (!string.IsNullOrEmpty(tempList[3]))
                            temp.Azimuth = Convert.ToDouble(tempList[3]);
                        if (!string.IsNullOrEmpty(tempList[4]))
                            temp.TurnTo = Convert.ToInt32(tempList[4]);
                        if (!string.IsNullOrEmpty(tempList[5]))
                            temp.R = Convert.ToDouble(tempList[5]);
                        //if (!string.IsNullOrEmpty(tempList[6]))
                        //    temp.Description = tempList[6];

                        var validate = TryValidateModel(temp);
                        if (validate)
                        {
                            var result = await FlatCurveBus.CreateAsync(temp.MapTo<FlatCurveCurveElementDto, FlatCurve_CurveElement>(), UserInfo.DataBaseName);
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
                    return SuccessMes($"平曲线表曲线要素法导入数据成功{success}条，失败{error}条");
                }
                else
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
                var data = await FlatCurveBus.GetListAsync(e => e.RouteId == routeId, UserInfo.DataBaseName);
                var tableData = data.OrderBy(e => e.SerialNumber).ToList();
                tableData.ForEach(i =>
                {
                    content += $"{i.Stake},{i.X},{i.Y},{i.Azimuth},{i.TurnTo},{i.R},\n";
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