namespace API.Data.Repositories.MessagesRepository
{
    public interface IMessageRepository : IRepository
    {
        void Add(Message message);

        Task<IEnumerable<Message>> GetMessageThreadAsync(int firstUserId, int secondUserId);
    }
}
