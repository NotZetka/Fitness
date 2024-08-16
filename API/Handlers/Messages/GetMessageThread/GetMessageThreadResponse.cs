using API.Data;
using API.Data.Dtos;

namespace API.Handlers.Messages.GetMessageThread
{
    public class GetMessageThreadResponse
    {
        public IEnumerable<MessageDto> Messages { get; set; }
    }
}
