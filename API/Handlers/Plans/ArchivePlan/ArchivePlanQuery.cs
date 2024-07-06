using MediatR;

namespace API.Handlers.Plans.ArchivePlan
{
    public class ArchivePlanQuery : IRequest<ArchivePlanQueryResult>
    {
        public int PlanId { get; set; }
    }
}
