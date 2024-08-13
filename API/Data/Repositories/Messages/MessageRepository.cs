
using Microsoft.EntityFrameworkCore;

namespace API.Data.Repositories
{
    public class MessageRepository(DataContext context) : AbstractRepository<Message>(context), IMessageRepository
    {
        public async Task<IEnumerable<Message>> GetMessageThreadAsync(int firstUserId, int secondUserId)
        {
            return await _dbSet
                .Where(x=> 
                (x.SenderId == firstUserId && x.ReceiverId == secondUserId) ||
                (x.SenderId == secondUserId && x.ReceiverId == firstUserId))
                .OrderBy(x=>x.DateSend)
                .ToListAsync();
        }
    }
}
