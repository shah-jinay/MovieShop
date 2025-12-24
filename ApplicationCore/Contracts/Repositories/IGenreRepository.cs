using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories;

public interface IGenreRepository : IRepository<Genre>
{
    IEnumerable<Genre> GetAllGenres();
}
