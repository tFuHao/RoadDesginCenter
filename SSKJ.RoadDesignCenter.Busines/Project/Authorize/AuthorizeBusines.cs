using SSKJ.RoadDesignCenter.IBusines.Project.Authorize;
using SSKJ.RoadDesignCenter.IRepository.Project.Authorize;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using SSKJ.RoadDesignCenter.IRepository.System;
using SSKJ.RoadDesignCenter.Models;
using SSKJ.RoadDesignCenter.Models.SystemModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Busines.Project.Authorize
{
    public class AuthorizeBusines : IAuthorizeBusines
    {
        private readonly IAuthorizeRepository authorizeRepo;
        private readonly IModuleRepository moduleRepo;
        private readonly IButtonRepository buttonRepo;
        private readonly IColumnRepository columnRepo;
        private readonly IRouteRepository routeRepo;

        public AuthorizeBusines(IAuthorizeRepository authorizeRepo, IModuleRepository moduleRepo, IButtonRepository buttonRepo, IColumnRepository columnRepo, IRouteRepository routeRepo)
        {
            this.authorizeRepo = authorizeRepo;
            this.moduleRepo = moduleRepo;
            this.buttonRepo = buttonRepo;
            this.columnRepo = columnRepo;
            this.routeRepo = routeRepo;
        }

        /// <summary>
        /// 获取功能权限
        /// </summary>
        /// <param name="category">1用户权限 2角色权限</param>
        /// <param name="objectId">用户ID或角色ID</param>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Module>> GetModuleAuthorizes(int category, string objectId, string dataBaseName)
        {
            var modules = await moduleRepo.GetListAsync(m => m.EnabledMark == 1);
            var _modules = new List<Module>();
            if (objectId == "System")
            {
                _modules = modules.ToList().FindAll(m => m.ParentId == "0" || m.ModuleId == "c1d4085e-df18-4584-8315-f14da229f6c9");
                _modules.ToList().AddRange(GetModules(modules, "c1d4085e-df18-4584-8315-f14da229f6c9"));
            }
            else if (objectId == "PrjManager")
            {
                _modules = modules.ToList().FindAll(m => m.ParentId == "0" || m.ModuleId == "ff01c3d3-9690-4848-8001-066831f6250c");
                _modules.ToList().AddRange(GetModules(modules, "ff01c3d3-9690-4848-8001-066831f6250c"));
            }
            else if (objectId == "PrjAdmin")
            {
                var ids = new List<string>();
                var mod = GetModules(modules, "c1d4085e-df18-4584-8315-f14da229f6c9");
                mod.ToList().AddRange(GetModules(modules, "ff01c3d3-9690-4848-8001-066831f6250c"));
                ids = mod.Select(s => s.ModuleId).ToList();
                ids.AddRange(new List<string>
                {
                    "c1d4085e-df18-4584-8315-f14da229f6c9",
                    "ff01c3d3-9690-4848-8001-066831f6250c"
                });
                _modules = modules.ToList().FindAll(m => !(ids.Any(id => id == m.ModuleId)));
            }
            else
            {
                var authorizes = await authorizeRepo.GetListAsync(a => a.Category == category && a.ObjectId == objectId && a.ItemType == 1, dataBaseName);
                _modules = modules.ToList().FindAll(m => authorizes.Any(a => a.ItemId == m.ModuleId));
            }

            //return TreeData.ModuleTreeJson(modules.ToList());
            return _modules.OrderBy(o => o.SortCode);
        }
        public IEnumerable<Module> GetModules(IEnumerable<Module> list, string pId)
        {
            var _list = list.Where(f => f.ParentId == pId);

            return _list.Concat(_list.SelectMany(t => GetModules(list, t.ModuleId)));
        }
        /// <summary>
        /// 获取功能按钮权限
        /// </summary>
        /// <param name="category">1用户权限 2角色权限</param>
        /// <param name="objectId">用户ID或角色ID</param>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleButton>> GetButtonAuthorizes(int category, string objectId, string dataBaseName)
        {
            var modules = await moduleRepo.GetListAsync(m => m.EnabledMark == 1);
            var buttons = await buttonRepo.GetListAsync();
            var _buttons = new List<ModuleButton>();
            if (objectId == "System")
            {
                var _modules = GetModules(modules, "c1d4085e-df18-4584-8315-f14da229f6c9").Select(m => m.ModuleId).ToList();
                _modules.Add("c1d4085e-df18-4584-8315-f14da229f6c9");
                _buttons = buttons.ToList().FindAll(m => _modules.Any(id => id == m.ModuleId));
            }
            else if (objectId == "PrjManager")
            {
                var _modules = GetModules(modules, "ff01c3d3-9690-4848-8001-066831f6250c").Select(m => m.ModuleId).ToList();
                _modules.Add("ff01c3d3-9690-4848-8001-066831f6250c");
                _buttons = buttons.ToList().FindAll(m => _modules.Any(id => id == m.ModuleId));
            }
            else if (objectId == "PrjAdmin")
            {
                var _modules = GetModules(modules, "ff01c3d3-9690-4848-8001-066831f6250c").Select(m => m.ModuleId).ToList();
                _modules.Add("ff01c3d3-9690-4848-8001-066831f6250c");
                _modules.AddRange(GetModules(modules, "c1d4085e-df18-4584-8315-f14da229f6c9").Select(m => m.ModuleId).ToList());
                _modules.Add("c1d4085e-df18-4584-8315-f14da229f6c9");

                _buttons = buttons.ToList().FindAll(m => !(_modules.Any(id => id == m.ModuleId)));
            }
            else
            {
                var authorizes = await authorizeRepo.GetListAsync(a => a.Category == category && a.ObjectId == objectId && a.ItemType == 2, dataBaseName);
                _buttons = buttons.ToList().FindAll(m => authorizes.Any(a => a.ItemId == m.ModuleId));
            }

            //return TreeData.ButtonTreeJson(buttons.OrderBy(o => o.SortCode).ToList());
            return _buttons.OrderBy(o => o.SortCode);
        }

        /// <summary>
        /// 获取功能视图权限
        /// </summary>
        /// <param name="category">1用户权限 2角色权限</param>
        /// <param name="objectId">用户ID或角色ID</param>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<ModuleColumn>> GetColumnAuthorizes(int category, string objectId, string dataBaseName)
        {
            var modules = await moduleRepo.GetListAsync(m => m.EnabledMark == 1);
            var columns = await columnRepo.GetListAsync();
            var _columns = new List<ModuleColumn>();
            if (objectId == "System")
            {
                var _modules = GetModules(modules, "c1d4085e-df18-4584-8315-f14da229f6c9").Select(m => m.ModuleId).ToList();
                _modules.Add("c1d4085e-df18-4584-8315-f14da229f6c9");
                _columns = columns.ToList().FindAll(m => _modules.Any(id => id == m.ModuleId));
            }
            else if (objectId == "PrjManager")
            {
                var _modules = GetModules(modules, "ff01c3d3-9690-4848-8001-066831f6250c").Select(m => m.ModuleId).ToList();
                _modules.Add("ff01c3d3-9690-4848-8001-066831f6250c");
                _columns = columns.ToList().FindAll(m => _modules.Any(id => id == m.ModuleId));
            }
            else if (objectId == "PrjAdmin")
            {
                var _modules = GetModules(modules, "ff01c3d3-9690-4848-8001-066831f6250c").Select(m => m.ModuleId).ToList();
                _modules.Add("ff01c3d3-9690-4848-8001-066831f6250c");
                _modules.AddRange(GetModules(modules, "c1d4085e-df18-4584-8315-f14da229f6c9").Select(m => m.ModuleId).ToList());
                _modules.Add("c1d4085e-df18-4584-8315-f14da229f6c9");
                _columns = columns.ToList().FindAll(m => !(_modules.Any(id => id == m.ModuleId)));
            }
            else
            {
                var authorizes = await authorizeRepo.GetListAsync(a => a.Category == category && a.ObjectId == objectId && a.ItemType == 3, dataBaseName);
                _columns = columns.ToList().FindAll(m => authorizes.Any(a => a.ItemId == m.ModuleId));
            }

            return _columns.ToList().OrderBy(o => o.SortCode);
        }
        /// <summary>
        /// 获取路线权限
        /// </summary>
        /// <param name="category">1用户权限 2角色权限</param>
        /// <param name="objectId">用户ID或角色ID</param>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Models.ProjectModel.Route>> GetRouteAuthorizes(int category, string objectId, string dataBaseName)
        {
            if (objectId == "System" || objectId == "PrjManager" || objectId == "PrjAdmin")
                return null;
            else
            {
                var routes = await routeRepo.GetListAsync(dataBaseName);
                var authorizes = await authorizeRepo.GetListAsync(a => a.Category == category && a.ObjectId == objectId && a.ItemType == 4, dataBaseName);
                var _routes = routes.ToList().FindAll(m => authorizes.Any(a => a.ItemId == m.RouteId));
                return _routes.OrderBy(o => o.CreateDate);
            }
        }

        public async Task<PermissionModel> GetModuleAndRoutePermission(int category, string objectId, string dataBaseName)
        {
            var existList = await authorizeRepo.GetListAsync(a => a.Category == category && a.ObjectId == objectId, dataBaseName);
            var moduleList = await moduleRepo.GetListAsync(m => m.EnabledMark == 1&&m.ModuleId!="c1d4085e-df18-4584-8315-f14da229f6c9"&&m.ModuleId!= "ff01c3d3-9690-4848-8001-066831f6250c");
            var moduleTreeList = new List<TreeEntity>();
            var moduleChecked = new List<string>();
            var moduleHalfChecked = new List<string>();

            var moduleIds = GetModules(moduleList, "c1d4085e-df18-4584-8315-f14da229f6c9").Select(m=>m.ModuleId);
            moduleIds.ToList().AddRange(GetModules(moduleList, "ff01c3d3-9690-4848-8001-066831f6250c").Select(m=>m.ModuleId));

            moduleList = moduleList.ToList().FindAll(m => !(moduleIds.Any(id => id == m.ModuleId)));

            moduleList.OrderBy(o => o.SortCode).ToList().ForEach(module =>
            {
                var check = existList.Count(t => t.ItemId == module.ModuleId && t.ItemType == 1 && t.IsHalf == 0);
                var halfCheck = existList.Count(t => t.ItemId == module.ModuleId && t.ItemType == 1 && t.IsHalf == 1);
                if (check > 0)
                    moduleChecked.Add(module.ModuleId);
                if (halfCheck > 0)
                    moduleHalfChecked.Add(module.ModuleId);
                TreeEntity tree = new TreeEntity
                {
                    Id = module.ModuleId,
                    ParentId = module.ParentId,
                    Name = module.FullName
                };
                moduleTreeList.Add(tree);
            });

            var buttonList = await buttonRepo.GetListAsync();
            var columnList = await columnRepo.GetListAsync();
            var routeList = await routeRepo.GetListAsync(dataBaseName);
            var routeTreeList = new List<TreeEntity>();
            var routeChecked = new List<string>();
            routeList.OrderBy(o => o.CreateDate).ToList().ForEach(route =>
            {
                var check = existList.Count(t => t.ItemId == route.RouteId && t.ItemType == 4 && t.IsHalf == 0);
                if (check > 0)
                    routeChecked.Add(route.RouteId);
                TreeEntity tree = new TreeEntity
                {
                    Id = route.RouteId,
                    ParentId = route.ParentId,
                    Name = route.RouteName
                };
                routeTreeList.Add(tree);
            });

            PermissionModel permissionModel = new PermissionModel
            {
                Authorizes = existList.ToList(),
                ModulePermission = moduleTreeList.TreeToJson(),
                ModuleCheckeds = moduleChecked,
                RoutePermission = routeTreeList.TreeToJson(),
                RouteCheckeds = routeChecked,
                ButtonList = buttonList.OrderBy(o => o.SortCode).ToList(),
                ColumnList = columnList.OrderBy(o => o.SortCode).ToList(),
                ModuleHalfCheckeds = moduleHalfChecked
            };
            return permissionModel;
        }
        public PermissionModel GetButtonAndColumnPermission(List<string> halfKeys, List<string> checkedKeys, string strAuthorizes, string strModules, string strButtons, string strColumns)
        {
            List<Models.ProjectModel.Authorize> authorizes = Utility.Tools.JsonUtils.ToObject<List<Models.ProjectModel.Authorize>>(strAuthorizes);
            List<TreeEntity> modules = Utility.Tools.JsonUtils.ToObject<List<TreeEntity>>(strModules);
            List<ModuleButton> buttons = Utility.Tools.JsonUtils.ToObject<List<ModuleButton>>(strButtons);
            List<ModuleColumn> columns = Utility.Tools.JsonUtils.ToObject<List<ModuleColumn>>(strColumns);

            var moduleId = halfKeys.Concat(checkedKeys).Distinct().ToList();
            var mods = modules.Where(m => moduleId.Any(c => c == m.Id)).ToList();
            var btns = buttons.Where(b => checkedKeys.Any(c => c == b.ModuleId)).ToList();
            var cols = columns.Where(b => checkedKeys.Any(c => c == b.ModuleId)).ToList();

            var treeList = new List<TreeEntity>();
            mods.ForEach(module =>
            {
                TreeEntity tree = new TreeEntity
                {
                    Id = module.Id,
                    ParentId = module.ParentId,
                    Name = module.Name
                };
                treeList.Add(tree);
            });

            var buttonTreeList = new List<TreeEntity>();
            buttonTreeList.AddRange(treeList);
            var buttonChecked = new List<string>();
            btns.OrderBy(o => o.SortCode).ToList().ForEach(button =>
            {
                var check = authorizes.Count(t => t.ItemId == button.ModuleButtonId && t.ItemType == 2 && t.IsHalf == 0);
                if (check > 0)
                    buttonChecked.Add(button.ModuleButtonId);
                TreeEntity tree = new TreeEntity
                {
                    Id = button.ModuleButtonId,
                    ParentId = button.ParentId == "0" ? button.ModuleId : button.ParentId,
                    Name = button.FullName
                };
                buttonTreeList.Add(tree);
            });
            var columnTreeList = new List<TreeEntity>();
            columnTreeList.AddRange(treeList);
            var columnChecked = new List<string>();
            cols.OrderBy(o => o.SortCode).ToList().ForEach(column =>
            {
                var check = authorizes.Count(t => t.ItemId == column.ModuleColumnId && t.ItemType == 3 && t.IsHalf == 0);
                if (check > 0)
                    columnChecked.Add(column.ModuleColumnId);
                TreeEntity tree = new TreeEntity
                {
                    Id = column.ModuleColumnId,
                    ParentId = column.ParentId ?? column.ModuleId,
                    Name = column.FullName
                };
                columnTreeList.Add(tree);
            });
            PermissionModel permissionModel = new PermissionModel
            {
                ButtonPermission = buttonTreeList.TreeToJson(),
                ButtonCheckeds = buttonChecked,
                ColumnPermission = columnTreeList.TreeToJson(),
                ColumnCheckeds = columnChecked
            };
            return permissionModel;
        }

        public async Task<bool> SavePermission(string userId, string currentUserId, int category, List<Models.ProjectModel.AuthorizeIdType> modules, List<Models.ProjectModel.AuthorizeIdType> buttons, List<Models.ProjectModel.AuthorizeIdType> columns, List<Models.ProjectModel.AuthorizeIdType> routes, string dataBaseName)
        {
            var delAuthorizes = await authorizeRepo.GetListAsync(a => a.ObjectId == userId, dataBaseName);
            await authorizeRepo.DeleteAsync(delAuthorizes, dataBaseName);

            var addAuthorizes = new List<Models.ProjectModel.Authorize>();
            modules.ForEach(module =>
            {
                var entity = new Models.ProjectModel.Authorize()
                {
                    AuthorizeId = Guid.NewGuid().ToString(),
                    Category = category,
                    ItemType = 1,
                    ItemId = module.Id,
                    ObjectId = userId,
                    CreateDate = DateTime.Now,
                    CreateUserId = currentUserId,
                    IsHalf = module.IsHalf
                };
                addAuthorizes.Add(entity);
            });
            buttons.ForEach(button =>
            {
                var entity = new Models.ProjectModel.Authorize()
                {
                    AuthorizeId = Guid.NewGuid().ToString(),
                    Category = category,
                    ItemType = 2,
                    ItemId = button.Id,
                    ObjectId = userId,
                    CreateDate = DateTime.Now,
                    CreateUserId = currentUserId,
                    IsHalf = button.IsHalf
                };
                addAuthorizes.Add(entity);
            });
            columns.ForEach(column =>
            {
                var entity = new Models.ProjectModel.Authorize()
                {
                    AuthorizeId = Guid.NewGuid().ToString(),
                    Category = category,
                    ItemType = 3,
                    ItemId = column.Id,
                    ObjectId = userId,
                    CreateDate = DateTime.Now,
                    CreateUserId = currentUserId,
                    IsHalf = column.IsHalf
                };
                addAuthorizes.Add(entity);
            });
            routes.ForEach(route =>
            {
                var entity = new Models.ProjectModel.Authorize()
                {
                    AuthorizeId = Guid.NewGuid().ToString(),
                    Category = category,
                    ItemType = 4,
                    ItemId = route.Id,
                    ObjectId = userId,
                    CreateDate = DateTime.Now,
                    CreateUserId = currentUserId,
                    IsHalf = route.IsHalf
                };
                addAuthorizes.Add(entity);
            });
            return await authorizeRepo.CreateAsync(addAuthorizes, dataBaseName);
        }

        public async Task<bool> CreateAsync(Models.ProjectModel.Authorize entity, string dataBaseName = null)
        {
            return await authorizeRepo.CreateAsync(entity, dataBaseName);
        }

        public async Task<bool> CreateAsync(IEnumerable<Models.ProjectModel.Authorize> entityList, string dataBaseName = null)
        {
            return await authorizeRepo.CreateAsync(entityList, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string keyValue, string dataBaseName = null)
        {
            return await authorizeRepo.DeleteAsync(keyValue, dataBaseName);
        }

        public async Task<bool> DeleteAsync(string[] keyValues, string dataBaseName = null)
        {
            return await authorizeRepo.DeleteAsync(keyValues, dataBaseName);
        }

        public async Task<bool> DeleteAsync(Models.ProjectModel.Authorize entity, string dataBaseName = null)
        {
            return await authorizeRepo.DeleteAsync(entity, dataBaseName);
        }

        public async Task<bool> DeleteAsync(IEnumerable<Models.ProjectModel.Authorize> entityList, string dataBaseName = null)
        {
            return await authorizeRepo.DeleteAsync(entityList, dataBaseName);
        }

        public async Task<Models.ProjectModel.Authorize> GetEntityAsync(Expression<Func<Models.ProjectModel.Authorize, bool>> where, string dataBaseName = null)
        {
            return await authorizeRepo.GetEntityAsync(where, dataBaseName);
        }

        public async Task<Models.ProjectModel.Authorize> GetEntityAsync(string keyValue, string dataBaseName = null)
        {
            return await authorizeRepo.GetEntityAsync(keyValue, dataBaseName);
        }

        public async Task<IEnumerable<Models.ProjectModel.Authorize>> GetListAsync(Expression<Func<Models.ProjectModel.Authorize, bool>> where, string dataBaseName = null)
        {
            return await authorizeRepo.GetListAsync(where, dataBaseName);
        }

        public async Task<Tuple<IEnumerable<Models.ProjectModel.Authorize>, int>> GetListAsync<Tkey>(Expression<Func<Models.ProjectModel.Authorize, bool>> where, Func<Models.ProjectModel.Authorize, Tkey> orderbyLambda, bool isAsc, int pageSize, int pageIndex, string dataBaseName = null)
        {
            return await authorizeRepo.GetListAsync(where, orderbyLambda, isAsc, pageSize, pageIndex, dataBaseName);
        }

        public async Task<IEnumerable<Models.ProjectModel.Authorize>> GetListAsync(string dataBaseName = null)
        {
            return await authorizeRepo.GetListAsync(dataBaseName);
        }

        public async Task<bool> UpdateAsync(Models.ProjectModel.Authorize entity, string dataBaseName = null)
        {
            return await authorizeRepo.UpdateAsync(entity, dataBaseName);
        }

        public async Task<bool> UpdateAsync(IEnumerable<Models.ProjectModel.Authorize> entityList, string dataBaseName = null)
        {
            return await authorizeRepo.UpdateAsync(entityList, dataBaseName);
        }
    }
}
