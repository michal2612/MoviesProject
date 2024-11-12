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

    public async Task<IEnumerable<MovieDto>> FindMovie(string movieTitle, string genre, string actor,
        int limit, int pageOffset, SortingOptions? sortingOptions, CancellationToken? token = null)
    {
        if (limit <= 0)
            throw new ArgumentException($"{nameof(limit)} must be greater than 0.");

        if (pageOffset <= 0)
            throw new ArgumentException($"{nameof(pageOffset)} must be greater than 0.");

        var query = _movieDbContext.Movies.AsQueryable();

        query = query
            .Where(m => m.Title.Contains(movieTitle))
            .Include(m => m.Genres)
            .Include(m => m.Actors);

        if (!query.Any()) return [];

        if (!string.IsNullOrWhiteSpace(genre))
        {
            query = query.Where(m => m.Genres.Any(g => g.Name == genre));
        }

        if (!string.IsNullOrWhiteSpace(actor))
        {
            query = query.Where(m => m.Actors.Any(g => g.Name == actor));
        }

        query = query.Skip((pageOffset - 1) * limit).Take(limit);

        if (sortingOptions != null && sortingOptions.SortOrder != SortOrder.None)
        {
            if (sortingOptions.SortOrder == SortOrder.None)
                throw new ArgumentException($"{nameof(SortOrder)} is not defined.");

            query = (sortingOptions.SortProperty, sortingOptions.SortOrder) switch
            {
                (SortProperty.Title, SortOrder.Ascending) => query.OrderBy(m => m.Title),
                (SortProperty.Title, SortOrder.Descending) => query.OrderByDescending(m => m.Title),
                (SortProperty.ReleaseDate, SortOrder.Ascending) => query.OrderBy(m => m.ReleaseDate),
                (SortProperty.ReleaseDate, SortOrder.Descending) => query.OrderByDescending(m => m.ReleaseDate),
                _ => throw new ArgumentException($"Not valid: {sortingOptions}")
            };
        }

        return await query.Select(m => m.AsDto()).ToListAsync();
    }
}
