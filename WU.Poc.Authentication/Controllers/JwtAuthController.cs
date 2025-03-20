using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WU.Poc.Authentication.Controllers
{
    [Route("api/jwt")]
    [ApiController]
    [Authorize]
    public class JwtAuthController : ControllerBase
    {
        [HttpGet(Name = "getDataWithJwt")]
        public IActionResult GetData()
        {
            return Ok(new { messsage = "Authotizacion con JWT exitoso" });
        }
    }
}
