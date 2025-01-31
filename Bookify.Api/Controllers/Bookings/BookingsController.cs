using BookifyApplication.Bookings.GetBooking;
using BookifyApplication.Bookings.ReserveBooking;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Bookings;
[Route("api/bookings")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly ISender _sender;

    public BookingsController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBooking(Guid id, CancellationToken cancellationToken)
    {
        var query = new GetBookingQuery(id);
        var result = await _sender.Send(query, cancellationToken);
        return result.IsSuccess ? Ok(result.Value) : NotFound();
    }
    [HttpPost]
    public async Task<IActionResult> ReserveBooking( ReserveBookingRequest request, CancellationToken cancellationToken)
    {
        var command = new ReserveBookingCommand(request.ApartmentId, request.UserId, request.StartDate, request.EndDate);
        var result = await _sender.Send(command, cancellationToken);
        return result.IsSuccess 
            ? CreatedAtAction(nameof(GetBooking),new {id =result.Value},result.Value) 
            : BadRequest(result.Error);
    }
}
