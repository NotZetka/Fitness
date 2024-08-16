using MediatR;

namespace API.Handlers.Plans.ChangeVisibility
{
    public class ChangevisibilityCommand : IRequest<ChangevisibilityResponse>
    {
        public int TemplateId { get; set; }
    }
}
