namespace cqrs.Models;

public class Order
{
    public int Id { get; set; }
    public int CustomerId { get; set; }
    public virtual Customer Customer { get; set; }
    public virtual ICollection<OrderItem> OrderItems { get; set; }
    public string Status { get; set; }
    public DateTime Date { get; set; }
}