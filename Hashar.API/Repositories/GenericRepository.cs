using Hashar.API.Data;
using System.Linq.Expressions;

namespace Hashar.API.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async ValueTask<TEntity> AddAsync(TEntity entity)
    {
        var entry = await _context.Set<TEntity>().AddAsync(entity);

        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression)
        => _context.Set<TEntity>().Where(expression);

    public IQueryable<TEntity> GetAll()
        => _context.Set<TEntity>();

    public TEntity? GetById(object id)
        => _context.Set<TEntity>().Find(id);

    public async ValueTask<TEntity> Remove(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Remove(entity);

        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public async ValueTask<TEntity> Update(TEntity entity)
    {
        var entry = _context.Set<TEntity>().Update(entity);

        await _context.SaveChangesAsync();

        return entry.Entity;
    }

    public int Save() => _context.SaveChanges();

    public async ValueTask<int> SaveAsync() => await _context.SaveChangesAsync();
}
