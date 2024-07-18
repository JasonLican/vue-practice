using ICan.Core.Enum;
using ICan.Core.IService;
using ICan.Module;
using ICan.Module.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ICan.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersionInfo.V1))]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserService _userService;
        private readonly IJWTService _jwtService;
        public UserController(ILogger<UserController> logger, IUserService userService, IJWTService jWTService)
        {
            _logger = logger;
            _userService = userService;
            _jwtService = jWTService;
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<BaseResponse> Login(string userName, string password)
        {
            try
            {
                var b = await _userService.CheckUser(userName, password);
                if (!b)
                {
                    return new BaseResponse
                    {
                        Code = 201,
                        Message = "账号密码错误！"
                    };
                }
                SysUser user = await _userService.GetUser(userName);
                return await _jwtService.CreateCredential(user);
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Code = 500,
                    Message = ex.Message,
                };
            }
        }
    }
}
