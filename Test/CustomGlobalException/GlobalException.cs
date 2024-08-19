using Microsoft.AspNetCore.Diagnostics;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;

namespace Test.CustomGlobalException
{
    public class GlobalException : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is ArgumentException arg)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsync(arg.Message);
                return true;
            }

            if (exception is KeyNotFoundException knf)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsync(knf.Message);
                return true;
            }

            if (exception is ArgumentNullException ane)
            {
                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
                await httpContext.Response.WriteAsync(ane.Message);
                return true;
            }
            return false;
        }
    }
}
