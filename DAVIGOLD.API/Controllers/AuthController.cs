using DAVIGOLD.API.Model;
using DAVIGOLD.API.Repository;
using DAVIGOLD.API.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace DAVIGOLD.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly IOptions<AppSettingModel> _appSettings;
        //public AuthController(IOptions<AppSettingModel> appSettings)
        //{
        //   _appSettings = appSettings;
        //}
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public  IActionResult Index([FromBody] AuthModel auth, [FromServices] IOptions<AppSettingModel> appSettings)
        {
            var response =  DbClientFactory<AuthDbClient>.Instance.AuthenticateUser(appSettings, auth);
           return Ok(response);
        }
    }
}
