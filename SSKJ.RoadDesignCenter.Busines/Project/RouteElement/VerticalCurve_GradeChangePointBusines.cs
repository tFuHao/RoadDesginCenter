using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class VerticalCurve_GradeChangePointBusines : IVerticalCurve_GradeChangePointBusines
    {
        public IVerticalCurve_GradeChangePointRepository VerticalCurveRepo;

        public VerticalCurve_GradeChangePointBusines(IVerticalCurve_GradeChangePointRepository verticalCurveRepo)
        {
            VerticalCurveRepo = verticalCurveRepo;
        }

        public async Task<bool> CreateAsync(VerticalCurve_GradeChangePoint entity, string dataBaseName = null)
        {
            return await VerticalCurveRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<VerticalCurve_GradeChangePoint> entityList, string dataBaseName = null)
        {
            return await VerticalCurveRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await VerticalCurveRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await VerticalCurveRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(VerticalCurve_GradeChangePoint entity, string dataBaseName = null)
        {
            return await VerticalCurveRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<VerticalCurve_GradeChangePoint> entityList, string dataBaseName = null)
        {
            return await VerticalCurveRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<VerticalCurve_GradeChangePoint> GetEntityAsync(Expression<Func<VerticalCurve_GradeChangePoint, bool>> where, string dataBaseName = null)
        {
            return await VerticalCurveRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<VerticalCurve_GradeChangePoint> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await VerticalCurveRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<VerticalCurve_GradeChangePoint>> GetListAsync(Expression<Func<VerticalCurve_GradeChangePoint, bool>> where, string dataBaseName = null)
        {
            return await VerticalCurveRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<VerticalCurve_GradeChangePoint>, int>> GetListAsync<Tkey>(Expression<Func<VerticalCurve_GradeChangePoint, bool>> where, Func<VerticalCurve_GradeChangePoint, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await VerticalCurveRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<VerticalCurve_GradeChangePoint>> GetListAsync(string dataBaseName = null)
        {
            return await VerticalCurveRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(VerticalCurve_GradeChangePoint entity, string dataBaseName = null)
        {
            return await VerticalCurveRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<VerticalCurve_GradeChangePoint> entityList, string dataBaseName = null)
        {
            return await VerticalCurveRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}