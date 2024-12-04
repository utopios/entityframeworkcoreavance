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
    
    
    [DbFunction("GetFullName", "dbo")]
    public static string GetFullName(int patientId)
    {
        throw new NotImplementedException();
    }
    
    [DbFunction("GetPatientActif", "dbo")]
    public IQueryable<Models.Models.Patient> GetPatientActif()
    {
        return FromExpression(() => GetPatientActif());
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDbFunction(typeof(AppDbContext).GetMethod(nameof(GetFullName)))
            .HasName("GetPatientActif")
            .HasSchema("dbo");

        modelBuilder.HasDbFunction(typeof(AppDbContext).GetMethod(nameof(GetFullName), new[] { typeof(int) }))
            .HasName("GetFullName")
            .HasSchema("dbo");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseLazyLoadingProxies().UseSqlServer("Server=localhost,1433;Database=demo;User Id=sa;Password=YourStrong!Password;TrustServerCertificate=True;")
            .UseLoggerFactory(LoggerFactoryInstance).EnableSensitiveDataLogging();
}