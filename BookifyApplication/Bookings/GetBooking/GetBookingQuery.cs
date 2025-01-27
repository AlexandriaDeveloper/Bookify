using BookifyApplication.Abstractions.Messaging;

namespace BookifyApplication.Bookings.GetBooking;
public sealed record GetBookingQuery(Guid bookingId) : IQuery<BookingResponse>
{
}
