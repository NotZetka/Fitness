using MediatR;

namespace API.Handlers.Messages.GetMessageThread
{
    public class GetMessageThreadQuery : IRequest<GetMessageThreadResponse>
    {
        public int userId { get; set; }
    }
}
