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
			catch (Exception e)
			{
                Error error = e switch
                {
                    ArgumentException => new(HttpStatusCode.NotFound, e.GetType().Name, e.Message),
                    _ => new(HttpStatusCode.InternalServerError, e.GetType().Name, e.Message),
                };
                context.Response.StatusCode = (int)error.StatusCode;
                await context.Response.WriteAsync(error.ToString());
            }
        }

        private record Error(HttpStatusCode StatusCode, string ExceptionType, string Reason);
    }
}
