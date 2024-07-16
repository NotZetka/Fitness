using FluentValidation;

namespace API.Handlers.BodyWeight.SetHeight
{
    public class SetHeightQueryValidator : AbstractValidator<SetHeightQuery>
    {
        public SetHeightQueryValidator()
        {
            RuleFor(x => x.Height).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
