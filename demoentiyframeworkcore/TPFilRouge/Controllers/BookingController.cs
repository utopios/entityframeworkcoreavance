using Microsoft.AspNetCore.Mvc;
using TPFilRouge.Repositories;
using TPFilRouge.Services;

namespace TPFilRouge.Controllers;


public class BookingController : Controller
{
    private readonly BookingService _bookingService;

    public BookingController(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    /// <summary>
    /// Crée une nouvelle réservation.
    /// </summary>
    /// <param name="roomId">Identifiant de la salle</param>
    /// <param name="bookingDate">Date de réservation</param>
    /// <param name="reservedBy">Nom du réservant</param>
    /// <returns>Statut HTTP indiquant le résultat</returns>
    [HttpPost]
    public async Task<IActionResult> CreateBooking([FromBody] BookingRequest bookingRequest)
    {
        if (bookingRequest == null)
        {
            return BadRequest(new { Error = "Invalid booking data." });
        }

        try
        {
            await _bookingService.CreateBooking(
                bookingRequest.RoomId,
                bookingRequest.BookingDate,
                bookingRequest.ReservedBy
            );
            return Ok(new { Message = "Booking created successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    public record BookingRequest
    {
        public int RoomId { get; set; }
        public DateTime BookingDate { get; set; }
        public string ReservedBy { get; set; }
    }


    [HttpGet()]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            var bookings = await _bookingService.GetAllBooking();
            return Ok(bookings);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }

    /// <summary>
    /// Annule une réservation.
    /// </summary>
    /// <param name="bookingId">Identifiant de la réservation</param>
    /// <returns>Statut HTTP indiquant le résultat</returns>
    [HttpDelete("{bookingId}")]
    public async Task<IActionResult> CancelBooking(int bookingId)
    {
        try
        {
            await _bookingService.CancelBooking(bookingId);
            return Ok(new { Message = "Booking cancelled successfully." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Error = ex.Message });
        }
    }
}