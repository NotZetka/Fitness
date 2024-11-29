using API.Data.Dtos;
using API.Utilities;
using MediatR;

namespace API.Handlers.Plans.GetYourTemplates;

public class GetYourTemplatesQuery : IRequest<PagedResult<FitnessPlanTemplateDto>>
{
    public int PageNumber { get; set; } = 1;
    public int? PageSize { get; set; }
}