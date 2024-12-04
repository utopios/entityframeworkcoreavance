using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using optimisation.DbContexts;
using optimisation.Models;
using optimisation.Services;

namespace optimisation.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DataService _dataService;
    private readonly AppDbContext _appDbContext;
    public HomeController(ILogger<HomeController> logger, DataService dataService, AppDbContext appDbContext)
    {
        _logger = logger;
        _dataService = dataService;
        _appDbContext = appDbContext;
    }

    public IActionResult Index()
    {
        var patients = _dataService.Patients.Select(p => new {Fullname = AppDbContext.GetFullName(p.Id)});
        var patientsActif = _appDbContext.GetPatientActif();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}