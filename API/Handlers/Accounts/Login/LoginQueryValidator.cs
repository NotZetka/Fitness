using FluentValidation;

namespace API.Handlers.Accounts.Login
{
    public class LoginQueryValidator : AbstractValidator<LoginQuery>
    {
        public LoginQueryValidator()
        {
            RuleFor(x=>x.UsernameOrEmail).NotEmpty().NotNull();
            RuleFor(x=>x.Password).NotEmpty().NotNull();
        }
    }
}
