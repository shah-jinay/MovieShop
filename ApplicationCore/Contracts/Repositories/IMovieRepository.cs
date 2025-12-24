using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IMovieRepository: IRepository<Movie>
{
    IEnumerable<Movie> GetTop20GrossingMovies();
    IEnumerable<Movie> GetMoviesByGenre(int genreId);
    IEnumerable<Movie> GetTopPurchasedMovies(int count);
    Movie? GetMovieByIdWithDetails(int id);
}
