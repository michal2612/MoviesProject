using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MoviesProject.Infrastructure.Entities;

namespace MoviesProject.Test.Integration
{
    internal class TestDatabase : IDisposable
    {
        private readonly SqliteConnection _connection;

        public MovieDbContext Context { get; set; }

        public TestDatabase()
        {
            _connection = new SqliteConnection("DataSource=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseSqlite(_connection)
                .Options;

            Context = new MovieDbContext(options);
            Context.Database.EnsureCreated();

            Context.Movies.Add(new Movie()
            { 
                Title = "My Testing Movie",
                Overview = "Great movie!",
                OriginalLanguage = "en",
                Popularity = 1423.13,
                VoteCount = 1482_2313,
                VoteAverage = 9.99,
                ReleaseDate = new DateTime(2024, 12, 31),
                PosterUrl = "http:\\\\poster-url.org",
                Genres = [new() { Name = "Action" }]
            });
            Context.SaveChanges();

        }

        public void Dispose()
        {
            Context.Dispose();
            _connection.Close();
        }
    }
}
