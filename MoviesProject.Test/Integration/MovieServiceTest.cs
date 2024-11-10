using MoviesProject.Application.Services.Impl;
using MoviesProject.Infrastructure.Entities;

namespace MoviesProject.Test.Integration
{
    public class MovieServiceTest : IDisposable
    {
        private readonly TestDatabase _db;
        private readonly MovieDbContext _context;

        public MovieServiceTest()
        {
            _db = new TestDatabase();
            _context = _db.Context;
        }

        [Fact]
        public async Task LimitBelow0ThrowsException()
        {
            var movieService = new MovieService(_context);

            var msg = await Assert.ThrowsAsync<ArgumentException>
                (async() => await movieService.FindMovieByTitle("", "", -1, 1));

            Assert.NotNull(msg);
            Assert.Equal("limit must be greater than 0.", msg.Message);
        }

        [Fact]
        public async Task PageOffsetBelow0ThrowsException()
        {
            var movieService = new MovieService(_context);

            var msg = await Assert.ThrowsAsync<ArgumentException>
                (async () => await movieService.FindMovieByTitle("", "", 10, -1));

            Assert.NotNull(msg);
            Assert.Equal("pageOffset must be greater than 0.", msg.Message);
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
