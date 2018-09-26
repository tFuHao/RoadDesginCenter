using SSKJ.RoadDesignCenter.Models.ProjectModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Busines.IBusines.Project
{
    public interface IPrjUserBusines:IPrjBaseBusines<User>
    {
        List<User> GetList();
    }
}
