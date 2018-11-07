using System;
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
    [Route("api/CrossSectionGroundLine/[action]")]
    [Area("RouteData")]
    public class CrossSectionGroundLineController : BaseController
    {
        public ICrossSectionGroundLineBusines SectionBus;

        public HostingEnvironment Hosting;

        public CrossSectionGroundLineController(ICrossSectionGroundLineBusines sectionBus, HostingEnvironment hosting)
        {
            SectionBus = sectionBus;
            Hosting = hosting;
        }

        public async Task<IActionResult> Get(int pageSize, int pageIndex, string routeId)
        {
            try
            {
                var result = await SectionBus.GetListAsync(e => e.RouteId == routeId, e => e.CrossSectionGroundLineId, true, pageSize, pageIndex, UserInfo.DataBaseName);
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
        public async Task<IActionResult> Insert(CrossSectionGroundLine input, string routeId)
        {
            try
            {
                if (input.CrossSectionGroundLineId == null)
                {
                    input.CrossSectionGroundLineId = Guid.NewGuid().ToString();
                    input.RouteId = routeId;
                    var result = await SectionBus.CreateAsync(input, UserInfo.DataBaseName);
                    if (result)
                        return SuccessMes();
                    return Fail();
                }
                else
                {
                    var entity = await SectionBus.GetEntityAsync(e => e.CrossSectionGroundLineId == input.CrossSectionGroundLineId, UserInfo.DataBaseName);
                    if (entity == null)
                        return Fail();
                    entity.Stake = input.Stake;
                    var result = await SectionBus.UpdateAsync(entity, UserInfo.DataBaseName);
                    if (result)
                        return SuccessMes();
                    return Fail();
                }
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
        public async Task<IActionResult> Delete(List<CrossSectionGroundLine> list)
        {
            try
            {
                var result = await SectionBus.DeleteAsync(list, UserInfo.DataBaseName);
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
                        var temp = new CrossSectionGroundLine()
                        {
                            CrossSectionGroundLineId = Guid.NewGuid().ToString(),
                            RouteId = routeId,
                            Stake = Convert.ToDouble(tempList[0]),
                        };
                        var validate = TryValidateModel(temp);
                        if (validate)
                        {
                            var result = await SectionBus.CreateAsync(temp, UserInfo.DataBaseName);
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
                    return SuccessMes($"横断面地面线数据导入数据成功{success}条，失败{error}条");
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
                var data = await SectionBus.GetListAsync(e => e.RouteId == routeId, UserInfo.DataBaseName);
                data.ToList().ForEach(i =>
                {
                    content += $"{i.Stake},\n";
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