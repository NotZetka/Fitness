using MediatR;

namespace API.Handlers.Payments.Checkout;

public class CheckoutQuery : IRequest<CheckoutResponse>
{
    public string? TokenId { get; set; }
    public string Email { get; set; }
    public int Price { get; set; }
    public int PlanId { get; set; }
}

