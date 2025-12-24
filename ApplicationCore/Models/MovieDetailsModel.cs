namespace ApplicationCore.Models;

public class MovieDetailsModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string PosterUrl { get; set; }
    public string? Overview { get; set; }
    public string? TagLine { get; set; }
    public decimal Budget { get; set; }
    public decimal Revenue { get; set; }
    public decimal? AverageRating { get; set; }
    public int? Runtime { get; set; }
    public string? OriginalLanguage { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public decimal? Price { get; set; }
    public List<TrailerModel> Trailers { get; set; } = new();
    public List<CastModel> Casts { get; set; } = new();
    public List<ReviewModel> Reviews { get; set; } = new();
}
