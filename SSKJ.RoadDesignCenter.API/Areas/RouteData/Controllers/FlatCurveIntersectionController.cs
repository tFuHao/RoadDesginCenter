﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.API.Controllers;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Utility.Tools;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteData.Controllers
{
    [Route("api/FlatCurveIntersection/[action]")]
    [Area("RouteData")]
    public class FlatCurveInterSectionController : BaseController
    {
        public IFlatCurve_IntersectionBusines FlatCurveBus;

        public HostingEnvironment Hosting;

        public FlatCurveInterSectionController(IFlatCurve_IntersectionBusines flatCurveBus, HostingEnvironment hosting)
        {
            FlatCurveBus = flatCurveBus;
            Hosting = hosting;
        }

        public async Task<IActionResult> Get(int pageSize, int pageIndex)
        {
            try
            {
                var result = await FlatCurveBus.GetListAsync(e => true, e => e.SerialNumber, true, pageSize, pageIndex, UserInfo.DataBaseName);
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

        /// <summary>
        /// 添加 或 插入 断链数据方法
        /// </summary>
        /// <param name="input">添加 或 插入 的数据</param>
        /// <param name="serialNumber">插入的序号，添加则为0</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(FlatCurve_Intersection input, int serialNumber)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (input.IntersectionPointId == null)
                    {
                        var allList = await FlatCurveBus.GetListAsync(UserInfo.DataBaseName);
                        var count = allList.Count();
                        input.IntersectionPointId = Guid.NewGuid().ToString();
                        input.SerialNumber = count + 1;
                        if (serialNumber != 0)
                        {
                            var temp = await FlatCurveBus.GetListAsync(e => e.SerialNumber >= serialNumber, UserInfo.DataBaseName);
                            var list = temp.ToList();
                            list.ForEach(async i =>
                            {
                                i.SerialNumber++;
                                await FlatCurveBus.UpdateAsync(i, UserInfo.DataBaseName);
                            });
                            input.SerialNumber = serialNumber;
                        }

                        var result = await FlatCurveBus.CreateAsync(input, UserInfo.DataBaseName);
                        if (result)
                            return Success();
                        return Fail();
                    }
                    else
                    {
                        var entity = await FlatCurveBus.GetEntityAsync(e => e.IntersectionPointId == input.IntersectionPointId, UserInfo.DataBaseName);
                        if (entity == null)
                            return Fail();
                        entity.IntersectionName = input.IntersectionName;
                        entity.Stake = input.Stake;
                        entity.X = input.X;
                        entity.Y = input.Y;
                        entity.R = input.R;
                        entity.Ls1 = input.Ls1;
                        entity.Ls2 = input.Ls2;
                        entity.Ls1R = input.Ls1R;
                        entity.Ls2R = input.Ls2R;
                        var result = await FlatCurveBus.UpdateAsync(entity, UserInfo.DataBaseName);
                        if (result)
                            return Success();
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
        public async Task<IActionResult> Delete(List<FlatCurve_Intersection> list)
        {
            try
            {
                if (list.Any())
                {
                    var result = await FlatCurveBus.DeleteAsync(list, UserInfo.DataBaseName);
                    var temp = await FlatCurveBus.GetListAsync(UserInfo.DataBaseName);
                    var allItem = temp.OrderBy(e => e.SerialNumber).ToList();
                    if (allItem.Any())
                    {
                        for (var i = 0; i < allItem.Count; i++)
                        {
                            allItem[i].SerialNumber = i + 1;
                            await FlatCurveBus.UpdateAsync(allItem[i], UserInfo.DataBaseName);
                        }
                        return Success();
                    }
                    else
                        return Fail();
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
        /// 上移和下移
        /// </summary>
        /// <param name="serialNumber">需要移动的序号</param>
        /// <param name="isUp">true为上移，false为下移</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Move(int serialNumber, bool isUp)
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
                    var data = await FlatCurveBus.GetListAsync(UserInfo.DataBaseName);
                    if (serialNumber == data.Count())
                        return Fail("选项不能再下移");
                    topNumber = serialNumber;
                    bottomNumber = serialNumber + 1;
                }

                var top = await FlatCurveBus.GetEntityAsync(e => e.SerialNumber == topNumber, UserInfo.DataBaseName);
                var bottom = await FlatCurveBus.GetEntityAsync(e => e.SerialNumber == bottomNumber, UserInfo.DataBaseName);
                var temp = top.SerialNumber;
                top.SerialNumber = bottom.SerialNumber;
                bottom.SerialNumber = temp;
                var update = new List<FlatCurve_Intersection>()
            {
                top, bottom
            };
                var result = await FlatCurveBus.UpdateAsync(update, UserInfo.DataBaseName);
                if (result)
                    return Success();
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
        public async Task<IActionResult> Import()
        {
            try
            {
                var file = Request.Form.Files;
                var success = 0;
                var error = 0;
                if (file != null)
                {
                    var path = FileUtils.SaveFile(Hosting.WebRootPath, file[0]);
                    StreamReader reader = new StreamReader(path, Encoding.Default);
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var tempList = line.Split(",");
                        var list = await FlatCurveBus.GetListAsync(UserInfo.DataBaseName);
                        var temp = new FlatCurve_Intersection()
                        {
                            IntersectionPointId = Guid.NewGuid().ToString(),
                            SerialNumber = list.Count() + 1,
                            IntersectionName = tempList[0],
                            Stake = Convert.ToDouble(tempList[1]),
                            X = Convert.ToDouble(tempList[2]),
                            Y = Convert.ToDouble(tempList[3]),
                            R = Convert.ToInt32(tempList[4]),
                            Ls1 = Convert.ToDouble(tempList[5]),
                            Ls2 = Convert.ToDouble(tempList[6]),
                            Ls1R = Convert.ToDouble(tempList[7]),
                            Ls2R = Convert.ToDouble(tempList[8])
                        };
                        var validate = TryValidateModel(temp);
                        if (validate)
                        {
                            var result = await FlatCurveBus.CreateAsync(temp, UserInfo.DataBaseName);
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
                    return Success($"平曲线表交点法导入数据成功{success}条，失败{error}条");
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
        public async Task<IActionResult> Export()
        {
            try
            {
                var content = "";
                var data = await FlatCurveBus.GetListAsync(UserInfo.DataBaseName);
                var tableData = data.OrderBy(e => e.SerialNumber).ToList();
                tableData.ForEach(i =>
                {
                    content += $"{i.IntersectionName},{i.Stake},{i.X},{i.Y},{i.R},{i.Ls1},{i.Ls2},{i.Ls1R},{i.Ls2R},\n";
                });
                content = content.Substring(0, content.Length - 2);
                return Success(content);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }
    }
}