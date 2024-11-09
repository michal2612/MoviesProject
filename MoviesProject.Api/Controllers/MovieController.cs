using Microsoft.AspNetCore.Mvc;
using MoviesProject.Application.DTO;
using MoviesProject.Application.Services;

namespace MoviesProject.Api.Controllers
{
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
            GetMoviesByTitle(string movieTitle, int limit = 10, int pageOffset = 1)
        {
            var movies = await _movieService.FindMovieByTitle(movieTitle, limit, pageOffset);

            if (movies == null || movies.Count() == 0)
            {
                return NotFound();
            }
            return Ok(movies);
        }
    }
}
