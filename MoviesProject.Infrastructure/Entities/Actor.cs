using System.ComponentModel.DataAnnotations;

namespace MoviesProject.Infrastructure.Entities;

public class Actor
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Movie> Movies { get; set; } = [];
}
