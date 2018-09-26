using SSKJ.RoadDesignCenter.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SSKJ.RoadDesignCenter.Busines.IBusines.System
{
    public interface ISysModuleBusines:ISysBaseBusines<Module>
    {
        List<Module> GetList();
    }
}
