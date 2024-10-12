using FluentValidation;

namespace API.Handlers.Accounts.Register.Member
{
    public class RegisterMemberCommandValidator : AbstractValidator<RegisterMemberCommand>
    {
        public RegisterMemberCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(6);
            RuleFor(x => x.UserName).NotEmpty().NotNull();
            RuleFor(x => x.DateOfBirth).NotEmpty().NotNull();
            RuleFor(x => x.Gender).NotEmpty().NotNull();
        }
    }
}
