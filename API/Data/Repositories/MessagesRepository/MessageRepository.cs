
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories.MessagesRepository
{
    public class MessageRepository : AbstractRepository, IMessageRepository
    {
        public MessageRepository(DataContext context) : base(context) { }

        public void Add(Message message)
        {
            _context.Messages.Add(message);
        }

        public async Task<IEnumerable<Message>> GetMessageThreadAsync(int firstUserId, int secondUserId)
        {
            return await _context.Messages
                .Where(x=> 
                (x.SenderId == firstUserId && x.ReceiverId == secondUserId) ||
                (x.SenderId == secondUserId && x.ReceiverId == firstUserId))
                .OrderBy(x=>x.DateSend)
                .ToListAsync();
        }
    }
}
