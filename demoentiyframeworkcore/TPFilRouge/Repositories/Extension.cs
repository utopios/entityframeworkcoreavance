using Microsoft.EntityFrameworkCore;
using TPFilRouge.Entities;

namespace TPFilRouge.Repositories;

public static class Extension
{
    public static async Task<IEnumerable<Booking>> GetBookingByRoomId(this BaseRepository<Booking> baseRepository, int roomId)
    {
        return await baseRepository.DbSet.Where(b => b.RoomId == roomId).ToListAsync();
    }
}