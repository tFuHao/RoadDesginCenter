using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SSKJ.RoadDesignCenter.IBusines.Project;
using SSKJ.RoadDesignCenter.IRepository.Project;
using SSKJ.RoadDesignCenter.Models.ProjectModel;

namespace SSKJ.RoadDesignCenter.Busines.Project
{
    public class RoleBusines : IRoleBusines
    {
        private readonly IRoleRepository roleRepo;

        public RoleBusines(IRoleRepository roleRepo)
        {
            this.roleRepo = roleRepo;
        }

        public async Task<bool> CreateAsync(Role entity, string dataBaseName = null)
        {
            return await roleRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<Role> entityList, string dataBaseName = null)
        {
            return await roleRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await roleRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await roleRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(Role entity, string dataBaseName = null)
        {
            return await roleRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<Role> entityList, string dataBaseName = null)
        {
            return await roleRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<Role> GetEntityAsync(Expression<Func<Role, bool>> where, string dataBaseName = null)
        {
            return await roleRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<Role> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await roleRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<Role>> GetListAsync(Expression<Func<Role, bool>> where, string dataBaseName = null)
        {
            return await roleRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<Role>, int>> GetListAsync<Tkey>(Expression<Func<Role, bool>> where, Func<Role, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await roleRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<Role>> GetListAsync(string dataBaseName = null)
        {
            return await roleRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(Role entity, string dataBaseName = null)
        {
            return await roleRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<Role> entityList, string dataBaseName = null)
        {
            return await roleRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}