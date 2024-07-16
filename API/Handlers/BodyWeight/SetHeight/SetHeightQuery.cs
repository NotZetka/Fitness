using MediatR;

namespace API.Handlers.BodyWeight.SetHeight
{
    public class SetHeightQuery : IRequest<SetHeightQueryResponse>
    {
        public int Height { get; set; }
    }
}
