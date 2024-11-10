using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace MoviesProject.Infrastructure.Exceptions;

public static class Extensions
{
    public static IServiceCollection AddExceptionMiddleware(this IServiceCollection services)
    {
        services.AddSingleton<ExceptionMiddleware>();
        return services;
    }

    public static WebApplication UseExceptionMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        return app;
    }
}
