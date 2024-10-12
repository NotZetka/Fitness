using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace API.Handlers.Plans.CreatePlan
{
    public class CreatePlanCommandValidator : AbstractValidator<CreatePlanCommand>
    {
        public CreatePlanCommandValidator()
        {
            RuleFor(x => x.Exercises).NotNull().NotEmpty();
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Exercises).NotEmpty().NotNull();
            RuleFor(x => x.Exercises.All(x => !x.Name.IsNullOrEmpty()));
        }
    }
}
