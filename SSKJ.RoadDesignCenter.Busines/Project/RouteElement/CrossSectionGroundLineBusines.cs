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

        public async Task<bool> CreateAsync(CrossSectionGroundLine entity, string connectionString = null)
        {
            return await CrossRepo.CreateAsync(entity, connectionString);
        }

        public async Task<bool> CreateAsync(IEnumerable<CrossSectionGroundLine> entityList, string connectionString = null)
        {
            return await CrossRepo.CreateAsync(entityList, connectionString);
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            return await CrossRepo.DeleteAsync(keyValue, connectionString);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            return await CrossRepo.DeleteAsync(keyValues, connectionString);
        }

        public async Task<bool> DeleteAsync(CrossSectionGroundLine entity, string connectionString = null)
        {
            return await CrossRepo.DeleteAsync(entity, connectionString);
        }

        public async Task<bool> DeleteAsync(IEnumerable<CrossSectionGroundLine> entityList, string connectionString = null)
        {
            return await CrossRepo.DeleteAsync(entityList, connectionString);
        }

        public async Task<CrossSectionGroundLine> GetEntityAsync(Expression<Func<CrossSectionGroundLine, bool>> where, string connectionString = null)
        {
            return await CrossRepo.GetEntityAsync(where, connectionString);
        }

        public async Task<CrossSectionGroundLine> GetEntityAsync(string keyValue, string connectionString = null)
        {
            return await CrossRepo.GetEntityAsync(keyValue, connectionString);
        }

        public async Task<IEnumerable<CrossSectionGroundLine>> GetListAsync(Expression<Func<CrossSectionGroundLine, bool>> where, string connectionString = null)
        {
            return await CrossRepo.GetListAsync(where, connectionString);
        }

        public async Task<Tuple<IEnumerable<CrossSectionGroundLine>, int>> GetListAsync<Tkey>(Expression<Func<CrossSectionGroundLine, bool>> where, Func<CrossSectionGroundLine, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            return await CrossRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, connectionString);
        }

        public async Task<IEnumerable<CrossSectionGroundLine>> GetListAsync(string connectionString = null)
        {
            return await CrossRepo.GetListAsync(connectionString);
        }

        public async Task<bool> UpdateAsync(CrossSectionGroundLine entity, string connectionString = null)
        {
            return await CrossRepo.UpdateAsync(entity, connectionString);
        }

        public async Task<bool> UpdateAsync(IEnumerable<CrossSectionGroundLine> entityList, string connectionString = null)
        {
            return await CrossRepo.UpdateAsync(entityList, connectionString);
        }
    }
}