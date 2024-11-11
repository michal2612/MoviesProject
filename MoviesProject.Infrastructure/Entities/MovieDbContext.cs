using Microsoft.EntityFrameworkCore;

namespace MoviesProject.Infrastructure.Entities;

public class MovieDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Actor> Actors { get; set; }

    public MovieDbContext(DbContextOptions<MovieDbContext> options) : base(options)
    {
    }
}
