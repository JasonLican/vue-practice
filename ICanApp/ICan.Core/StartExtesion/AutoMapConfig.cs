using AutoMapper;
using ICan.Module.Entity;
using ICan.Module.EntityDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICan.Core.StartExtesion
{
    public class AutoMapConfig : Profile
    {
        public AutoMapConfig()
        {
            CreateMap<SysMenus, MenuTreeDataDto>().ForMember(d => d.Name, q => { q.MapFrom(p => p.Code); }).ReverseMap();
            CreateMap<SysMenus, PermissionDataDto>();
            CreateMap<SysMenus,V_User_Role>().ReverseMap();
        }
    }
}
