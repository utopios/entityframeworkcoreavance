using Microsoft.EntityFrameworkCore;

namespace cqrs.Models;

public class AppDbContext : DbContext
{
    private readonly ILogger<AppDbContext> _logger;
    public DbSet<Order> Orders { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    
    public static readonly ILoggerFactory LoggerFactoryInstance =
        LoggerFactory.Create(builder => { builder.AddConsole(); });

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer("Server=localhost,1433;Database=ReservationSystem;User Id=sa;Password=YourStrong!Password;TrustServerCertificate=True;")
            .UseLoggerFactory(LoggerFactoryInstance)
            .EnableSensitiveDataLogging(); 
            ;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Relationships Configuration
        modelBuilder.Entity<Order>()
            .HasOne(o => o.Customer)
            .WithMany()
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<OrderItem>()
            .HasOne(oi => oi.Product)
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
    
    public override async ValueTask DisposeAsync()
    {
        Console.WriteLine("DbContext disposed.");
        base.DisposeAsync();
    }
}