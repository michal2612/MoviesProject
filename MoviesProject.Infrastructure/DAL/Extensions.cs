using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesProject.Infrastructure.Entities;

namespace MoviesProject.Infrastructure.DAL;
public static class Extensions
{
    public static IServiceCollection AddMoviesSqlLite(this IServiceCollection services, string? ConnectionString = null)
    {
        services.AddDbContext<MovieDbContext>(options =>
            options.UseSqlite(ConnectionString ?? "Data Source=app.db"));
        services.AddHostedService<DatabaseInitializer>();
        return services;
    }
}