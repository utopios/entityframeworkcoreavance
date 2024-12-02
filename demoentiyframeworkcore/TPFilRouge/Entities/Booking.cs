using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TPFilRouge.Entities;

public partial class Booking
{
    public int Id { get; set; }

   
    public int RoomId { get; set; }

    public DateTime BookingDate { get; set; }

    [Required]
    public string ReservedBy { get; set; } = null!;

    public virtual Room Room { get; set; } = null!;
}
