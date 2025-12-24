using System.Linq;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class MovieRepository: BaseRepository<Movie>, IMovieRepository
{
    public MovieRepository(MovieShopDbContext dbContext) : base(dbContext)
    {
        
    }
    //GetTop20GrossMovies()
    public IEnumerable<Movie> GetTop20GrossingMovies()
    {
        var movies = _movieShopDbContext.Movie.OrderByDescending(m => m.Revenue).Take(20);
        return movies;
    }

    public IEnumerable<Movie> GetMoviesByGenre(int genreId)
    {
        var movies = _movieShopDbContext.MovieGenres
            .Where(mg => mg.GenreId == genreId)
            .Select(mg => mg.Movie)
            .Where(m => m != null)
            .Select(m => m!)
            .Distinct()
            .OrderByDescending(m => m.Revenue)
            .ToList();
        return movies;
    }

    public IEnumerable<Movie> GetTopPurchasedMovies(int count)
    {
        var movies = _movieShopDbContext.Movie
            .Select(m => new { Movie = m, PurchaseCount = m.Purchases!.Count })
            .OrderByDescending(x => x.PurchaseCount)
            .ThenBy(x => x.Movie.Title)
            .Take(count)
            .Select(x => x.Movie)
            .ToList();
        return movies;
    }

    public Movie? GetMovieByIdWithDetails(int id)
    {
        return _movieShopDbContext.Movie
            .Include(m => m.Trailers)
            .Include(m => m.MovieCasts!)
                .ThenInclude(mc => mc.Cast)
            .Include(m => m.Reviews!)
                .ThenInclude(r => r.User)
            .FirstOrDefault(m => m.Id == id);
    }
}
