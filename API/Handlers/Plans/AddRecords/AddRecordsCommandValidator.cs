using API.Handlers.Plans.AddRecord;
using FluentValidation;

namespace API.Handlers.Plans.AddRecords
{
    public class AddRecordsCommandValidator : AbstractValidator<AddRecordsCommand>
    {
        public AddRecordsCommandValidator()
        {
            RuleFor(x=>x.Records).NotNull().NotEmpty();
        }
    }
}
