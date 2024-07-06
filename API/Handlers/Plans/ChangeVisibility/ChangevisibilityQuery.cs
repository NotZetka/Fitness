using MediatR;

namespace API.Handlers.Plans.ChangeVisibility
{
    public class ChangevisibilityQuery : IRequest<ChangevisibilityQueryResult>
    {
        public int TemplateId { get; set; }
    }
}
