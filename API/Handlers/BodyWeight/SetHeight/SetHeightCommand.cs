using MediatR;

namespace API.Handlers.BodyWeight.SetHeight
{
    public class SetHeightCommand : IRequest<SetHeightResponse>
    {
        public int Height { get; set; }
    }
}
