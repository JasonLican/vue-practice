using ICan.Module.ReuqestModule;
using ICan.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICan.Module.Entity;

namespace ICan.Core.IService
{
    public interface IJWTService
    {
        Task<BaseResponse> CreateCredential(SysUser user);
        Task<BaseResponse> RefreshToken(RefreshTokenRequest refreshTokenRequest);
    }
}
