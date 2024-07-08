using Broxel.Logger.Web.Extensions;
using Broxel.Logger.Web.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Broxel.Logger.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }


        ////Log Operativo
        //_logger.LogInformation("Log Operativo");   

        ////Log Auditoría
        //_logger.LogAuditError("Log Auditoria", Web.Models.MovementTypes.Add, User.Identity?.Name ?? "No User", new { }, new { });
        //_logger.LogAuditWarning("Log Auditoria", Web.Models.MovementTypes.Add, User.Identity?.Name ?? "No User", new { }, new { });
        //_logger.LogAuditInformation("Log Auditoria", Web.Models.MovementTypes.Add, User.Identity?.Name ?? "No User", new { }, new { });
        //_logger.LogAuditDebug("Log Auditoria", Web.Models.MovementTypes.Add, User.Identity?.Name ?? "No User", new { }, new { });
        //_logger.LogAuditTrace("Log Auditoria", Web.Models.MovementTypes.Add, User.Identity?.Name ?? "No User", new { }, new { });

        ////Log Aplicativo
        //_logger.LogApplicationInformation("Log Aplicativo");


        //[TypeFilter(typeof(FilterLogger))]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogInformation("Log Operativo");
            _logger.LogAuditInformation("Log Auditoria", Web.Models.MovementTypes.Add, User.Identity?.Name ?? "No User", new { }, new { });
            _logger.LogApplicationInformation("Log Aplicativo");

            return Enumerable.Range(1, 2).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }


        //[TypeFilter(typeof(FilterLogger))]
        [HttpPost(Name = "PostException")]
        //public void GetException()
        public void PostException(int param1, string param2, bool param3, decimal param4)
        {
            throw new NotImplementedException();
        }


        //[TypeFilter(typeof(FilterLogger))]
        [HttpPut(Name = "PutInvokeService")]
        public async Task<string> PutInvokeService()
        {
            await InvokeService();
            return "ApiUno invoca a ApiDos";
        }

        private async Task InvokeService()
        {
            var httpClient = _httpClientFactory.CreateClient();
            _ = await httpClient.GetStringAsync("http://localhost:5002/WeatherForecast");
        }

    }
}
