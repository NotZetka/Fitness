using FluentValidation;
using Microsoft.IdentityModel.Tokens;

namespace API.Handlers.Plans.Publish
{
    public class PublicPlanCommandValidator : AbstractValidator<PublishPlanCommand>
    {
        public PublicPlanCommandValidator()
        {
            RuleFor(x=>x.Exercises).NotNull().NotEmpty();
            RuleFor(x=>x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Exercises).NotEmpty().NotNull();
            RuleFor(x => x.Exercises.All(x => !x.Name.IsNullOrEmpty()));
        }
    }
}
