using democqrs.DbContexts;

namespace democqrs.Events;

public class ProductProjectHandler(QueryDbContext queryDbContext)
{
    private readonly QueryDbContext _queryDbContext = queryDbContext;

    public async Task HandleAsync(object productCreatedEvent)
    {
        //Mise à jour du queryDbContext;
    }
}