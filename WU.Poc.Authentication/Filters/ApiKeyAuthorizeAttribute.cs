using Microsoft.AspNetCore.Mvc;

namespace WU.Poc.Authentication.Filters
{
    public class ApiKeyAuthorizeAttribute : TypeFilterAttribute
    {
        public ApiKeyAuthorizeAttribute() : base(typeof(ApiKeyAuthorizationFilter)) { }
    }
}
