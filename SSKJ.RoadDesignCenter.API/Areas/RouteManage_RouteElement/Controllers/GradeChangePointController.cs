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
    [Route("api/GradeChangePoint/[action]")]
    [Area("RouteManage_RouteElement")]
    public class GradeChangePointController : BaseController
    {
        public IVerticalCurve_GradeChangePointBusines GradeBus;

        public HostingEnvironment Hosting;

        public GradeChangePointController(IVerticalCurve_GradeChangePointBusines gradeBus, HostingEnvironment hosting)
        {
            GradeBus = gradeBus;
            Hosting = hosting;
        }

        public async Task<IActionResult> Get(int pageSize, int pageIndex)
        {
            var result = await GradeBus.GetListAsync(e => true, e => e.SerialNumber, true, pageSize, pageIndex, GetConStr());
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
        public async Task<IActionResult> Insert(VerticalCurve_GradeChangePoint input, int serialNumber)
        {
            if (ModelState.IsValid)
            {
                if (input.GradeChangePointId == null)
                {
                    var allList = await GradeBus.GetListAsync(GetConStr());
                    var count = allList.Count();
                    input.GradeChangePointId = Guid.NewGuid().ToString();
                    input.SerialNumber = count + 1;
                    if (serialNumber != 0)
                    {
                        var temp = await GradeBus.GetListAsync(e => e.SerialNumber >= serialNumber, GetConStr());
                        var list = temp.ToList();
                        list.ForEach(async i =>
                        {
                            i.SerialNumber++;
                            await GradeBus.UpdateAsync(i, GetConStr());
                        });
                        input.SerialNumber = serialNumber;
                    }
                    var result = await GradeBus.CreateAsync(input, GetConStr());
                    return Json(result);
                }
                else
                {
                    var entity = await GradeBus.GetEntityAsync(e => e.GradeChangePointId == input.GradeChangePointId, GetConStr());
                    if (entity == null)
                        return null;
                    entity.VerticalCurveId = input.VerticalCurveId;
                    entity.SerialNumber = input.SerialNumber;
                    entity.Stake = input.Stake;
                    entity.H = input.H;
                    entity.R = input.R;
                    var result = await GradeBus.UpdateAsync(entity, GetConStr());
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
        public async Task<IActionResult> Delete(List<VerticalCurve_GradeChangePoint> list)
        {
            if (list.Any())
            {
                var result = await GradeBus.DeleteAsync(list, GetConStr());
                var temp = await GradeBus.GetListAsync(GetConStr());
                var allItem = temp.OrderBy(e => e.SerialNumber).ToList();
                if (allItem.Any())
                {
                    for (var i = 0; i < allItem.Count; i++)
                    {
                        allItem[i].SerialNumber = i + 1;
                        await GradeBus.UpdateAsync(allItem[i], GetConStr());
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
                var data = await GradeBus.GetListAsync(GetConStr());
                if (serialNumber == data.Count())
                    return Json(new { code = 0, errorMsg = "选项不能再下移" });
                topNumber = serialNumber;
                bottomNumber = serialNumber + 1;
            }

            var top = await GradeBus.GetEntityAsync(e => e.SerialNumber == topNumber, GetConStr());
            var bottom = await GradeBus.GetEntityAsync(e => e.SerialNumber == bottomNumber, GetConStr());
            var temp = top.SerialNumber;
            top.SerialNumber = bottom.SerialNumber;
            bottom.SerialNumber = temp;
            var update = new List<VerticalCurve_GradeChangePoint>()
            {
                top, bottom
            };
            var result = await GradeBus.UpdateAsync(update, GetConStr());

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
                    var list = await GradeBus.GetListAsync(GetConStr());
                    var temp = new VerticalCurve_GradeChangePoint()
                    {
                        GradeChangePointId = Guid.NewGuid().ToString(),
                        SerialNumber = list.Count() + 1,
                        Stake = Convert.ToDouble(tempList[0]),
                        H = Convert.ToDouble(tempList[1]),
                        R = Convert.ToDouble(tempList[2])
                    };
                    var result = await GradeBus.CreateAsync(temp, GetConStr());
                    if (result)
                        success++;
                    else error++;
                }
                reader.Close();
                FileUtils.DeleteFile(path);
                return Content($"竖曲线表交点法导入数据成功{success}条，失败{error}条");
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
            var data = await GradeBus.GetListAsync(GetConStr());
            var tableData = data.OrderBy(e => e.SerialNumber).ToList();
            tableData.ForEach(i =>
            {
                content += $"{i.Stake},{i.H},{i.R},\n";
            });
            content = content.Substring(0, content.Length - 2);
            return Content(content);
        }
    }
}