using SSKJ.RoadDesignCenter.Models.SystemModel;
using SSKJ.RoadDesignCenter.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Repository.Repositories
{
    public class ColumnRepository : BaseRepository<ModuleColumn>, IColumnRepository
    {
        public ColumnRepository(IDatabase db) : base(db)
        {

        }
    }
}
