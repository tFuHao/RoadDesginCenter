using Microsoft.EntityFrameworkCore;
using SSKJ.RoadDesignCenter.Models.SystemModel;
using SSKJ.RoadDesignCenter.Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Repository.Repositories
{
    public class ModuleRepository : BaseRepository<Module>, IModuleRepository
    {
        public ModuleRepository(IDatabase db) : base(db)
        {

        }
    }
}
