using demoentiyframeworkcore.Models;
using Microsoft.EntityFrameworkCore;

namespace demoentiyframeworkcore.Context;

public partial class DemoContext
{
    public DbSet<Product> Products { get; set; }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        //
    }
}