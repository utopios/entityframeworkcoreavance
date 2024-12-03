using democqrs.Models;
using Microsoft.EntityFrameworkCore;

namespace democqrs.DbContexts;

public class CommandDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
}