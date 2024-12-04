using BenchmarkDotNet.Attributes;
using optimisation.DbContexts;
using optimisation.Models;
using optimisation.Services;

namespace benchmark;


[MemoryDiagnoser]
public class LoadingBenchmark
{
    private AppDbContext _appDbContext;
    private DataService _dataService;

    [GlobalSetup]
    public void Setup()
    {
        _dataService = new DataService(new AppDbContext());
    }

    [Benchmark]
    public List<Models.Patient> ExplicitLoad()
    {
        return _dataService.GetPatients();
    }
    
    [Benchmark]
    public List<Models.Patient> EagerLoad()
    {
        return _dataService.GetPatientsEager();
    }
    
}