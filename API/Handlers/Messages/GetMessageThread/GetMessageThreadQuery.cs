using MediatR;

namespace API.Handlers.Messages.GetMessageThread
{
    public class GetMessageThreadQuery : IRequest<GetMessageThreadQueryResponse>
    {
        public int userId { get; set; }
    }
}
