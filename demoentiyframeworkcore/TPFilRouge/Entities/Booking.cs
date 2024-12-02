using System;
using System.Collections.Generic;

namespace TPFilRouge.Entities;

public partial class Booking
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public DateTime BookingDate { get; set; }

    public string ReservedBy { get; set; } = null!;

    public byte[] RowVersion { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
