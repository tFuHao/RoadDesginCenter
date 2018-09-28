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
    [Route("api/VerticalSectionGroundLine/[action]")]
    [Area("RouteManage_RouteElement")]
    public class VerticalSectionGroundLineController : Controller
    {
        public IVerticalSectionGroundLineBusines SectionBus;

        public HostingEnvironment Hosting;

        public string ConStr = "server=139.224.200.194;port=3306;database=road_project_001;user id=root;password=SSKJ*147258369";

        public VerticalSectionGroundLineController(IVerticalSectionGroundLineBusines sectionBus, HostingEnvironment hosting)
        {
            SectionBus = sectionBus;
            Hosting = hosting;
        }

        public async Task<IActionResult> Get(int pageSize, int pageIndex)
        {
            var result = await SectionBus.GetListAsync(e => true, e => e.SerialNumber, true, pageSize, pageIndex, ConStr);
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
        public async Task<IActionResult> Insert(VerticalSectionGroundLine input, int serialNumber)
        {
            if (ModelState.IsValid)
            {
                if (input.Id == null)
                {
                    var allList = await SectionBus.GetListAsync(ConStr);
                    var count = allList.Count();
                    input.Id = Guid.NewGuid().ToString();
                    input.SerialNumber = count + 1;
                    if (serialNumber != 0)
                    {
                        var temp = await SectionBus.GetListAsync(e => e.SerialNumber >= serialNumber, ConStr);
                        var list = temp.ToList();
                        list.ForEach(async i =>
                        {
                            i.SerialNumber++;
                            await SectionBus.UpdateAsync(i, ConStr);
                        });
                        input.SerialNumber = serialNumber;
                    }
                    var result = await SectionBus.CreateAsync(input, ConStr);
                    return Json(result);
                }
                else
                {
                    var entity = await SectionBus.GetEntityAsync(e => e.Id == input.Id, ConStr);
                    if (entity == null)
                        return null;
                    entity.RouteId = input.RouteId;
                    entity.SerialNumber = input.SerialNumber;
                    entity.Stake = input.Stake;
                    entity.H = input.H;
                    var result = await SectionBus.UpdateAsync(entity, ConStr);
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
        public async Task<IActionResult> Delete(List<VerticalSectionGroundLine> list)
        {
            if (list.Any())
            {
                var result = await SectionBus.DeleteAsync(list, ConStr);
                var temp = await SectionBus.GetListAsync(ConStr);
                var allItem = temp.OrderBy(e => e.SerialNumber).ToList();
                if (allItem.Any())
                {
                    for (var i = 0; i < allItem.Count; i++)
                    {
                        allItem[i].SerialNumber = i + 1;
                        await SectionBus.UpdateAsync(allItem[i], ConStr);
                    }

                    return Json(true);
                }
                else
                {
                    return Json(result);
                }
            }
            return Json(false);
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
            var topNumber = 0;
            var bottomNumber = 0;
            if (isUp)
            {
                if (serialNumber == 1)
                    return Json(new { code = 0, errorMsg = "选项不能再上移" });
                topNumber = serialNumber - 1;
                bottomNumber = serialNumber;
            }
            else
            {
                var data = await SectionBus.GetListAsync(ConStr);
                if (serialNumber == data.Count())
                    return Json(new { code = 0, errorMsg = "选项不能再下移" });
                topNumber = serialNumber;
                bottomNumber = serialNumber + 1;
            }

            var top = await SectionBus.GetEntityAsync(e => e.SerialNumber == topNumber, ConStr);
            var bottom = await SectionBus.GetEntityAsync(e => e.SerialNumber == bottomNumber, ConStr);
            var temp = top.SerialNumber;
            top.SerialNumber = bottom.SerialNumber;
            bottom.SerialNumber = temp;
            var update = new List<VerticalSectionGroundLine>()
            {
                top, bottom
            };
            var result = await SectionBus.UpdateAsync(update, ConStr);

            return Json(new { code = result });
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
                    var list = await SectionBus.GetListAsync(ConStr);
                    var temp = new VerticalSectionGroundLine()
                    {
                        Id = Guid.NewGuid().ToString(),
                        SerialNumber = list.Count() + 1,
                        Stake = Convert.ToDouble(tempList[0]),
                        H = Convert.ToDouble(tempList[1]),
                    };
                    var result = await SectionBus.CreateAsync(temp, ConStr);
                    if (result)
                        success++;
                    else error++;
                }
                reader.Close();
                FileUtils.DeleteFile(path);
                return Content($"纵断面地面线数据导入数据成功{success}条，失败{error}条");
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
            var data = await SectionBus.GetListAsync(ConStr);
            var tableData = data.OrderBy(e => e.SerialNumber).ToList();
            tableData.ForEach(i =>
            {
                content += $"{i.Stake},{i.H},\n";
            });
            content = content.Substring(0, content.Length - 2);
            return Content(content);
        }
    }
}