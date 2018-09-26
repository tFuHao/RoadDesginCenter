using Microsoft.EntityFrameworkCore;
using SSKJ.RoadDesignCenter.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Repository.MySQL.System
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, new()
    {
        public async Task<bool> CreateAsync(T entity, string connectionString = null)
        {
            using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
            {
                await context.Set<T>().AddAsync(entity);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> CreateAsync(IEnumerable<T> entityList, string connectionString = null)
        {
            using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
            {
                await context.Set<T>().AddRangeAsync(entityList);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(string keyValue, string connectionString = null)
        {
            using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
            {
                context.Set<T>().Remove(context.Find<T>(keyValue));
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string connectionString = null)
        {
            using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
            {
                List<T> entitys = new List<T>();
                entitys.ForEach(async u =>
                {
                    entitys.Add(await context.FindAsync<T>(u));
                });
                return await DeleteAsync(entitys);
            }
        }

        public async Task<bool> DeleteAsync(T entity, string connectionString = null)
        {
            using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
            {
                context.Set<T>().Remove(entity);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> DeleteAsync(IEnumerable<T> entityList, string connectionString = null)
        {
            using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
            {
                context.Set<T>().RemoveRange(entityList);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<IEnumerable<T>> GetListAsync(string connectionString = null)
        {
            using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
            {
                return await context.Set<T>().AsNoTracking().ToListAsync();
            }
        }

        public async Task<T> GetEntityAsync(string keyValue, string connectionString = null)
        {
            using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
            {
                return await context.FindAsync<T>(keyValue);
            }
        }

        public async Task<T> GetEntityAsync(Expression<Func<T, bool>> where, string connectionString = null)
        {
            using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
            {
                return await context.Set<T>().FirstOrDefaultAsync(where);
            }
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> where, string connectionString = null)
        {
            using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
            {
                return await context.Set<T>().AsNoTracking().Where(where).ToListAsync();
            }
        }

        //public async  Task<Tuple<IEnumerable<T>, int>> GetListAsync(Expression<Func<T, bool>> where, string orderField, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        //{
        //    using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
        //    {
        //        var tempData = context.Set<T>().AsQueryable();
        //       int total =await tempData.CountAsync();
        //        if (!string.IsNullOrWhiteSpace(orderField))
        //        {
        //            string[] _order = orderField.Split(",");
        //            MethodCallExpression resultExp = null;
        //            foreach (string item in _order)
        //            {
        //                string _orderPart = item;
        //                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
        //                string[] _orderArry = _orderPart.Split(' ');
        //                string _orderField = _orderArry[0];
        //                bool sort = isAsc;
        //                if (_orderArry.Length == 2)
        //                {
        //                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
        //                }
        //                var parameter = Expression.Parameter(typeof(T), "t");
        //                var property = typeof(T).GetProperty(_orderField);
        //                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
        //                var orderByExp = Expression.Lambda(propertyAccess, parameter);
        //                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(T), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
        //            }
        //            tempData = tempData.Provider.CreateQuery<T>(resultExp);
        //        }
        //        else
        //        {
        //            tempData = tempData.Skip(pageSize * (pageIndex - 1)).Take(pageSize).AsQueryable();
        //        }
        //        return new Tuple<IEnumerable<T>, int>(tempData.AsNoTracking(), total);
        //    }
        //}
        public async Task<Tuple<IEnumerable<T>, int>> GetListAsync<Tkey>(Expression<Func<T, bool>> where, Func<T, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null)
        {
            using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
            {
                int total = await context.Set<T>().AsNoTracking().CountAsync();
                if (isAsc)
                {
                    var temp = context.Set<T>().Where(where)
                                 .OrderBy(orderbyLambda)
                                 .Skip(pageSize * (pageIndex - 1))
                                 .Take(pageSize).ToList();
                    return new Tuple<IEnumerable<T>, int>(temp, total);
                }
                else
                {
                    var temp = context.Set<T>().Where(where)
                               .OrderByDescending(orderbyLambda)
                               .Skip(pageSize * (pageIndex - 1))
                               .Take(pageSize);
                    return new Tuple<IEnumerable<T>, int>(temp, total);
                }
            }
        }

        public async Task<bool> UpdateAsync(IEnumerable<T> entityList, string connectionString = null)
        {
            using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
            {
                context.UpdateRange(entityList);
                return await context.SaveChangesAsync() > 0;
            }
        }

        public async Task<bool> UpdateAsync(T entity, string connectionString = null)
        {
            using (SystemContext context = DataBaseConfig.CreateContext(connectionString))
            {
                context.Update(entity);
                return await context.SaveChangesAsync() > 0;
            }
        }
    }
}
