using FluentValidation;

namespace API.Handlers.Plans.AddPlan
{
    public class AddPlanCommandValidator : AbstractValidator<AddPlanCommand>
    {
        public AddPlanCommandValidator()
        {
            RuleFor(x=>x.Id).NotNull();
        }
    }
}
