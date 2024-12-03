using Microsoft.EntityFrameworkCore;
using optimisation.DbContexts;

namespace optimisation.Services;

public class DataService(AppDbContext appDbContext)
{
    private readonly AppDbContext _appDbContext = appDbContext;

    public List<Models.Models.Patient> GetPatients()
    {
        //var patients = _appDbContext.Patients.ToList();
        var patients = _appDbContext.Patients.AsNoTracking().ToList();
        return patients;
    }
    
    public Models.Models.Patient GetPatient(int id)
    {
        var patient = _appDbContext.Patients.Find(id);
        _appDbContext.Entry(patient).State = EntityState.Detached;
    }
}