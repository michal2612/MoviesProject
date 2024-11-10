using Microsoft.AspNetCore.Mvc;
using MoviesProject.Api.Controllers;
using MoviesProject.Application.DTO;
using MoviesProject.Application.Services;

namespace MoviesProject.Test.Unit;

public class MovieControllerTest
{
    [Fact]
    public async Task GetMoviesByTitleTest()
    {
        // arrange
        var movieService = new MovieServiceTest();
        var movieController = new MovieController(movieService);

        // act
        var result = await movieController.GetMoviesByTitle("title");

        // assert
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task NoMoviesFoundTest()
    {
        // arrange
        var movieService = new MovieServiceTest();
        var movieController = new MovieController(movieService);

        // act
        var result = await movieController.GetMoviesByTitle(string.Empty);

        // assert
        Assert.IsType<NotFoundResult>(result.Result);
    }

    private class MovieServiceTest : IMovieService
    {
        public readonly IList<MovieDto> _moviesToReturns = [ new() ];

        public async Task<IEnumerable<MovieDto>> FindMovieByTitle
            (string movieTitle, string genre, SortingMethod sortingMethod, int limit, int pageOffset)
        {
            return await Task.FromResult(string.IsNullOrWhiteSpace(movieTitle) ? [] : _moviesToReturns);
        }
    }
}
