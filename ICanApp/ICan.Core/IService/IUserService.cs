using ICan.Module.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICan.Core.IService
{
    public interface IUserService
    {
        Task<SysUser> GetUser(string userName);
        Task UpdateAsync(SysUser user);
        Task<bool> CheckUser(string userName, string pwd);
    }
}
