using Microsoft.EntityFrameworkCore;
using SSKJ.RoadDesignCenter.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Repository.MySQL.Project
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        public async Task<bool> CreateAsync(T entity, string dataBaseName = null)
        {
            using (ProjectContext context = await DataBaseConfig.CreateContext(dataBaseName))
            {
                await context.Set<T>().AddAsync(entity);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> CreateAsync(IEnumerable<T> entityList, string dataBaseName = null)
        {
            using (ProjectContext context = await DataBaseConfig.CreateContext(dataBaseName))
            {
                await context.Set<T>().AddRangeAsync(entityList);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            using (ProjectContext context = await DataBaseConfig.CreateContext(dataBaseName))
            {
                context.Set<T>().Remove(context.Find<T>(keyValue));
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            using (ProjectContext context = await DataBaseConfig.CreateContext(dataBaseName))
            {
                List<T> entitys = new List<T>();
                entitys.ForEach(async u =>
                {
                    entitys.Add(await context.FindAsync<T>(u));
                });
                return await DeleteAsync(entitys);
            }
        }

        public async Task<bool> DeleteAsync(T entity, string dataBaseName = null)
        {
            using (ProjectContext context = await DataBaseConfig.CreateContext(dataBaseName))
            {
                context.Set<T>().Remove(entity);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(IEnumerable<T> entityList, string dataBaseName = null)
        {
            using (ProjectContext context = await DataBaseConfig.CreateContext(dataBaseName))
            {
                context.Set<T>().RemoveRange(entityList);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<IEnumerable<T>> GetListAsync(string dataBaseName = null)
        {
            using (ProjectContext context = await DataBaseConfig.CreateContext(dataBaseName))
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<T> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            using (ProjectContext context = await DataBaseConfig.CreateContext(dataBaseName))
            {
                return await context.FindAsync<T>(keyValue);
            }
        }

        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> where, string dataBaseName = null)
        {
            using (ProjectContext context = await DataBaseConfig.CreateContext(dataBaseName))
            {
                return await context.Set<T>().FirstOrDefaultAsync(where);
            }
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> where, string dataBaseName = null)
        {
            using (ProjectContext context = await DataBaseConfig.CreateContext(dataBaseName))
            {
                return await context.Set<T>().AsNoTracking().Where(where).ToListAsync();
            }
        }

        public async Task<Tuple<IEnumerable<T>, int>> GetListAsync<Tkey>(Expression<Func<T, bool>> where, Func<T, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            using (ProjectContext context = await DataBaseConfig.CreateContext(dataBaseName))
            {
                var list= await context.Set<T>().AsNoTracking().Where(where).ToListAsync();
                int total = list.Count();
                if (isAsc)
                {
                    var temp = list
                                 .OrderBy(orderbyLambda)
                                 .Skip(pageSize * (pageIndex - 1))
                                 .Take(pageSize);
                    return new Tuple<IEnumerable<T>, int>(temp, total);
                }
                else
                {
                    var temp = list
                               .OrderByDescending(orderbyLambda)
                               .Skip(pageSize * (pageIndex - 1))
                               .Take(pageSize);
                    return new Tuple<IEnumerable<T>, int>(temp, total);
                }
            }
        }


        public async Task<bool> UpdateAsync(IEnumerable<T> entityList, string dataBaseName = null)
        {
            using (ProjectContext context = await DataBaseConfig.CreateContext(dataBaseName))
            {
                context.UpdateRange(entityList);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> UpdateAsync(T entity, string dataBaseName = null)
        {
            using (ProjectContext context = await DataBaseConfig.CreateContext(dataBaseName))
            {
                context.Update(entity);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
