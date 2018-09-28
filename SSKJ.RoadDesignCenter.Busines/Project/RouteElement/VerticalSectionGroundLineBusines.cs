using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteElement
{
    public class VerticalSectionGroundLineBusines : IVerticalSectionGroundLineBusines
    {

        public IVerticalSectionGroundLineRepository SectionRepo;

        public VerticalSectionGroundLineBusines(IVerticalSectionGroundLineRepository sectionRepo)
        {
            SectionRepo = sectionRepo;
        }

        public async Task<bool> CreateAsync(VerticalSectionGroundLine entity, string connectionString = null)
        {
            return await SectionRepo.CreateAsync(entity, connectionString);
        }

        public async Task<bool> CreateAsync(IEnumerable<VerticalSectionGroundLine> entityList, string connectionString = null)
        {
            return await SectionRepo.CreateAsync(entityList, connectionString);
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            return await SectionRepo.DeleteAsync(keyValue, connectionString);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            return await SectionRepo.DeleteAsync(keyValues, connectionString);
        }

        public async Task<bool> DeleteAsync(VerticalSectionGroundLine entity, string connectionString = null)
        {
            return await SectionRepo.DeleteAsync(entity, connectionString);
        }

        public async Task<bool> DeleteAsync(IEnumerable<VerticalSectionGroundLine> entityList, string connectionString = null)
        {
            return await SectionRepo.DeleteAsync(entityList, connectionString);
        }

        public async Task<VerticalSectionGroundLine> GetEntityAsync(Expression<Func<VerticalSectionGroundLine, bool>> where, string connectionString = null)
        {
            return await SectionRepo.GetEntityAsync(where, connectionString);
        }

        public async Task<VerticalSectionGroundLine> GetEntityAsync(string keyValue, string connectionString = null)
        {
            return await SectionRepo.GetEntityAsync(keyValue, connectionString);
        }

        public async Task<IEnumerable<VerticalSectionGroundLine>> GetListAsync(Expression<Func<VerticalSectionGroundLine, bool>> where, string connectionString = null)
        {
            return await SectionRepo.GetListAsync(where, connectionString);
        }

        public async Task<Tuple<IEnumerable<VerticalSectionGroundLine>, int>> GetListAsync<Tkey>(Expression<Func<VerticalSectionGroundLine, bool>> where, Func<VerticalSectionGroundLine, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            return await SectionRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, connectionString);
        }

        public async Task<IEnumerable<VerticalSectionGroundLine>> GetListAsync(string connectionString = null)
        {
            return await SectionRepo.GetListAsync(connectionString);
        }

        public async Task<bool> UpdateAsync(VerticalSectionGroundLine entity, string connectionString = null)
        {
            return await SectionRepo.UpdateAsync(entity, connectionString);
        }

        public async Task<bool> UpdateAsync(IEnumerable<VerticalSectionGroundLine> entityList, string connectionString = null)
        {
            return await SectionRepo.UpdateAsync(entityList, connectionString);
        }
    }
}