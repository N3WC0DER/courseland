using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AgroLink.Server.Filters
{
    public class BaseExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<BaseExceptionFilterAttribute> _logger;

        public BaseExceptionFilterAttribute(ILogger<BaseExceptionFilterAttribute> logger) { _logger = logger; }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "Exception: {Message}", context.Exception.Message);

            var errorResponse = new
            {
                Error = "Internal Server Error"
            };
            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };

            context.ExceptionHandled = true;
        }
    }
}
