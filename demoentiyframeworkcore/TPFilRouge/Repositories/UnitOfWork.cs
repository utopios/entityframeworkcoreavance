using TPFilRouge.Context;
using TPFilRouge.Entities;

namespace TPFilRouge.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ReservationSystemContext _context;
    public BaseRepository<Booking> Bookings { get; set; }
    public UnitOfWork(ReservationSystemContext context, BaseRepository<Booking> bookings)
    {
        _context = context;
        Bookings = bookings;
    }

    
    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
    
}