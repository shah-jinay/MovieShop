using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(MovieShopDbContext movieShopDbContext) : base(movieShopDbContext)
    {
    }

    public User? GetUserByEmail(string email)
    {
        return _movieShopDbContext.Users
            .Include(u => u.UserRoles!)
            .ThenInclude(ur => ur.Role)
            .FirstOrDefault(u => u.Email == email);
    }

    public User? GetUserWithDetails(int userId)
    {
        return _movieShopDbContext.Users
            .Include(u => u.Favorites!)
            .ThenInclude(f => f.Movie)
            .Include(u => u.Purchases!)
            .ThenInclude(p => p.Movie)
            .FirstOrDefault(u => u.Id == userId);
    }

    public IEnumerable<Movie> GetFavoriteMovies(int userId)
    {
        return _movieShopDbContext.Favorites
            .Where(f => f.UserId == userId)
            .Include(f => f.Movie)
            .Select(f => f.Movie)
            .Where(m => m != null)
            .Select(m => m!);
    }

    public IEnumerable<Purchase> GetPurchasedMovies(int userId)
    {
        return _movieShopDbContext.Purchases
            .Where(p => p.UserId == userId)
            .Include(p => p.Movie);
    }
}
