using FluentValidation;

namespace API.Handlers.Accounts.List
{
    public class GetAccountsListQueryValidator : AbstractValidator<GetAccountsListQuery>
    {
        public GetAccountsListQueryValidator()
        {
            RuleFor(x => x.PageNumber)
                .Must(x => !x.HasValue || x.Value > 0)
                .WithMessage("PageNumber must be greater than 0 if provided.");

            RuleFor(x => x.PageSize)
                .Must(x => !x.HasValue || x.Value > 0)
                .WithMessage("PageSize must be greater than 0 if provided.");
        }
    }
}
