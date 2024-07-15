using FluentValidation;

namespace API.Handlers.Messages.GetMessageThread
{
    public class GetMessageThreadQueryValidator : AbstractValidator<GetMessageThreadQuery>
    {
        public GetMessageThreadQueryValidator()
        {
            RuleFor(x => x.userId).NotEmpty().NotNull();
        }
    }
}
