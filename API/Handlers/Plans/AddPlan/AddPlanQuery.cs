using MediatR;

namespace API.Handlers.Plans.AddPlan
{
    public class AddPlanQuery : IRequest<AddPlanQueryResult>
    {
        public int Id { get; set; }
    }
}
