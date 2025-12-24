using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IMovieService
{
    List<MovieCardModel> GetTop20GrossingMovies();
    List<MovieCardModel> GetMoviesByGenre(int genreId);
    List<MovieCardModel> GetTopPurchasedMovies(int count);
    List<GenreModel> GetAllGenres();
    MovieDetailsModel GetMovieDetails(int id);
    bool DeleteMovie(int id);
}
