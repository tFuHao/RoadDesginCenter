using SSKJ.RoadDesignCenter.IBusines.Project.Authorize;
using SSKJ.RoadDesignCenter.IRepository.Project.Authorize;
using SSKJ.RoadDesignCenter.Models.ProjectModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Busines.Project.Authorize
{
   public class UserRelationBusines: IUserRelationBusines
    {
        private readonly IUserRelationRepository roleUserDal;

        public UserRelationBusines(IUserRelationRepository roleUserDal)
        {
            this.roleUserDal = roleUserDal;
        }

        public async Task<bool> CreateAsync(UserRelation entity, string dataBaseName = null)
        {
            return await roleUserDal.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<UserRelation> entityList, string dataBaseName = null)
        {
            return await roleUserDal.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await roleUserDal.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await roleUserDal.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(UserRelation entity, string dataBaseName = null)
        {
            return await roleUserDal.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<UserRelation> entityList, string dataBaseName = null)
        {
            return await roleUserDal.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<UserRelation> GetEntityAsync(Expression<Func<UserRelation, bool>> where, string dataBaseName = null)
        {
            return await roleUserDal.GetEntityAsync(where, dataBaseName);
        }

        public async Task<UserRelation> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await roleUserDal.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<UserRelation>> GetListAsync(Expression<Func<UserRelation, bool>> where, string dataBaseName = null)
        {
            return await roleUserDal.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<UserRelation>, int>> GetListAsync<Tkey>(Expression<Func<UserRelation, bool>> where, Func<UserRelation, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await roleUserDal.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<UserRelation>> GetListAsync(string dataBaseName = null)
        {
            return await roleUserDal.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(UserRelation entity, string dataBaseName = null)
        {
            return await roleUserDal.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<UserRelation> entityList, string dataBaseName = null)
        {
            return await roleUserDal.UpdateAsync(entityList, dataBaseName);
        }
    }
}
