using System.Security.Cryptography;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    private const int SaltSize = 16;
    private const int KeySize = 32;
    private const int Iterations = 10000;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public UserModel? RegisterUser(UserRegisterModel model)
    {
        var existingUser = _userRepository.GetUserByEmail(model.Email.Trim());
        if (existingUser != null)
        {
            return null;
        }

        var user = new User
        {
            Email = model.Email.Trim(),
            FirstName = model.FirstName.Trim(),
            LastName = model.LastName.Trim(),
            PhoneNumber = model.PhoneNumber.Trim(),
            DateOfBirth = model.DateOfBirth,
            isLocked = "false"
        };

        var (salt, hash) = HashPassword(model.Password);
        user.Salt = salt;
        user.HashesPassword = hash;

        var createdUser = _userRepository.Insert(user);
        return MapToModel(createdUser);
    }

    public UserModel? ValidateUser(UserLoginModel model)
    {
        var user = _userRepository.GetUserByEmail(model.Email.Trim());
        if (user == null)
        {
            return null;
        }

        if (!VerifyPassword(model.Password, user.Salt, user.HashesPassword))
        {
            return null;
        }

        return MapToModel(user);
    }

    public UserProfileModel? GetUserProfile(int userId)
    {
        var user = _userRepository.GetUserWithDetails(userId);
        if (user == null)
        {
            return null;
        }

        return new UserProfileModel
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber,
            DateOfBirth = user.DateOfBirth
        };
    }

    public UserProfileModel? UpdateUserProfile(UserProfileUpdateModel model)
    {
        var user = _userRepository.GetById(model.Id);
        if (user == null)
        {
            return null;
        }

        user.FirstName = model.FirstName.Trim();
        user.LastName = model.LastName.Trim();
        user.PhoneNumber = model.PhoneNumber.Trim();
        user.DateOfBirth = model.DateOfBirth;

        _userRepository.Update(user);
        return GetUserProfile(user.Id);
    }

    public List<MovieCardModel> GetFavoriteMovies(int userId)
    {
        var movies = _userRepository.GetFavoriteMovies(userId);
        return movies.Select(MapToCard).ToList();
    }

    public List<PurchasedMovieModel> GetPurchasedMovies(int userId)
    {
        var purchases = _userRepository.GetPurchasedMovies(userId);
        var result = new List<PurchasedMovieModel>();
        foreach (var purchase in purchases)
        {
            if (purchase.Movie == null) continue;
            result.Add(new PurchasedMovieModel
            {
                Movie = MapToCard(purchase.Movie),
                PurchaseDateTime = purchase.PurchaseDateTime,
                TotalPrice = purchase.TotalPrice,
                PurchaseNumber = purchase.PurchaseNumber
            });
        }

        return result;
    }

    private static UserModel MapToModel(User user)
    {
        var isAdmin = user.UserRoles?.Any(ur => ur.Role.Name.Equals("Admin", StringComparison.OrdinalIgnoreCase)) ?? false;
        return new UserModel
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            IsAdmin = isAdmin
        };
    }

    private static MovieCardModel MapToCard(Movie movie)
    {
        return new MovieCardModel
        {
            Id = movie.Id,
            Title = movie.Title ?? string.Empty,
            PosterURL = movie.PosterUrl ?? string.Empty
        };
    }

    private static (string Salt, string Hash) HashPassword(string password)
    {
        using var rng = RandomNumberGenerator.Create();
        var saltBytes = new byte[SaltSize];
        rng.GetBytes(saltBytes);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
        var key = pbkdf2.GetBytes(KeySize);
        return (Convert.ToBase64String(saltBytes), Convert.ToBase64String(key));
    }

    private static bool VerifyPassword(string password, string? salt, string? storedHash)
    {
        if (string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(storedHash))
        {
            return false;
        }

        var saltBytes = Convert.FromBase64String(salt);
        using var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, Iterations, HashAlgorithmName.SHA256);
        var key = pbkdf2.GetBytes(KeySize);
        var storedHashBytes = Convert.FromBase64String(storedHash);
        return CryptographicOperations.FixedTimeEquals(key, storedHashBytes);
    }
}
