using Microsoft.EntityFrameworkCore;

namespace optimisation.DbContexts;

public class AppDbContext : DbContext
{
    private readonly ILogger<AppDbContext> _logger;
    public DbSet<Models.Models.Patient> Patients { get; set; }
    public DbSet<Models.Models.Medecin> Medecins { get; set; }
    public DbSet<Models.Models.Consultation> Consultations { get; set; }
    public DbSet<Models.Models.Prescription> Prescriptions { get; set; }
    public DbSet<Models.Models.Medicament> Medicaments { get; set; }
    
    public static readonly ILoggerFactory LoggerFactoryInstance =
        LoggerFactory.Create(builder => { builder.AddConsole(); });
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=localhost,1433;Database=demo;User Id=sa;Password=YourStrong!Password;TrustServerCertificate=True;")
            .UseLoggerFactory(LoggerFactoryInstance).EnableSensitiveDataLogging();
}