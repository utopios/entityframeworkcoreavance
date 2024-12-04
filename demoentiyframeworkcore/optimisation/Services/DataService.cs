using Microsoft.EntityFrameworkCore;
using optimisation.DbContexts;

namespace optimisation.Services;

public class DataService(AppDbContext appDbContext)
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public DbSet<Models.Models.Patient> Patients => _appDbContext.Patients;

    public List<Models.Models.Patient> GetPatients()
    {
        //var patients = _appDbContext.Patients.ToList();
        // var patients = _appDbContext.Patients
        //     .Include(p => p.Consultations)
        //     .ThenInclude(c => c.Prescriptions)
        //     .ThenInclude(p => p.Medicament)
        //     .AsNoTracking()
        //     .Where(p => p.DateNaissance >= DateTime.Now.AddYears(-30))
        //     // .Select(p => new
        //     // {
        //     //     Name = p.Nom + " "+p.Prenom,
        //     //     Consultations = p.Consultations,
        //     //     Medicament = p.Consultations.First().Prescriptions.First().Medicament
        //     // })
        //     .AsSplitQuery()
        //     .ToList();
        // var consultations = patients.FirstOrDefault()!.Consultations;
        //return (List<object>)patients.Cast<object>();
        var patients = _appDbContext.Patients.Where(p => p.DateNaissance >= DateTime.Now.AddYears(-30));
        foreach (var patient in patients.AsEnumerable())
        {
            _appDbContext.Entry(patient).Collection(p => p.Consultations).Load();
        }

        return patients.AsEnumerable().ToList();
    }
    
    public List<Models.Models.Patient> GetPatientsEager()
    {
        
        var patients = _appDbContext.Patients
            .Include(p => p.Consultations)
            .ThenInclude(c => c.Prescriptions)
            .ThenInclude(p => p.Medicament)
            .Where(p => p.DateNaissance >= DateTime.Now.AddYears(-30))
            .ToList();

        return patients;
    }
    
    
    
    public Models.Models.Patient GetPatient(int id)
    {
        var patient = _appDbContext.Patients.Find(id);
        _appDbContext.Entry(patient).State = EntityState.Detached;
        return patient;
    }
}