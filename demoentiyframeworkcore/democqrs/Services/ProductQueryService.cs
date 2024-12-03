using democqrs.DbContexts;
using democqrs.Models;

namespace democqrs.Services;

public class ProductQueryService(QueryDbContext queryDbContext)
{
    private readonly QueryDbContext _queryDbContext = queryDbContext;

    public async Task<Product> GetProductAsyncById(int id)
    {
        return await _queryDbContext.Products.FindAsync(id);
    }
}