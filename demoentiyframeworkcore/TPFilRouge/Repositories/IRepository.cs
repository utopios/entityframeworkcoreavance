using TPFilRouge.Entities;

namespace TPFilRouge.Repositories;


public interface IRepository<T> where T: TEntity
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
}