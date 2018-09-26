using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.IBusines
{
    /// <summary>
    /// 基类业务接口定义
    /// </summary>
    public interface IBaseBusines<T> where T : class
    {
        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="entity">要创建的实体</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        Task<bool> CreateAsync(T entity, string connectionString = null);

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entityList">要创建的实体</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        Task<bool> CreateAsync(IEnumerable<T> entityList, string connectionString = null);

        /// <summary>
        /// 根据主键获取一个实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        Task<T> GetEntityAsync(string keyValue, string connectionString = null);

        /// <summary>
        /// 根据条件获取一个实体
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        Task<T> GetEntityAsync(Expression<Func<T, bool>> where, string connectionString = null);

        /// <summary>
        /// 根据条件where获取数据
        /// </summary>
        /// <param name="where">条件where</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> where, string connectionString = null);

        /// <summary>
        /// 根据条件where获取分页数据
        /// </summary>
        /// <param name="where">查询条件</param>
        /// <param name="orderbyLambda">排序条件</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="total">数据总数</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        Task<Tuple<IEnumerable<T>, int>> GetListAsync<Tkey>(Expression<Func<T, bool>> where, Func<T, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string connectionString = null);

        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetListAsync(string connectionString = null);

        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="entity">要修改的实体</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(T entity, string connectionString = null);

        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="entityList">要修改的实体</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(IEnumerable<T> entityList, string connectionString = null);

        /// <summary>
        /// 根据主键删除一个实体
        /// </summary>
        /// <param name="keyValue">主键</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string keyValue, string connectionString = null);

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="keyValues">要删除的实体的主键</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(string[] keyValues, string connectionString = null);

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(T entity, string connectionString = null);

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="entityList">要删除的实体</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(IEnumerable<T> entityList, string connectionString = null);
    }
}
