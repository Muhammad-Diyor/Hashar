using System.Linq.Expressions;

namespace Hashar.API.Repositories;

public interface IGenericRepository<TEntity> where TEntity : class
{
    TEntity? GetById(object id);
    IQueryable<TEntity> GetAll();
    IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> expression);
    ValueTask<TEntity> AddAsync(TEntity entity);
    ValueTask<TEntity> Remove(TEntity entity);
    ValueTask<TEntity> Update(TEntity entity);
    int Save();
    ValueTask<int> SaveAsync();
}