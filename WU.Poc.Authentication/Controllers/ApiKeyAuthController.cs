using Microsoft.AspNetCore.Mvc;
using WU.Poc.Authentication.Filters;

namespace WU.Poc.Authentication.Controllers
{
    [Route("api/apikey")]
    [ApiController]
    [ApiKeyAuthorize]
    public class ApiKeyAuthController : Controller
    {
        [HttpGet(Name = "getDataWithApiKey")]
        public IActionResult GetData()
        {
            return Ok(new { messsage = "Authotizacion con API Key exitoso" });
        }
    }
}
