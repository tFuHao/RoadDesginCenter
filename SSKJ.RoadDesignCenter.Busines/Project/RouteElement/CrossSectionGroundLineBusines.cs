using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class CrossSectionGroundLineBusines : ICrossSectionGroundLineBusines
    {

        public ICrossSectionGroundLineRepository CrossRepo;

        public CrossSectionGroundLineBusines(ICrossSectionGroundLineRepository crossRepo)
        {
            CrossRepo = crossRepo;
        }

        public async Task<bool> CreateAsync(CrossSectionGroundLine entity, string dataBaseName = null)
        {
            return await CrossRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<CrossSectionGroundLine> entityList, string dataBaseName = null)
        {
            return await CrossRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await CrossRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await CrossRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(CrossSectionGroundLine entity, string dataBaseName = null)
        {
            return await CrossRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<CrossSectionGroundLine> entityList, string dataBaseName = null)
        {
            return await CrossRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<CrossSectionGroundLine> GetEntityAsync(Expression<Func<CrossSectionGroundLine, bool>> where, string dataBaseName = null)
        {
            return await CrossRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<CrossSectionGroundLine> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await CrossRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<CrossSectionGroundLine>> GetListAsync(Expression<Func<CrossSectionGroundLine, bool>> where, string dataBaseName = null)
        {
            return await CrossRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<CrossSectionGroundLine>, int>> GetListAsync<Tkey>(Expression<Func<CrossSectionGroundLine, bool>> where, Func<CrossSectionGroundLine, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await CrossRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<CrossSectionGroundLine>> GetListAsync(string dataBaseName = null)
        {
            return await CrossRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(CrossSectionGroundLine entity, string dataBaseName = null)
        {
            return await CrossRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<CrossSectionGroundLine> entityList, string dataBaseName = null)
        {
            return await CrossRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}