using MoviesProject.Application.Services;
using MoviesProject.Application.Services.Impl;
using MoviesProject.Infrastructure.Entities;

namespace MoviesProject.Test.Integration
{
    public class MovieServiceTest : IDisposable
    {
        private readonly TestDatabase _db;
        private readonly MovieDbContext _context;
        private readonly IMovieService _movieService;

        public MovieServiceTest()
        {
            _db = new TestDatabase();
            _context = _db.Context;
            _movieService = new MovieService(_context);
        }

        [Theory]
        [InlineData(SortingMethod.TitleAsceding, "My Testing Movie", "My Testing Movie Sequel")]
        [InlineData(SortingMethod.TitleDescending, "My Testing Movie Sequel", "My Testing Movie")]
        [InlineData(SortingMethod.ReleaseDateAscending, "My Testing Movie Sequel", "My Testing Movie")]
        [InlineData(SortingMethod.ReleaseDateDescending, "My Testing Movie", "My Testing Movie Sequel")]
        public async Task MoviesAreSorted(SortingMethod method, string movieTitle1, string movieTitle2)
        {
            var movies = await _movieService.FindMovie("My Testing Movie", string.Empty, string.Empty, method, 2, 1);

            Assert.NotNull(movies);
            Assert.Equal(movies.ElementAt(0).Title, movieTitle1);
            Assert.Equal(movies.ElementAt(1).Title, movieTitle2);
        }

        [Fact]
        public async Task LimitBelow0ThrowsException()
        {
            var msg = await Assert.ThrowsAsync<ArgumentException>
                (async() => await _movieService.FindMovie("", "", "", 0, -1, 1));

            Assert.NotNull(msg);
            Assert.Equal("limit must be greater than 0.", msg.Message);
        }

        [Fact]
        public async Task PageOffsetBelow0ThrowsException()
        {
            var msg = await Assert.ThrowsAsync<ArgumentException>
                (async () => await _movieService.FindMovie("", "", "", 0, 10, -1));

            Assert.NotNull(msg);
            Assert.Equal("pageOffset must be greater than 0.", msg.Message);
        }

        [Fact]
        public async Task InvalidSortingMethod()
        {
            var msg = await Assert.ThrowsAsync<ArgumentException>
                (async () => await _movieService.FindMovie("", "", "", (SortingMethod)99, 10, 2));

            Assert.NotNull(msg);
            Assert.Equal("Not valid SortingMethod.", msg.Message);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
