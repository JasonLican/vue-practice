using ICan.Core.Enum;
using ICan.Core.IService;
using ICan.Module;
using ICan.Module.Entity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace ICan.Host.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiExplorerSettings(IgnoreApi = false, GroupName = nameof(ApiVersionInfo.V1))]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IMenuService _menuService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IMenuService menuService)
        {
            _logger = logger;
            _menuService = menuService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<BaseResponse> Get()
        {
            try
            {
                var result = await _menuService.GetMenuTree();
                return new BaseResponse
                {
                    Data = result,
                    Code = 200,
                    Message = "³É¹¦"
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
        [HttpGet,Route("test")]
        public IEnumerable<WeatherForecast> Gettest()
        {
            var result = _menuService.GetMenuTree();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
