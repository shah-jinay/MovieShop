using ApplicationCore.Models;

namespace MovieShopMVC.Models;

public class GenreMoviesViewModel
{
    public int? SelectedGenreId { get; set; }
    public List<GenreModel> Genres { get; set; } = new();
    public List<MovieCardModel> Movies { get; set; } = new();
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
}
