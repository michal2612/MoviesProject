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
        [InlineData(SortProperty.Title, SortOrder.Ascending, "My Testing Movie", "My Testing Movie Sequel")]
        [InlineData(SortProperty.Title, SortOrder.Descending, "My Testing Movie Sequel", "My Testing Movie")]
        [InlineData(SortProperty.ReleaseDate, SortOrder.Ascending, "My Testing Movie Sequel", "My Testing Movie")]
        [InlineData(SortProperty.ReleaseDate, SortOrder.Descending, "My Testing Movie", "My Testing Movie Sequel")]
        public async Task MoviesAreSorted(SortProperty sortProperty, SortOrder sortMethod, string movieTitle1, string movieTitle2)
        {
            var options = new SortingOptions(sortProperty, sortMethod);
            var movies = await _movieService.FindMovie("My Testing Movie", string.Empty, string.Empty, 2, 1, options);

            Assert.NotNull(movies);
            Assert.Equal(movies.ElementAt(0).Title, movieTitle1);
            Assert.Equal(movies.ElementAt(1).Title, movieTitle2);
        }

        [Fact]
        public async Task LimitBelow0ThrowsException()
        {
            var msg = await Assert.ThrowsAsync<ArgumentException>
                (async() => await _movieService.FindMovie("", "", "", -1, 1));

            Assert.NotNull(msg);
            Assert.Equal("limit must be greater than 0.", msg.Message);
        }

        [Fact]
        public async Task PageOffsetBelow0ThrowsException()
        {
            var msg = await Assert.ThrowsAsync<ArgumentException>
                (async () => await _movieService.FindMovie("", "", "", 10, -1));

            Assert.NotNull(msg);
            Assert.Equal("pageOffset must be greater than 0.", msg.Message);
        }

        [Fact]
        public async Task InvalidSortingMethod()
        {
            var msg = await Assert.ThrowsAsync<ArgumentException>
                (async () => await _movieService.FindMovie("", "", "", 10, 2,
                new SortingOptions((SortProperty)99, SortOrder.Ascending)));

            Assert.NotNull(msg);
            Assert.Equal("Not valid: SortProperty: 99. SortOrder: Ascending.", msg.Message);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
