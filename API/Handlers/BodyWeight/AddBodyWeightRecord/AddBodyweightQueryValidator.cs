using FluentValidation;

namespace API.Handlers.BodyWeight.AddBodyWeightRecord
{
    public class AddBodyweightQueryValidator : AbstractValidator<AddBodyWeightRecordQuery>
    {
        public AddBodyweightQueryValidator()
        {
            RuleFor(x=>x.Weight).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
