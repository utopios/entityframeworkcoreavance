namespace TPFilRouge.Events;

public class BookingCreatedEvent
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public DateTime BookingDate { get; set; }
    public string ReservedBy { get; set; }
}