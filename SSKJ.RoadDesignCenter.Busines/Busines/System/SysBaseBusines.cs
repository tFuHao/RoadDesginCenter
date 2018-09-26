using SSKJ.RoadDesignCenter.Busines.IBusines.System;
using SSKJ.RoadDesignCenter.Service.IDalService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace SSKJ.RoadDesignCenter.Busines.Busines.System
{
   public class SysBaseBusines<T>:ISysBaseBusines<T>where T:class,new()
    {
        protected ISystemBaseService<T> DBService;
        public SysBaseBusines(ISystemBaseService<T> baseService)
        {
            DBService = baseService;
        }

        public T AddEntity(T entity, bool IsSave)
        {
            entity = DBService.AddEntity(entity);
            if (IsSave)
            {
                if (SaveChanges() > 0)
                    return null;
            }
            return entity;
        }

        public IQueryable<T> LoadEntites(Expression<Func<T, bool>> where)
        {
            return DBService.LoadEntites(where);
        }

        public int SaveChanges()
        {
            return DBService.SaveChanges();
        }
    }
}
