using Microsoft.EntityFrameworkCore;
using TPFilRouge.Context;
using TPFilRouge.Entities;

namespace TPFilRouge.Repositories;

// public class BookingRepository(ReservationSystemContext dbContext)
// {
//     private ReservationSystemContext _dbContext = dbContext;
//
//     public async Task<int> CreateBooking(Booking booking)
//     {
//         using var transaction = await _dbContext.Database.BeginTransactionAsync();
//         try
//         {
//             //Logique métier de recherche 
//             if ((await _dbContext.Rooms.AnyAsync(r => r.Id == booking.RoomId)))
//             {
//                 if (!(await _dbContext.Bookings.AnyAsync(b =>
//                         b.RoomId == booking.RoomId && b.BookingDate.Date == booking.BookingDate.Date)))
//                 {
//                     Booking newBooking = new Booking()
//                     {
//                         RoomId = booking.RoomId,
//                         BookingDate = booking.BookingDate,
//                         ReservedBy = booking.ReservedBy
//                     };
//                     await _dbContext.Bookings.AddAsync(newBooking);
//                     await _dbContext.SaveChangesAsync();
//                     await transaction.CommitAsync();
//                     return newBooking.Id;
//                 }
//
//                 throw new Exception();
//             }
//             throw new Exception();
//         }
//         catch (DbUpdateConcurrencyException)
//         {
//             await transaction.RollbackAsync();
//             throw;
//         }
//     }
// }

public class BookingRepository(DbContext context) : BaseRepository<Booking>(context), IBookingRepository
{
    public async Task<IEnumerable<Booking>> GetBookingsByRoomId(int roomId)
    {
        return await _dbSet.Where(b => b.RoomId == roomId).ToListAsync();
    }
}