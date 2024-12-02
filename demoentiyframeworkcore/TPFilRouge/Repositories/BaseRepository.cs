using Microsoft.EntityFrameworkCore;
using TPFilRouge.Repositories;

namespace TPFilRouge.Entities;

public abstract class BaseRepository<T> : IRepository<T> where T : TEntity
{
    private readonly DbContext _context;
    protected readonly DbSet<T> _dbSet;

    public DbSet<T> DbSet
    {
        get => _dbSet;
    }
    protected BaseRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
}