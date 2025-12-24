using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services;

public interface IUserService
{
    UserModel? RegisterUser(UserRegisterModel model);
    UserModel? ValidateUser(UserLoginModel model);
    UserProfileModel? GetUserProfile(int userId);
    UserProfileModel? UpdateUserProfile(UserProfileUpdateModel model);
    List<MovieCardModel> GetFavoriteMovies(int userId);
    List<PurchasedMovieModel> GetPurchasedMovies(int userId);
}
