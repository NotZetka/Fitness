using API.Data.Dtos;
using API.Utilities;
using MediatR;

namespace API.Handlers.Plans.GetPlanTemplates
{
    public class GetPlanTemplatesQuery : IRequest<PagedResult<FitnessPlanTemplateDto>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
