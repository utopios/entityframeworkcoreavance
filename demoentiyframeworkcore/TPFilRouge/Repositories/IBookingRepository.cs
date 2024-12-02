using TPFilRouge.Entities;

namespace TPFilRouge.Repositories;

public interface IBookingRepository: IRepository<Booking>
{
    Task<IEnumerable<Booking>> GetBookingsByRoomId(int roomId);
}