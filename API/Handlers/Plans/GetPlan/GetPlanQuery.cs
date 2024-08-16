using MediatR;

namespace API.Handlers.Plans.GetPlan
{
    public class GetPlanQuery : IRequest<GetPlanResponse>
    {
        public int PlanId { get; set; }
    }
}
