namespace democqrs.Events;

public class ProductCreatedEvent
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}