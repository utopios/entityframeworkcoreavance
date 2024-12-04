using Microsoft.EntityFrameworkCore;

namespace correction_tp_final.Context;

public partial class AppDbContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder
            .HasDbFunction(() => GetAverageProfessorSalary(default))
            .HasName("GetAverageProfessorSalary")
            .HasSchema("dbo");
    }
    
    [DbFunction("GetAverageProfessorSalary", "dbo")]
    public static decimal? GetAverageProfessorSalary(int departmentId)
    {
        throw new NotSupportedException();
    }
}