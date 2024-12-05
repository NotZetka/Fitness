using API.Data.Dtos;
using MediatR;

namespace API.Handlers.Plans.CreatePlanTemplate
{
    public class CreatePlanTemplateCommand : IRequest<CreatePlanTemplateResponse>
    {
        public string Name { get; set; }
        
        public decimal Price { get; set; }
        
        public bool Public { get; set; }

        public IEnumerable<ExerciseTemplateDto> Exercises { get; set; }
    }
}
