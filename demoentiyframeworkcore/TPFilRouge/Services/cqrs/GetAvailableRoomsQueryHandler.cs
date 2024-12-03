using Microsoft.EntityFrameworkCore;
using TPFilRouge.Context;
using TPFilRouge.Entities;
using TPFilRouge.Interfaces;
using TPFilRouge.Queries;

namespace TPFilRouge.Services.cqrs;

public class GetAvailableRoomsQueryHandler(QueryBookingDbContext dbContext)
    : IQueryHandler<GetAvailableRoomsQuery, List<Room>>
{

    private readonly QueryBookingDbContext _dbContext = dbContext;

    public async Task<List<Room>> HandleAsync(GetAvailableRoomsQuery query)
    {
        return await _dbContext.Rooms
            .Where(r => r.Capacity >= query.MinimumCapacity)
            .Where(r => r.Bookings.All(b => b.BookingDate != query.BookingDate))
            .ToListAsync();
    }
}