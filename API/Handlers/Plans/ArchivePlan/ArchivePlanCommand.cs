using MediatR;

namespace API.Handlers.Plans.ArchivePlan
{
    public class ArchivePlanCommand : IRequest<ArchivePlanResponse>
    {
        public int PlanId { get; set; }
    }
}
