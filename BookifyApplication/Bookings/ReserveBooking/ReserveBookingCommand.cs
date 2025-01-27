using BookifyApplication.Abstractions.Messaging;

namespace BookifyApplication.Bookings.ReserveBooking;
public record ReserveBookingCommand(
    Guid ApartmentId,
    Guid UserId,
    DateOnly StartDate,
    DateOnly EndDate
   ) : ICommand<Guid>;