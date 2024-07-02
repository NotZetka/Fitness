using FluentValidation;

namespace API.Handlers.Accounts.Register
{
    public class RegisterQueryValidator : AbstractValidator<RegisterQuery>
    {
        public RegisterQueryValidator()
        {
            RuleFor(x=>x.Email).NotEmpty().NotNull().Must(mail=>mail.Contains('@'));
            RuleFor(x=>x.Password).NotEmpty().NotNull().MinimumLength(6);
            RuleFor(x=>x.UserName).NotEmpty().NotNull();
            RuleFor(x=>x.DateOfBirth).NotEmpty().NotNull();
            RuleFor(x=>x.Gender).NotEmpty().NotNull();
        }
    }
}
