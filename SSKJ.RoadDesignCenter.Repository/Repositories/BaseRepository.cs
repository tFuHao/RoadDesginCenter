using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using SSKJ.Util.WebControl;
using SSKJ.RoadDesignCenter.Repository.IRepositories;

namespace SSKJ.RoadDesignCenter.Repository.Repositories
{
    /// <summary>
    /// 定义仓储模型中的数据标准操作
    /// </summary>
    /// <typeparam name="TEntity">动态实体类型</typeparam>
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        public IDatabase db;

        public BaseRepository(IDatabase idatabase)
        {
            this.db = idatabase;
        }

        #region 事物提交
        public int Commit()
        {
            return db.Commit();
        }
        #endregion

        #region 对象实体 查询
        public TEntity FindEntity(object keyValue)
        {
            return db.FindEntity<TEntity>(keyValue);
        }
        public TEntity FindEntity(Expression<Func<TEntity, bool>> condition)
        {
            return db.FindEntity<TEntity>(condition);
        }
        public IEnumerable<TEntity> FindList()
        {
            return db.FindList<TEntity>();
        }
        public IEnumerable<TEntity> FindList(Pagination pagination)
        {
            int total = pagination.records;
            var data = db.FindList<TEntity>(pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            pagination.records = total;
            return data;
        }
        public IEnumerable<TEntity> FindList(Expression<Func<TEntity, bool>> condition)
        {
            return db.FindList(condition);
        }

        public IEnumerable<TEntity> FindList(Expression<Func<TEntity, bool>> condition, Pagination pagination)
        {
            int total = pagination.records;
            var data = db.FindList<TEntity>(condition, pagination.sidx, pagination.sord.ToLower() == "asc" ? true : false, pagination.rows, pagination.page, out total);
            pagination.records = total;
            return data;
        }
        #endregion

        #region 对象实体 添加、修改、删除
        public void Insert(TEntity entity)
        {
            db.Insert<TEntity>(entity);
        }
        public void Insert(IEnumerable<TEntity> entity)
        {
            db.Insert<TEntity>(entity);
        }

        public void Delete(object keyValue)
        {
            db.Delete<TEntity>(keyValue);
        }
        public void Delete(object[] keyValue)
        {
            db.Delete<TEntity>(keyValue);
        }
        public void Delete(TEntity entity)
        {
            db.Delete<TEntity>(entity);
        }
        public void Delete(IEnumerable<TEntity> entity)
        {
            db.Delete<TEntity>(entity);
        }

        public void Update(TEntity entity)
        {
            db.Update<TEntity>(entity);
        }
        public void Update(IEnumerable<TEntity> entity)
        {
            db.Update<TEntity>(entity);
        }

        public void Delete(Expression<Func<TEntity, bool>> condition)
        {
            db.Delete<TEntity>(condition);
        }

        #endregion
    }
}
