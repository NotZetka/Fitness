using API.Data.Dtos;

namespace API.Handlers.Plans.GetPlanTemplates
{
    public class GetPlanTemplatesQueryResult
    {
        public IEnumerable<FitnessPlanTemplateDto> Plans { get; set; }
    }
}
