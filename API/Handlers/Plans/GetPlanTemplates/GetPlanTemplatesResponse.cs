using API.Data.Dtos;

namespace API.Handlers.Plans.GetPlanTemplates
{
    public class GetPlanTemplatesResponse
    {
        public IEnumerable<FitnessPlanTemplateDto> Plans { get; set; }
    }
}
