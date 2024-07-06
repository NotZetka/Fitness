using API.Handlers.Plans.AddRecord;
using FluentValidation;

namespace API.Handlers.Plans.AddRecords
{
    public class AddRecordsQueryValidator : AbstractValidator<AddRecordsQuery>
    {
        public AddRecordsQueryValidator()
        {
            RuleFor(x=>x.Records).NotNull().NotEmpty();
        }
    }
}
