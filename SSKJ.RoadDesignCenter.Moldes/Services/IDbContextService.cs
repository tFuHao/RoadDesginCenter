using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Models.Services
{
    public interface IDbContextService
    {
        ProjectModel.ProjectContext CreateProjectDbContext(string DbBaseName);
    }
}
