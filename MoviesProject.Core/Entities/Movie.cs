using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesProject.Core.Entities
{
    public class Movie
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Overview { get; set; }
        public string Title { get; set; }
        public double Popularity { get; set; }
        public long VoteCount { get; set; }
        public double VoteAverage { get; set; }
        public string OriginalLanguage { get; set; }
        public ICollection<Genre> Genre { get; set; }
        public string PosterUrl { get; set; }
    }
}
