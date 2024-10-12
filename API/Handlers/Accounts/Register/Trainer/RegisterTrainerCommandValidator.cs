using FluentValidation;

namespace API.Handlers.Accounts.Register.Trainer
{
    public class RegisterTrainerCommandValidator : AbstractValidator<RegisterTrainerCommand>
    {
        public RegisterTrainerCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().NotNull().MinimumLength(6);
            RuleFor(x => x.UserName).NotEmpty().NotNull();
            RuleFor(x => x.BankAccountNumber).NotEmpty().NotNull();
        }
    }
}
