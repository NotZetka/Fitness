using API.Data;
using API.Data.Repositories;
using API.Exceptions;
using API.Services;
using MediatR;
using Stripe;

namespace API.Handlers.Payments.Checkout;

public class CheckoutHandler : IRequestHandler<CheckoutQuery, CheckoutResponse>
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;

    public CheckoutHandler(IUserService userService, IUnitOfWork unitOfWork)
    {
        _userService = userService;
        _unitOfWork = unitOfWork;
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
            ReturnUrl = "http://localhost:4200/plans/market"
        };

        var service = new PaymentIntentService();
        await service.CreateAsync(options);

        var user = await _userService.GetCurrentMemberAsync(includeFitnessPlans: true);
        var planTemplate = await _unitOfWork.PlansTemplateRepository.GetByIdAsync(request.PlanId, true);

        if (planTemplate == null) throw new NotFoundException($"Plan with id {request.PlanId} has not been found");
        if (planTemplate.AuthorId != _userService.GetCurrentUserId()) throw new ForbiddenException("You are not allowed to add this plan");

        var exercises = planTemplate.Exercises
            .SelectMany(x => Enumerable.Range(1, x.Sets)
                .Select(_ => new Exercise
                {
                    Name = x.Name,
                    Description = x.Description
                })
            ).ToList();


        var plan = new FitnessPlan
        {
            Archived = false,
            User = user,
            Name = planTemplate.Name,
            Exercises = exercises,
        };

        _unitOfWork.PlansRepository.Add(plan);
        await _unitOfWork.SaveChangesAsync();

        return new CheckoutResponse();
    }

}