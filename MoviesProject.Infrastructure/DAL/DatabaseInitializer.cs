using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoviesProject.Infrastructure.Entities;
using System.Globalization;

namespace MoviesProject.Infrastructure.DAL;

internal sealed class DatabaseInitializer : IHostedService
{
    private readonly IServiceProvider _serviceProvider;

    public DatabaseInitializer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    private record MovieCSV(DateTime Release_Date, string Title, string Overview,
            double Popularity, long Vote_Count, double Vote_Average,
            string Original_Language, string Genre, string Poster_Url);

    public async Task StartAsync(CancellationToken _)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<MovieDbContext>();

        if (await dbContext.Movies.AnyAsync())
            return;

        using var reader = new StreamReader("mymoviedb.csv");
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        csv.Read();
        csv.ReadHeader();

        // there are about 10_000 entries in .csv file. Add records in batches.
        var batchSize = 500;
        var movies = new List<Movie>();
        var existingGenres = await dbContext.Genres.ToDictionaryAsync(g => g.Name.ToLower());
        var actors = GetActors();

        while (csv.Read())
        {
            try
            {
                var csvRecord = csv.GetRecord<MovieCSV>();

                // Resolve genres
                var genres = new List<Genre>();
                foreach (var genreName in csvRecord.Genre.Split(','))
                {
                    var trimmedGenreName = genreName.Trim().ToLower();

                    if (existingGenres.TryGetValue(trimmedGenreName, out var existingGenre))
                    {
                        // Reuse the existing genre if it already exists
                        genres.Add(existingGenre);
                    }
                    else
                    {
                        // Create a new genre and add it to the dictionary for future reuse
                        var newGenre = new Genre { Name = genreName.Trim() };
                        genres.Add(newGenre);
                        existingGenres[trimmedGenreName] = newGenre;
                    }
                }

                // Add movie
                movies.Add(new()
                {
                    ReleaseDate = csvRecord.Release_Date.ToUniversalTime(),
                    Title = csvRecord.Title,
                    Overview = csvRecord.Overview,
                    Popularity = csvRecord.Popularity,
                    VoteCount = csvRecord.Vote_Count,
                    VoteAverage = csvRecord.Vote_Average,
                    OriginalLanguage = csvRecord.Original_Language,
                    Genres = genres,
                    // basic non over engineered way to select different actors
                    Actors = actors.Skip(movies.Count % 2 == 0 ? 3 : 0).Take(3).ToList(),
                    PosterUrl = csvRecord.Poster_Url
                });

                if (movies.Count == batchSize)
                {
                    await dbContext.Movies.AddRangeAsync(movies);
                    await dbContext.SaveChangesAsync();
                    movies.Clear();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // The last batch can be >batchSize
        if (movies.Count != 0)
        {
            await dbContext.Movies.AddRangeAsync(movies);
            await dbContext.SaveChangesAsync();
        }
    }

    public async Task StopAsync(CancellationToken _)
    {
        await Task.FromResult(0);
    }

    private ICollection<Actor> GetActors()
    {
        var actors = new string[]
        { "Tom Hanks", "Tom Hardy", "Marion Cotillard",
            "Robin Wright", "Matt Damon", "Michael Caine" };

        return actors.Select(a => new Actor() { Name = a}).ToList();
    }
}
