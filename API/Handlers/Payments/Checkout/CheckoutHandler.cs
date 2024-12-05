using API.Exceptions;
using API.Utilities.Configurations;
using MediatR;
using Microsoft.Extensions.Options;
using Stripe;

namespace API.Handlers.Payments.Checkout;

public class CheckoutHandler : IRequestHandler<CheckoutQuery, CheckoutResponse>
{
    private readonly StripeSettings _stripeSettings;

    public CheckoutHandler(IOptions<StripeSettings> stripeSettings)
    {
        _stripeSettings = stripeSettings.Value;
    }
    
    public async Task<CheckoutResponse> Handle(CheckoutQuery request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.TokenId))
        {
            throw new BadRequestException("Invalid payment token.");
        }

        var paymentMethodService = new PaymentMethodService();
        var paymentMethod = await paymentMethodService.CreateAsync(new PaymentMethodCreateOptions
        {
            Type = "card",
            Card = new PaymentMethodCardOptions
            {
                Token = request.TokenId, 
            },
        });
        
        var options = new PaymentIntentCreateOptions
        {
            Amount = request.Price,
            Currency = "pln",
            PaymentMethod = paymentMethod.Id,
            ConfirmationMethod = "manual",
            Confirm = true,
            ReceiptEmail = request.Email,
            ReturnUrl = "/"
        };

        var service = new PaymentIntentService();
        await service.CreateAsync(options);

        return new CheckoutResponse();
    }

}