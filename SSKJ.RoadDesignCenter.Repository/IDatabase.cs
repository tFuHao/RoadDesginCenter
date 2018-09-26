using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace SSKJ.RoadDesignCenter.Repository
{
    /// <summary>
    /// 操作数据库接口
    /// </summary>
    public interface IDatabase
    {
        //IDatabase BeginTrans();
        int Commit();
        //void Rollback();
        void Insert<T>(T entity) where T : class;
        void Insert<T>(IEnumerable<T> entities) where T : class;
        void Delete<T>(T entity) where T : class;
        void Delete<T>(IEnumerable<T> entities) where T : class;
        void Delete<T>(Expression<Func<T, bool>> condition) where T : class, new();
        void Delete<T>(object KeyValue) where T : class;
        void Delete<T>(object[] KeyValue) where T : class;
        void Update<T>(T entity) where T : class;
        void Update<T>(IEnumerable<T> entities) where T : class;
        T FindEntity<T>(object KeyValue) where T : class;
        T FindEntity<T>(Expression<Func<T, bool>> condition) where T : class, new();
        IEnumerable<T> FindList<T>() where T : class, new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition) where T : class, new();
        IEnumerable<T> FindList<T>(string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class, new();
        IEnumerable<T> FindList<T>(Expression<Func<T, bool>> condition, string orderField, bool isAsc, int pageSize, int pageIndex, out int total) where T : class, new();
    }
}
