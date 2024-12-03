using Microsoft.EntityFrameworkCore;
using TPFilRouge.Context;
using TPFilRouge.Entities;
using TPFilRouge.Services;

namespace TPFilRouge.Repositories;

public static class Extension
{
    public static async Task<IEnumerable<Booking>> GetBookingByRoomId(this BaseRepository<Booking> baseRepository, int roomId)
    {
        return await baseRepository.DbSet.Where(b => b.RoomId == roomId).ToListAsync();
    }

    public static void AddRepositories(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<ReservationSystemContext>();
        builder.Services.AddScoped<BaseRepository<Booking>, BookingRepository>();
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    public static void AddServices(this IHostApplicationBuilder builder)
    {
        builder.Services.AddScoped<BookingService>();
    }
}