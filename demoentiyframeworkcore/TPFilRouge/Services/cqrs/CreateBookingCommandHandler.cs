using System.Windows.Input;
using Microsoft.EntityFrameworkCore;
using TPFilRouge.Commands;
using TPFilRouge.Context;
using TPFilRouge.Entities;
using TPFilRouge.Interfaces;

namespace TPFilRouge.Services.cqrs;

public class CreateBookingCommandHandler(CommandBookingDbContext dbContext) : ICommandHandler<CreateBookingCommand>
{

    private readonly CommandBookingDbContext _dbContext = dbContext;

    public async Task HandleAsync(CreateBookingCommand command)
    {
        var room = await _dbContext.Rooms.FindAsync(command.RoomId);

        if (room == null)
            throw new Exception("Room not found.");

        var isAvailable = !await _dbContext.Bookings.AnyAsync(b => 
            b.RoomId == command.RoomId && b.BookingDate == command.BookingDate);

        if (!isAvailable)
            throw new Exception("Room is not available for the selected date.");

        var booking = new Booking
        {
            RoomId = command.RoomId,
            BookingDate = command.BookingDate,
            ReservedBy = command.ReservedBy
        };

        _dbContext.Bookings.Add(booking);
        await _dbContext.SaveChangesAsync();
    }
}