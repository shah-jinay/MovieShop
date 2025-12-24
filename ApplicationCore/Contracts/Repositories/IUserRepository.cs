using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IUserRepository : IRepository<User>
{
    User? GetUserByEmail(string email);
    User? GetUserWithDetails(int userId);
    IEnumerable<Movie> GetFavoriteMovies(int userId);
    IEnumerable<Purchase> GetPurchasedMovies(int userId);
}
