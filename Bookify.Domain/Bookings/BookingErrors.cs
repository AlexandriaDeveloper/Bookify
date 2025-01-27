using Bookify.Domain.Abstractions;

namespace Bookify.Domain.Bookings;
public static class BookingErrors
{
    public static Error NotFound = new("Booking.Found", "the booking with the specified identifier was not found");
    public static Error Overlap = new("Booking.Overlap", "the booking is overlapping with an existing booking");
    public static Error NotReserved = new("Booking.NotReserved", "the booking is not pending");
    public static Error NotConfirmed = new("Booking.NotConfirmed", "the booking is not confirmed");
    public static Error AlreadyReserved = new("Booking.AlreadyReserved", "the booking is already reserved");
}
