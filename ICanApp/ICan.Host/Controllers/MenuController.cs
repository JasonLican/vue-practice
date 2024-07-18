using ICan.Core.Enum;
using ICan.Core.IService;
using ICan.Module;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ICan.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersionInfo.V1))]
    public class MenuController : ControllerBase
    {
        private readonly ILogger<MenuController> _logger;
        private readonly IMenuService _menuService;
        public MenuController(ILogger<MenuController> logger, IMenuService menuService)
        {
            _logger = logger;
            _menuService = menuService;
        }
        [HttpGet]
        public async Task<BaseResponse> Get()
        {
            try
            {
                var result = await _menuService.GetMenuTree();
                return new BaseResponse
                {
                    Data = result,
                    Code = 200,
                    Message = "成功"
                };
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
