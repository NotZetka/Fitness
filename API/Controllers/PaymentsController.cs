using API.Handlers.Payments.Checkout;
using API.Handlers.Plans.AddPlan;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class PaymentsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost("checkout")]
    public async Task<IActionResult> Checkout(CheckoutQuery query)
    {
        if (query.Price == 0) await mediator.Send(new AddPlanCommand { Id = query.PlanId });
        else await mediator.Send(query);

        return Ok();
    }
}