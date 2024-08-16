using FluentValidation;

namespace API.Handlers.BodyWeight.AddBodyWeightRecord
{
    public class AddBodyWeightRecordCommandValidator : AbstractValidator<AddBodyWeightRecordCommand>
    {
        public AddBodyWeightRecordCommandValidator()
        {
            RuleFor(x=>x.Weight).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
