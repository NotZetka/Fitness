namespace API.Data.Repositories
{
    public interface IMessageRepository : IRepository<Message>
    {
        Task<IEnumerable<Message>> GetMessageThreadAsync(int firstUserId, int secondUserId);
    }
}
