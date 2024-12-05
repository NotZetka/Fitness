using API.Handlers.Payments.Checkout;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PaymentsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout(CheckoutQuery query)
    {
        await mediator.Send(query);
        return Ok();
    }
}