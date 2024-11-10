using Microsoft.EntityFrameworkCore;
using MoviesProject.Application.DTO;
using MoviesProject.Infrastructure.Entities;

namespace MoviesProject.Application.Services.Impl;

public sealed class MovieService : IMovieService
{
    private readonly MovieDbContext _movieDbContext;

    public MovieService(MovieDbContext movieDbContext)
    {
        _movieDbContext = movieDbContext;
    }

    public async Task<IEnumerable<MovieDto>> FindMovieByTitle(string movieTitle, string genre, int limit, int pageOffset)
    {
        if (limit <= 0)
            throw new ArgumentException($"{nameof(limit)} must be greater than 0.");

        if (pageOffset <= 0)
            throw new ArgumentException($"{nameof(pageOffset)} must be greater than 0.");

        var query = _movieDbContext.Movies.AsQueryable();

        query = query
            .Where(m => m.Title.Contains(movieTitle))
            .Include(m => m.Genres);

        if (!string.IsNullOrWhiteSpace(genre))
        {
            query = query.Where(m => m.Genres.Any(g => g.Name == genre));
        }

        var items = await query
            .Skip((pageOffset - 1) * pageOffset)
            .Take(limit)
            .Select(m => m.AsDto())
            .ToListAsync();

        return items;
    }
}
