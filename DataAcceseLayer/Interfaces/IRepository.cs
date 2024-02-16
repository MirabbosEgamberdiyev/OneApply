

using DataAcceseLayer.Entities;

namespace DataAcceseLayer.Interfaces;

public interface IRepository<TEntity> where TEntity : BaseEntity
{
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}
