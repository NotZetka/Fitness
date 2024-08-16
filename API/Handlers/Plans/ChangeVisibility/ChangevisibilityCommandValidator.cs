using FluentValidation;

namespace API.Handlers.Plans.ChangeVisibility
{
    public class ChangevisibilityCommandValidator : AbstractValidator<ChangevisibilityCommand>
    {
        public ChangevisibilityCommandValidator()
        {
            RuleFor(x=>x.TemplateId).NotEmpty().NotNull().GreaterThanOrEqualTo(0);
        }
    }
}
