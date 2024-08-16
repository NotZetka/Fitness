using MediatR;

namespace API.Handlers.Plans.AddPlan
{
    public class AddPlanCommand : IRequest<AddPlanResponse>
    {
        public int Id { get; set; }
    }
}
