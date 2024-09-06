using MediatR;

namespace API.Handlers.BodyWeight.GetBodyWeight
{
    public class GetBodyWeightQuery : IRequest<GetBodyWeightResponse>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
