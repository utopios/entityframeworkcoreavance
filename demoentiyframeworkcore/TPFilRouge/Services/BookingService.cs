using TPFilRouge.Entities;
using TPFilRouge.Repositories;

namespace TPFilRouge.Services;

public class BookingService
{
    private readonly IUnitOfWork _unitOfWork;

    public BookingService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task CreateBooking(int roomId, DateTime bookingDate, string reservedBy)
    {
        // var room = await _unitOfWork.Rooms.GetByIdAsync(roomId);
        // if (room == null)
        // {
        //     throw new Exception("Room does not exist.");
        // }

        var existingBooking = await _unitOfWork.Bookings.GetAllAsync();
        foreach (var booking in existingBooking)
        {
            if (booking.RoomId == roomId && booking.BookingDate.Date == bookingDate.Date)
            {
                throw new Exception("Room is already booked for the specified date.");
            }
        }

        var newBooking = new Booking
        {
            RoomId = roomId,
            BookingDate = bookingDate,
            ReservedBy = reservedBy
        };

        await _unitOfWork.Bookings.AddAsync(newBooking);
        await _unitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<Booking>> GetAllBooking()
    {
        return await _unitOfWork.Bookings.GetAllAsync();
    }

    public async Task CancelBooking(int bookingId)
    {
        var booking = await _unitOfWork.Bookings.GetByIdAsync(bookingId);
        if (booking == null)
        {
            throw new Exception("Booking not found.");
        }

        _unitOfWork.Bookings.Delete(booking);
        await _unitOfWork.SaveChangesAsync();
    }
}