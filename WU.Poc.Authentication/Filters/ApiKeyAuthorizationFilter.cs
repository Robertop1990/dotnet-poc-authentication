using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using WU.Poc.Authentication.Infrastructure;

namespace WU.Poc.Authentication.Filters
{
    public class ApiKeyAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly WUDbContext _dbContext;

        public ApiKeyAuthorizationFilter(WUDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;

            if (!request.Headers.TryGetValue("x-api-key", out var apiKeyValue))
            {
                context.Result = new UnauthorizedObjectResult("API Key es requerida.");
                return;
            }

            var apiKey = await _dbContext.ApiKeys
                .FirstOrDefaultAsync(k => k.Key == apiKeyValue);

            if (apiKey is null)
            {
                context.Result = new UnauthorizedObjectResult("API key no encontrada.");
                return;
            }

            if (!apiKey.IsActive)
            {
                context.Result = new UnauthorizedObjectResult("API key se encuentra inactiva.");
                return;
            }
        }
    }
}
