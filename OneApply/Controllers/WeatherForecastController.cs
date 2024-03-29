using BusinessLogicLayer.Roles;
using DTOLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OneApply.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        [Route("Get")]
        [Authorize(Roles = StaticUserRoles.EMPLOYER)]
        public IActionResult Get()
        {
            return Ok(Summaries);
        }

        [HttpGet]
        [Route("GetWorkerRole")]
        [Authorize(Roles = StaticUserRoles.WORKER)]
        public IActionResult GetWorkerRole()
        {
            return Ok(Summaries);
        }

        [HttpGet]
        [Route("GetEmployerRole")]
        [Authorize(Roles = StaticUserRoles.EMPLOYER)]
        public IActionResult GetEmployerRole()
        {
            return Ok(Summaries);
        }

        [HttpGet]
        [Route("GetAdminRole")]
        [Authorize(Roles = StaticUserRoles.ADMIN)]
        public IActionResult GetAdminRole()
        {
            return Ok(Summaries);
        }
    }
}

