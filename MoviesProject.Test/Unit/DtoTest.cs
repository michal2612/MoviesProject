using MoviesProject.Application.DTO;
using MoviesProject.Infrastructure.Entities;

namespace MoviesProject.Test.Unit;

public class DtoTest
{
    [Fact]
    public void MovieIsConvertedToDtoCorrectly()
    {
        // arrange
        var title = "MovieTitle";
        var overwiew = "Movie Overview";
        var lang = "jp";
        var popularity = 13.2;
        var posterUrl = "my-url.com";
        var release = new DateTime(1992, 3, 4);
        var voteAvg = 9.8;
        var voteCount = 2137;

        var movie = new Movie()
        {
            Title = title,
            Overview = overwiew,
            OriginalLanguage = lang,
            Popularity = popularity,
            PosterUrl = posterUrl,
            ReleaseDate = release,
            VoteAverage = voteAvg,
            VoteCount = voteCount
        };

        // act
        var movieDto = movie.AsDto();

        // assert
        Assert.IsType<MovieDto>(movieDto);
        Assert.Equal(movie.Title, movieDto.Title);
        Assert.Equal(movie.Overview, movieDto.Overview);
        Assert.Equal(movie.OriginalLanguage, movieDto.OriginalLanguage);
        Assert.Equal(movie.Popularity, movieDto.Popularity);
        Assert.Equal(movie.PosterUrl, movieDto.PosterUrl);
        Assert.Equal(movie.ReleaseDate, movieDto.ReleaseDate);
        Assert.Equal(movie.VoteAverage, movieDto.VoteAverage);
        Assert.Equal(movie.VoteCount, movieDto.VoteCount);
    }
}
