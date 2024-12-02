using TPFilRouge.Entities;

namespace TPFilRouge.Repositories;

public interface IUnitOfWork
{
    BaseRepository<Booking> Bookings { get; }

    Task saveChangeAsync();
}