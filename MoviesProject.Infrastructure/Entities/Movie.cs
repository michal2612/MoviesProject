namespace MoviesProject.Infrastructure.Entities;

public class Movie
{
    public int Id { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Overview { get; set; }
    public string Title { get; set; }
    public double Popularity { get; set; }
    public long VoteCount { get; set; }
    public double VoteAverage { get; set; }
    public string OriginalLanguage { get; set; }
    public string PosterUrl { get; set; }
    public ICollection<Genre> Genres { get; set; } = [];
}
