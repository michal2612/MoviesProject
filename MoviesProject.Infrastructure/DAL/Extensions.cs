using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MoviesProject.Infrastructure.Entities;

namespace MoviesProject.Infrastructure.DAL;
public static class Extensions
{
    public static IServiceCollection AddMoviesSqlLite(this IServiceCollection services)
    {
        services.AddDbContext<MovieDbContext>(options =>
            options.UseSqlite("Data Source=MovieDatabase.db"));
        services.AddHostedService<DatabaseInitializer>();
        return services;
    }
}