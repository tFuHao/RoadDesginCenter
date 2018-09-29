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

        public async Task<bool> CreateAsync(VerticalSectionGroundLine entity, string dataBaseName = null)
        {
            return await SectionRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<VerticalSectionGroundLine> entityList, string dataBaseName = null)
        {
            return await SectionRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await SectionRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await SectionRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(VerticalSectionGroundLine entity, string dataBaseName = null)
        {
            return await SectionRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<VerticalSectionGroundLine> entityList, string dataBaseName = null)
        {
            return await SectionRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<VerticalSectionGroundLine> GetEntityAsync(Expression<Func<VerticalSectionGroundLine, bool>> where, string dataBaseName = null)
        {
            return await SectionRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<VerticalSectionGroundLine> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await SectionRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<VerticalSectionGroundLine>> GetListAsync(Expression<Func<VerticalSectionGroundLine, bool>> where, string dataBaseName = null)
        {
            return await SectionRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<VerticalSectionGroundLine>, int>> GetListAsync<Tkey>(Expression<Func<VerticalSectionGroundLine, bool>> where, Func<VerticalSectionGroundLine, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await SectionRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<VerticalSectionGroundLine>> GetListAsync(string dataBaseName = null)
        {
            return await SectionRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(VerticalSectionGroundLine entity, string dataBaseName = null)
        {
            return await SectionRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<VerticalSectionGroundLine> entityList, string dataBaseName = null)
        {
            return await SectionRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}