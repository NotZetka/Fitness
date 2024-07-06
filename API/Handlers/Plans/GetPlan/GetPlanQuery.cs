using MediatR;

namespace API.Handlers.Plans.GetPlan
{
    public class GetPlanQuery : IRequest<GetPlanQueryResult>
    {
        public int PlanId { get; set; }
    }
}
