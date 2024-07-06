using FluentValidation;

namespace API.Handlers.Plans.AddPlan
{
    public class AddPlanQueryValidator : AbstractValidator<AddPlanQuery>
    {
        public AddPlanQueryValidator()
        {
            RuleFor(x=>x.Id).NotNull();
        }
    }
}
