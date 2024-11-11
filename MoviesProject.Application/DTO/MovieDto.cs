namespace MoviesProject.Application.DTO;

public class MovieDto
{
    public DateTime ReleaseDate { get; set; }
    public string Overview { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public double Popularity { get; set; }
    public long VoteCount { get; set; }
    public double VoteAverage { get; set; }
    public string OriginalLanguage { get; set; } = string.Empty;
    public ICollection<GenreDto> Genres { get; set; } = [];
    public ICollection<ActorDto> Actors { get; set; } = [];
    public string PosterUrl { get; set; } = string.Empty;
}
