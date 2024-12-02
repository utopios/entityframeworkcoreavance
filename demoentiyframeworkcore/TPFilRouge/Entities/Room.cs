using System;
using System.Collections.Generic;

namespace TPFilRouge.Entities;

public partial class Room
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Capacity { get; set; }

    public int CompanyId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Company Company { get; set; } = null!;
}
