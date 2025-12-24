using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services;

public class MovieService: IMovieService
{
    private readonly IMovieRepository _movieRepository;
    private readonly IGenreRepository _genreRepository;
    
    public MovieService(IMovieRepository movieRepository, IGenreRepository genreRepository)
    {
        _movieRepository = movieRepository;
        _genreRepository = genreRepository;
    }
    public List<MovieCardModel> GetTop20GrossingMovies()
    {
        var movies = _movieRepository.GetTop20GrossingMovies();
        var moveCardModels = new List<MovieCardModel>();
        foreach (var movie in movies)
        {
            moveCardModels.Add(new MovieCardModel()
            {
                Id = movie.Id, PosterURL = movie.PosterUrl, Title = movie.Title
            });
        }
        return moveCardModels;
    }

    public List<MovieCardModel> GetMoviesByGenre(int genreId)
    {
        var movies = _movieRepository.GetMoviesByGenre(genreId);
        var moveCardModels = new List<MovieCardModel>();
        foreach (var movie in movies)
        {
            moveCardModels.Add(new MovieCardModel()
            {
                Id = movie.Id, PosterURL = movie.PosterUrl, Title = movie.Title
            });
        }

        return moveCardModels;
    }

    public List<MovieCardModel> GetTopPurchasedMovies(int count)
    {
        var movies = _movieRepository.GetTopPurchasedMovies(count);
        var moveCardModels = new List<MovieCardModel>();
        foreach (var movie in movies)
        {
            moveCardModels.Add(new MovieCardModel()
            {
                Id = movie.Id, PosterURL = movie.PosterUrl, Title = movie.Title
            });
        }
        return moveCardModels;
    }

    public List<GenreModel> GetAllGenres()
    {
        var genres = _genreRepository.GetAllGenres();
        var genreModels = new List<GenreModel>();
        foreach (var genre in genres)
        {
            genreModels.Add(new GenreModel
            {
                Id = genre.Id,
                Name = genre.Name ?? string.Empty
            });
        }

        return genreModels;
    }

    public MovieDetailsModel GetMovieDetails(int id)
    {
        var movie = _movieRepository.GetMovieByIdWithDetails(id);
        if (movie != null)
        {
            var moviesDetailModel = new MovieDetailsModel()
            {
                Id = movie.Id,
                PosterUrl = movie.PosterUrl,
                Title = movie.Title,
                Budget = movie.Budget,
                Overview = movie.Overview,
                TagLine = movie.TagLine,
                Revenue = movie.Revenue,
                Runtime = movie.Runtime,
                OriginalLanguage = movie.OriginalLanguage,
                ReleaseDate = movie.ReleaseDate,
                Price = movie.Price,
                Trailers = movie.Trailers?.Select(t => new TrailerModel
                {
                    Id = t.Id,
                    Name = t.Name,
                    TrailerUrl = t.TrailerUrl
                }).ToList() ?? new List<TrailerModel>(),
                Casts = movie.MovieCasts?.Select(mc => new CastModel
                {
                    Id = mc.CastId,
                    Name = mc.Cast?.Name ?? "Unknown",
                    Character = mc.Character,
                    ProfilePath = mc.Cast?.ProfilePath
                }).ToList() ?? new List<CastModel>(),
                Reviews = movie.Reviews?.Select(r => new ReviewModel
                {
                    Rating = r.Rating,
                    ReviewText = r.ReviewText,
                    CreatedDate = r.CreatedDate,
                    UserName = string.IsNullOrWhiteSpace(r.User?.FirstName) && string.IsNullOrWhiteSpace(r.User?.LastName)
                        ? "MovieShop User"
                        : $"{r.User?.FirstName} {r.User?.LastName}".Trim()
                }).ToList() ?? new List<ReviewModel>()
            };

            if (moviesDetailModel.Reviews.Any())
            {
                moviesDetailModel.AverageRating = Math.Round(moviesDetailModel.Reviews.Average(r => r.Rating), 1);
            }

            return moviesDetailModel;
        }

        return null;
    }

    public bool DeleteMovie(int id)
    {
        var movie = _movieRepository.DeleteById(id);
        if (movie == null)
        {
            return false;
        }
        return true;
    }
}
