using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace TaskApi.Filters
{
    public class ApiKeyValidationAttribute : ActionFilterAttribute
    {
        private readonly string _headerName = "X-Api-Key";

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(_headerName, out Microsoft.Extensions.Primitives.StringValues headerValue))
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    message = $"{_headerName} header needed"
                });
                return;
            }

            IConfiguration? configuration = context.HttpContext.RequestServices
                .GetService(typeof(IConfiguration)) as IConfiguration;

            if (string.IsNullOrEmpty(headerValue) || !ValidarHeader(headerValue, configuration))
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    message = $"Invalid {_headerName}"
                });
                return;
            }

            base.OnActionExecuting(context);
        }

        private bool ValidarHeader(string headerValue, IConfiguration configuration)
        {
            string? apiKey = configuration["ApiKey"];

            if (string.IsNullOrEmpty(apiKey))
            {
                return false;
            }

            return headerValue == apiKey;
        }
    }
}