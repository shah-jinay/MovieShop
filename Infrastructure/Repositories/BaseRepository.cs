using ApplicationCore.Contracts.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class BaseRepository<T>: IRepository<T> where T: class
{
    protected readonly MovieShopDbContext _movieShopDbContext;
    public BaseRepository(MovieShopDbContext movieShopDbContext)
    {
        _movieShopDbContext = movieShopDbContext;
    }
    public T Insert(T entity)
    {
        _movieShopDbContext.Set<T>().Add(entity);
        _movieShopDbContext.SaveChanges();
        return entity;
    }

    public T DeleteById(int id)
    {
        var entity = _movieShopDbContext.Set<T>().Find(id);
        if (entity != null)
        {
            _movieShopDbContext.Set<T>().Remove(entity);
            _movieShopDbContext.SaveChanges();
            return entity;
        }
        return null;
    }

    public T Update(T entity)
    {
        _movieShopDbContext.Entry(entity).State = EntityState.Modified;
        _movieShopDbContext.SaveChanges();
        return entity;
    }

    public IEnumerable<T> GetAll()
    {
        return _movieShopDbContext.Set<T>().ToList();
    }

    public T GetById(int id)
    {
        return _movieShopDbContext.Set<T>().Find(id);
    }
}