namespace MoviesProject.Infrastructure.Entities;

public class Movie
{
    public int Id { get; set; }
    public DateTime ReleaseDate { get; set; }
    public required string Overview { get; set; }
    public required string Title { get; set; }
    public double Popularity { get; set; }
    public long VoteCount { get; set; }
    public double VoteAverage { get; set; }
    public required string OriginalLanguage { get; set; }
    public required string PosterUrl { get; set; }
    public ICollection<Genre> Genres { get; set; } = [];
    public ICollection<Actor> Actors { get; set; } = [];
}
