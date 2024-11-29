using API.Data.Dtos;
using MediatR;

namespace API.Handlers.Plans.EditPlanTemplate;

public class EditPlanTemplateCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }
        
    public decimal Price { get; set; }
        
    public bool Public { get; set; }

    public IEnumerable<ExerciseTemplateDto> Exercises { get; set; }
}