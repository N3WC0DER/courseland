using AgroLink.Server.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace courseland.Server.Filters
{
    public class NotFoundExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            var errorResponse = new
            {
                Error = context.Exception.Message,
            };
            context.Result = new ObjectResult(errorResponse)
            {
                StatusCode = StatusCodes.Status404NotFound
            };

            context.ExceptionHandled = true;
        }
    }
}
