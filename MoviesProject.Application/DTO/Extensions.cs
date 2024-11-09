using MoviesProject.Infrastructure.Entities;

namespace MoviesProject.Application.DTO
{
    internal static class Extensions
    {
        public static GenreDto AsDto(this Genre genre)
        {
            return new()
            {
                Name = genre.Name
            };
        }

        public static MovieDto AsDto(this Movie movie)
        {
            return new()
            {
                Title = movie.Title,
                Overview = movie.Overview,
                OriginalLanguage = movie.OriginalLanguage,
                Popularity = movie.Popularity,
                ReleaseDate = movie.ReleaseDate,
                VoteAverage = movie.VoteAverage,
                Genres = movie.Genres.Select(g => g.AsDto()).ToList(),
                PosterUrl = movie.PosterUrl,
                VoteCount = movie.VoteCount,
            };
        }
    }
}
