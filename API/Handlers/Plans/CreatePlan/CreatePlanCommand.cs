using API.Data.Dtos;
using MediatR;

namespace API.Handlers.Plans.CreatePlan
{
    public class CreatePlanCommand : IRequest<CreatePlanResponse>
    {
        public string Name { get; set; }
        public bool Public { get; set; }
        public IEnumerable<ExerciseTemplateDto> Exercises { get; set; }
    }
}
