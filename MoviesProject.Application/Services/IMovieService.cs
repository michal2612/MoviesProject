using MoviesProject.Application.DTO;

namespace MoviesProject.Application.Services;

/// <summary>
/// Defines the contract for movie-related operations.
/// </summary>
public interface IMovieService
{
    /// <summary>
    /// Searches for movies based on title, genre, and actor with specified sorting, limit, and pagination options.
    /// </summary>
    /// <param name="movieTitle">The title or partial title of the movie to search for.</param>
    /// <param name="genre">The genre to filter movies by.</param>
    /// <param name="actor">The actor to filter movies by.</param>
    /// <param name="limit">The maximum number of results to return.</param>
    /// <param name="pageOffset">The offset for pagination, indicating the starting point for results.</param>
    /// <param name="sortingOptions">Sorting options.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of movies matching the search criteria.</returns>
    public Task<IEnumerable<MovieDto>> FindMovie(string movieTitle,
        string genre,
        string actor,
        int limit,
        int pageOffset,
        SortingOptions? sortingOptions = null,
        CancellationToken? cancellationToken = null);
}
