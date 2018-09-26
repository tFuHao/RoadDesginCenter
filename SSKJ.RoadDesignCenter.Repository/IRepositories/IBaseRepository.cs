using SSKJ.Util.WebControl;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Repository.IRepositories
{
    /// <summary>
    /// 描 述：定义仓储模型中的数据标准操作接口
    /// </summary>
    /// <typeparam name="T">动态实体类型</typeparam>
    public interface IBaseRepository<T> where T : class, new()
    {
        int Commit();

        void Insert(T entity);
        void Insert(IEnumerable<T> entity);
        void Delete(T entity);
        void Delete(IEnumerable<T> entity);
        void Delete(object keyValue);
        void Delete(object[] keyValue);
        void Delete(Expression<Func<T, bool>> condition);
        void Update(T entity);
        void Update(IEnumerable<T> entity);

        T FindEntity(object keyValue);
        T FindEntity(Expression<Func<T, bool>> condition);
        IEnumerable<T> FindList();
        IEnumerable<T> FindList(Pagination pagination);
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition);
        IEnumerable<T> FindList(Expression<Func<T, bool>> condition, Pagination pagination);
    }
}
