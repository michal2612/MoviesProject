using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using MoviesProject.Infrastructure.Entities;

namespace MoviesProject.Test.Integration
{
    internal class TestDatabase : IDisposable
    {
        private readonly SqliteConnection _connection;

        public string ConnectionString { get; } = "DataSource=:memory:";
        public MovieDbContext Context { get; set; }

        public TestDatabase()
        {
            _connection = new SqliteConnection(ConnectionString);
            _connection.Open();

            var options = new DbContextOptionsBuilder<MovieDbContext>()
                .UseSqlite(_connection)
                .Options;

            Context = new MovieDbContext(options);
            Context.Database.EnsureCreated();

            AddMoviesToDb();
        }

        private void AddMoviesToDb()
        {
            Context.Movies.AddRange(new Movie()
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
            },
            new Movie()
            {
                Title = "My Testing Movie 2",
                Overview = "Great movie!",
                OriginalLanguage = "en",
                Popularity = 1423.13,
                VoteCount = 1482_2313,
                VoteAverage = 9.99,
                ReleaseDate = new DateTime(2024, 12, 31),
                PosterUrl = "http:\\\\poster-url.org",
                Genres = [new() { Name = "Fantasy" }]
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
