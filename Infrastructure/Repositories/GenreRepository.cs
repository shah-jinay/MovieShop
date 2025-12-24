using System.Linq;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;

namespace Infrastructure.Repositories;

public class GenreRepository : BaseRepository<Genre>, IGenreRepository
{
    public GenreRepository(MovieShopDbContext movieShopDbContext) : base(movieShopDbContext)
    {
    }

    public IEnumerable<Genre> GetAllGenres()
    {
        return _movieShopDbContext.Genres
            .OrderBy(g => g.Name)
            .ToList();
    }
}
