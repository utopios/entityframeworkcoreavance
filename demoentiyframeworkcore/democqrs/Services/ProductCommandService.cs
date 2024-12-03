using democqrs.DbContexts;
using democqrs.Events;
using democqrs.Models;

namespace democqrs.Services;

public class ProductCommandService(CommandDbContext commandDbContext, IEventPublisher eventPublisher)
{
    private readonly CommandDbContext _commandDbContext = commandDbContext;
    private readonly IEventPublisher _eventPublisher = eventPublisher;
    public async Task CreateProductAsync(Product product)
    {
        await _commandDbContext.Products.AddAsync(product);
        await _commandDbContext.SaveChangesAsync();
        await _eventPublisher.PublishAsync(new ProductCreatedEvent
            { Id = product.Id, Name = product.Name, Price = product.Price });
    }
}