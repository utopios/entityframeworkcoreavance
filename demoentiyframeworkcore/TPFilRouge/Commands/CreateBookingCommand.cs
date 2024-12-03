namespace TPFilRouge.Commands;

public class CreateBookingCommand
{
    public int RoomId { get; set; }
    public DateTime BookingDate { get; set; }
    public string ReservedBy { get; set; }
}
