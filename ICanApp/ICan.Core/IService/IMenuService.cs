using ICan.Module.Entity;
using ICan.Module.EntityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICan.Core.IService
{
    public interface IMenuService
    {
        Task<List<MenuTreeDataDto>> GetMenuTree();
        Task<List<MenuTreeDataDto>> GetMenuTree(SysUser user);
        Task<List<PermissionDataDto>> GetPermissionTree();
    }
}
