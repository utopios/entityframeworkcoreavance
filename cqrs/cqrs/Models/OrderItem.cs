

using System.Text.Json.Serialization;

namespace cqrs.Models;

public class OrderItem
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    
    [JsonIgnore]
    public virtual Order Order { get; set; }
    public int ProductId { get; set; }
    public virtual Product Product { get; set; }
    public int Quantity { get; set; }
}