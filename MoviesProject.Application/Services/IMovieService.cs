using MoviesProject.Application.DTO;

namespace MoviesProject.Application.Services;

/// <summary>
/// 
/// </summary>
public interface IMovieService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="movieTitle"></param>
    /// <param name="genre"></param>
    /// <param name="limit"></param>
    /// <param name="pageOffset"></param>
    /// <returns></returns>
    public Task<IEnumerable<MovieDto>> FindMovie(string movieTitle, string genre, string actor,
        SortingMethod sortingMethod, int limit, int pageOffset);
}
