using FluentValidation;

namespace API.Handlers.Plans.ArchivePlan
{
    public class ArchivePlanCommandValidator : AbstractValidator<ArchivePlanCommand>
    {
        public ArchivePlanCommandValidator()
        {
            RuleFor(x=>x.PlanId).NotEmpty().NotNull().GreaterThanOrEqualTo(0);
        }
    }
}
