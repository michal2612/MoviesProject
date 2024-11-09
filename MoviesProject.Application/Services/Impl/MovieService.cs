using Microsoft.EntityFrameworkCore;
using MoviesProject.Application.DTO;
using MoviesProject.Infrastructure.Entities;

namespace MoviesProject.Application.Services.Impl
{
    public sealed class MovieService : IMovieService
    {
        private readonly MovieDbContext _movieDbContext;

        public MovieService(MovieDbContext movieDbContext)
        {
            _movieDbContext = movieDbContext;
        }

        public async Task<IEnumerable<MovieDto>> FindMovieByTitle(string movieTitle, int limit, int pageOffset)
        {
            var query = _movieDbContext.Movies.AsQueryable();

            query = query.Where(m => m.Title.Contains(movieTitle));

            query = query.Include(m => m.Genres);

            var items = await query
                .Skip((pageOffset - 1) * pageOffset)
                .Take(limit)
                .Select(m => m.AsDto())
                .ToListAsync();

            return items;
        }
    }
}
