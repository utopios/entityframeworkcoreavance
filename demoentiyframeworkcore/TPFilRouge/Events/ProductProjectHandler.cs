using TPFilRouge.Context;

namespace TPFilRouge.Events;

public class BookingProjectHandler(QueryBookingDbContext queryDbContext)
{
    private readonly QueryBookingDbContext _queryDbContext = queryDbContext;

    public async Task HandleAsync(object bookingCreatedEvent)
    {
        //Mise à jour du queryDbContext;
    }
}