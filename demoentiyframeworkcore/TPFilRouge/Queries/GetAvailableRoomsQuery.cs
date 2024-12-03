namespace TPFilRouge.Queries;

public class GetAvailableRoomsQuery
{
    public DateTime BookingDate { get; set; }
    public int MinimumCapacity { get; set; }

    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}
