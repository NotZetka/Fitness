using API.Data.Dtos;
using MediatR;

namespace API.Handlers.Plans.Publish
{
    public class PublishPlanQuery : IRequest<PublishPlanQueryResult>
    {
        public string Name { get; set; }
        public bool Public { get; set; }
        public IEnumerable<ExerciseTemplateDto> Exercises { get; set; }
    }
}
