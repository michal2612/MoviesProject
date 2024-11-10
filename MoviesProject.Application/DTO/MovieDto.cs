namespace MoviesProject.Application.DTO;

public class MovieDto
{
    public DateTime ReleaseDate { get; set; }
    public string Overview { get; set; }
    public string Title { get; set; }
    public double Popularity { get; set; }
    public long VoteCount { get; set; }
    public double VoteAverage { get; set; }
    public string OriginalLanguage { get; set; }
    public ICollection<GenreDto> Genres { get; set; }
    public string PosterUrl { get; set; }
}
