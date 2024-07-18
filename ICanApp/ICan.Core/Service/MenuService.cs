using AutoMapper;
using ICan.Core.EFDbContext;
using ICan.Core.Enum;
using ICan.Core.IService;
using ICan.Module.Entity;
using ICan.Module.EntityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICan.Core.Service
{
    public class MenuService : IMenuService
    {
        public readonly ApplicationDbContext _dbContext;
        public readonly IMapper _mapper;
        public MenuService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<MenuTreeDataDto>> GetMenuTree(SysUser user)
        {
            var userMenus = _dbContext.V_User_Roles.Where(p => p.UserID == user.ID).ToList();
            var menuList = userMenus.Where(a => a.IsActive == true && a.Type < (int)MenuType.three).ToList();
            var tempList = new List<SysMenus>();
            if (menuList.Count > 0)
            {
                tempList = _mapper.Map<List<V_User_Role>,List<SysMenus>>(menuList);
            }
            List<SysMenus> menuTree = new List<SysMenus>();
            foreach (var menu in tempList.Where(a => (a.ParentID == null || a.ParentID == 0)).OrderBy(a => a.Sort).ToList())
            {
                menu.Children = await GetMenuChildTree(tempList, menu);
                menuTree.Add(menu);
            }
            return _mapper.Map<List<SysMenus>, List<MenuTreeDataDto>>(menuTree); 
        }
        public async Task<List<MenuTreeDataDto>> GetMenuTree()
        {
            var menuList = _dbContext.Menus.Where(a => a.IsActive == true && a.Type < (int)MenuType.three).ToList();
            List<SysMenus> menuTree = new List<SysMenus>();
            foreach (var menu in menuList.Where(a => (a.ParentID == null || a.ParentID == 0)).OrderBy(a => a.Sort).ToList())
            {
                menu.Meta.Title = menu.Label;
                menu.Children = await GetMenuChildTree(menuList, menu);
                menuTree.Add(menu);
            }
            return _mapper.Map<List<SysMenus>, List<MenuTreeDataDto>>(menuTree);
        }
        public async Task<List<PermissionDataDto>> GetPermissionTree()
        {
            var menuList = _dbContext.Menus.Where(a => a.IsActive == true).ToList();
            List<SysMenus> menuTree = new List<SysMenus>();
            foreach (var menu in menuList.Where(a => (a.ParentID == null || a.ParentID == 0)).OrderBy(a => a.Sort).ToList())
            {
                menu.Children = await GetMenuChildTree(menuList, menu);
                menuTree.Add(menu);
            }
            return _mapper.Map<List<SysMenus>, List<PermissionDataDto>>(menuTree);

        }
        #region 菜单增删改查
       
        #endregion

        public async Task<List<string>> GetPermission()
        {
           var permissions= _dbContext.V_User_Roles.Where(a => a.IsActive == true && a.Type >= (int)MenuType.three).ToList();
            return permissions.Select(e=>e.Code).ToList();
        }
        private async Task<List<SysMenus>> GetMenuChildTree(List<SysMenus> menus, SysMenus pre)
        {
            try
            {
                List<SysMenus> childrenList = new List<SysMenus>();
                var childrens = menus.Where(p => p.ParentID == pre.ID);
                foreach (var chl in childrens)
                {
                    chl.Meta.Title = chl.Label;
                    chl.Meta.ParentTitle = pre.Label;
                    chl.Children = await GetMenuChildTree(menus, chl);
                    childrenList.Add(chl);
                }
                return childrenList;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
