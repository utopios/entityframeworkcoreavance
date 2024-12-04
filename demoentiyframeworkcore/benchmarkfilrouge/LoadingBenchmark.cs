using TPFilRouge.Context;
using TPFilRouge.Entities;

namespace benchmarkfilrouge;


using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;



[MemoryDiagnoser]
public class LoadingBenchmark
{
    private ReservationSystemContext _context;

    [GlobalSetup]
    public void Setup()
    {
        var optionsBuilder = new DbContextOptionsBuilder<ReservationSystemContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=demo;User Id=sa;Password=YourStrong!Password;TrustServerCertificate=True;");
        _context = new ReservationSystemContext(optionsBuilder.Options);
    }

    [Benchmark]
    public async Task<List<Room>> ExplicitLoading()
    {
        var rooms = await _context.Rooms.ToListAsync();

        foreach (var room in rooms)
        {
            await _context.Entry(room).Collection<Booking>(room1 => room1.Bookings).LoadAsync();
        }

        return rooms;
    }
    
    [Benchmark]
    public async Task<List<Room>> EagerLoading()
    {
        var rooms = await _context.Rooms
            .Include(x => x.Bookings)
            .ToListAsync();
        return rooms;
    }
    
    [Benchmark]
    public async Task<List<Room>> ExplicitLoadingANT()
    {
        var rooms = await _context.Rooms.AsNoTracking().ToListAsync();

        foreach (var room in rooms)
        {
            await _context.Entry(room).Collection<Booking>(room1 => room1.Bookings).LoadAsync();
        }

        return rooms;
    }
    
    [Benchmark]
    public async Task<List<Room>> EagerLoadingANT()
    {
        var rooms = await _context.Rooms
            .Include(x => x.Bookings)
            .AsNoTracking()
            .ToListAsync();
        return rooms;
    }
}