using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SSKJ.RoadDesignCenter.Busines.IBusines.System
{
    public interface ISysBaseBusines<T> where T : class, new()
    {
        T AddEntity(T entity, bool IsSave);
        IQueryable<T> LoadEntites(Expression<Func<T, bool>> where);
        int SaveChanges();
    }
}
