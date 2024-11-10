using MoviesProject.Application.DTO;
using System.Net.Http.Json;

namespace MoviesProject.Test.Integration;

public class MovieControllerTest
{
    private readonly MyMovieProjectApp _app;
    private readonly HttpClient _client;
    public MovieControllerTest()
    {
        _app = new MyMovieProjectApp();
        _client = _app.Client;
    }

    [Fact]
    public async Task MoviesCanBeQueried()
    {
        var response = await _client.GetAsync("/movie?movieTitle=My");

        Assert.True(response.IsSuccessStatusCode);
        Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<IEnumerable<MovieDto>>();

        Assert.NotNull(content);
        Assert.Equal(2, content.Count());

        var movie = content.First();
        Assert.Equal("My Testing Movie", movie.Title);
        Assert.Equal("Great movie!", movie.Overview);
        Assert.Equal("en", movie.OriginalLanguage);
        Assert.Equal(1423.13, movie.Popularity);
        Assert.Equal("http:\\\\poster-url.org", movie.PosterUrl);
        Assert.Equal(new DateTime(2024, 12, 31), movie.ReleaseDate);
        Assert.Equal(9.99, movie.VoteAverage);
        Assert.Equal(1482_2313, movie.VoteCount);
        Assert.Single(movie.Genres);
        Assert.Equal("Action", movie.Genres.First().Name);

        movie = content.Last();
        Assert.Equal("My Testing Movie 2", movie.Title);
        Assert.Equal("Great movie!", movie.Overview);
        Assert.Equal("en", movie.OriginalLanguage);
        Assert.Equal(1423.13, movie.Popularity);
        Assert.Equal("http:\\\\poster-url.org", movie.PosterUrl);
        Assert.Equal(new DateTime(2024, 12, 31), movie.ReleaseDate);
        Assert.Equal(9.99, movie.VoteAverage);
        Assert.Equal(1482_2313, movie.VoteCount);

        Assert.Single(movie.Genres);
        Assert.Equal("Fantasy", movie.Genres.First().Name);
    }

    [Fact]
    public async Task FilteringByGenre()
    {
        var response = await _client.GetAsync("/movie?movieTitle=My&genre=Action");

        Assert.True(response.IsSuccessStatusCode);
        Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);

        var content = await response.Content.ReadFromJsonAsync<IEnumerable<MovieDto>>();

        Assert.NotNull(content);
        Assert.Single(content);

        var movie = content.First();
        Assert.True(movie.Genres.First().Name == "Action");
    }

    [Fact]
    public async Task MovieCannotBeFound()
    {
        var response = await _client.GetAsync("/movie?movieTitle=InvalidTitle");

        Assert.True(!response.IsSuccessStatusCode);
        Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task InvalidGenre()
    {
        var response = await _client.GetAsync("/movie?movieTitle=My&genre=InvalidGenre");

        Assert.True(!response.IsSuccessStatusCode);
        Assert.True(response.StatusCode == System.Net.HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task InvalidLimit()
    {
        var response = await _client.GetAsync("/movie?movieTitle=My&limit=0");

        Assert.True(!response.IsSuccessStatusCode);
        Assert.True(response.StatusCode == System.Net.HttpStatusCode.InternalServerError);
    }

    [Fact]
    public async Task InvalidPageOffset()
    {
        var response = await _client.GetAsync("/movie?movieTitle=My&pageOffset=0");

        Assert.True(!response.IsSuccessStatusCode);
        Assert.True(response.StatusCode == System.Net.HttpStatusCode.InternalServerError);
    }
}
