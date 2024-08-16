using FluentValidation;

namespace API.Handlers.BodyWeight.SetHeight
{
    public class SetHeightCommandValidator : AbstractValidator<SetHeightCommand>
    {
        public SetHeightCommandValidator()
        {
            RuleFor(x => x.Height).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
