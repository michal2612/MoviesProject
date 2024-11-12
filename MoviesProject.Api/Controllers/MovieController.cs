using Microsoft.AspNetCore.Mvc;
using MoviesProject.Application.DTO;
using MoviesProject.Application.Services;

namespace MoviesProject.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<MovieDto>>>
        GetMoviesByTitle(string movieTitle,
        string genre = "",
        string actor = "",
        int limit = 10,
        int pageOffset = 1,
        SortProperty sortProperty = SortProperty.None,
        SortOrder sortOrder = SortOrder.None)
    {
        using var cancellationTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(2));
        var token = cancellationTokenSource.Token;

        var movies = await _movieService
            .FindMovie(movieTitle, genre, actor, limit, pageOffset, new(sortProperty, sortOrder), token);

        if (movies == null || !movies.Any())
        {
            return NotFound();
        }
        return Ok(movies);
    }
}
