using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Mvc;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using SSKJ.RoadDesignCenter.Utility.Tools;

namespace SSKJ.RoadDesignCenter.API.Areas.RouteManage_RouteElement.Controllers
{
    [Route("api/CrossSectionGroundLine/[action]")]
    [Area("RouteManage_RouteElement")]
    public class CrossSectionGroundLineController : BaseController
    {
        public ICrossSectionGroundLineBusines SectionBus;

        public HostingEnvironment Hosting;

        public CrossSectionGroundLineController(ICrossSectionGroundLineBusines sectionBus, HostingEnvironment hosting)
        {
            SectionBus = sectionBus;
            Hosting = hosting;
        }

        public async Task<IActionResult> Get(int pageSize, int pageIndex)
        {
            var result = await SectionBus.GetListAsync(e => true, e => e.CrossSectionGroundLineId, true, pageSize, pageIndex, GetConStr());
            return Json(new
            {
                data = result.Item1,
                count = result.Item2
            });
        }

        /// <summary>
        /// 添加 或 插入 断链数据方法
        /// </summary>
        /// <param name="input">添加 或 插入 的数据</param>
        /// <param name="serialNumber">插入的序号，添加则为0</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(CrossSectionGroundLine input)
        {
            if (ModelState.IsValid)
            {
                if (input.CrossSectionGroundLineId == null)
                {
                    input.CrossSectionGroundLineId = Guid.NewGuid().ToString();
                    var result = await SectionBus.CreateAsync(input, GetConStr());
                    return Json(result);
                }
                else
                {
                    var entity = await SectionBus.GetEntityAsync(e => e.CrossSectionGroundLineId == input.CrossSectionGroundLineId, GetConStr());
                    if (entity == null)
                        return null;
                    entity.RouteId = input.RouteId;
                    entity.Stake = input.Stake;
                    var result = await SectionBus.UpdateAsync(entity, GetConStr());
                    return Json(result);
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

        /// <summary>
        /// 删除断链数据
        /// </summary>
        /// <param name="list">删除的实体对象列表</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Delete(List<CrossSectionGroundLine> list)
        {
            if (list.Any())
            {
                var result = await SectionBus.DeleteAsync(list, GetConStr());
                return Json(result);
            }
            return Json(false);
        }

        /// <summary>
        /// 从文件导入数据
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Import()
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
                    var temp = new CrossSectionGroundLine()
                    {
                        CrossSectionGroundLineId = Guid.NewGuid().ToString(),
                        Stake = Convert.ToDouble(tempList[0]),
                    };
                    var validate = TryValidateModel(temp);
                    if (validate)
                    {
                        var result = await SectionBus.CreateAsync(temp, GetConStr());
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
                return Content($"横断面地面线数据导入数据成功{success}条，失败{error}条");
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 导出数据到文件
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Export()
        {
            var content = "";
            var data = await SectionBus.GetListAsync(GetConStr());
            data.ToList().ForEach(i =>
            {
                content += $"{i.Stake},\n";
            });
            content = content.Substring(0, content.Length - 2);
            return Content(content);
        }

        public IActionResult Test()
        {
            return Content("success");
        }
    }
}