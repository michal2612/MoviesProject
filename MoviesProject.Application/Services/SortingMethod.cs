namespace MoviesProject.Application.Services;

public record SortingOptions(SortProperty SortProperty, SortOrder SortOrder)
{
    public override string ToString()
        => $"SortProperty: {SortProperty}. SortOrder: {SortOrder}.";
}

public enum SortProperty
{
    None,
    Title,
    ReleaseDate
}

public enum SortOrder
{
    None,
    Ascending,
    Descending
}