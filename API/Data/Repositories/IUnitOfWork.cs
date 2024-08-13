namespace API.Data.Repositories
{
    public interface IUnitOfWork
    {
        public IBodyWeightRepository BodyWeightRepository { get; }
        public IMessageRepository MessageRepository { get; }
        public IPlansRepository PlansRepository { get; }
        public IPlansTemplateRepository PlansTemplateRepository { get; }
        public IUsersRepository UsersRepository { get; }
        Task<int> SaveChangesAsync();
    }
}
