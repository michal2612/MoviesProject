using Microsoft.AspNetCore.Http;
using System.Net;

namespace MoviesProject.Infrastructure.Exceptions
{
    internal sealed class ExceptionMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
			try
			{
                await next(context);
			}
            catch (OperationCanceledException)
            {
                var statusCode = HttpStatusCode.RequestTimeout;
                context.Response.StatusCode = (int)statusCode;
                await context.Response.WriteAsync
                    (new Error(statusCode, typeof(OperationCanceledException).Name, "The request timed out.").ToString());
            }
			catch (Exception e)
			{
                Error error = e switch
                {
                    _ => new(HttpStatusCode.InternalServerError, e.GetType().Name, e.Message),
                };
                context.Response.StatusCode = (int)error.StatusCode;
                await context.Response.WriteAsync(error.ToString());
            }
        }

        private record Error(HttpStatusCode StatusCode, string ExceptionType, string Reason);
    }
}
