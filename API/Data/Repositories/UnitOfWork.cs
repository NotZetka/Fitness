using API.Services;
using AutoMapper;

namespace API.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _dataContext;
        public UnitOfWork(DataContext context, IMapper mapper, IUserService userService)
        {
            BodyWeightRepository = new BodyWeightRepository(context,mapper);
            MessageRepository = new MessageRepository(context);
            PlansRepository = new PlansRepository(context);
            PlansTemplateRepository = new PlansTemplateRepository(context, mapper);
            UsersRepository = new UsersRepository(context,mapper,userService);
            _dataContext = context;
        }
        public IBodyWeightRepository BodyWeightRepository { get; private set; }

        public IMessageRepository MessageRepository { get; private set; }
        public IPlansRepository PlansRepository { get; private set; }

        public IPlansTemplateRepository PlansTemplateRepository { get; private set; }

        public IUsersRepository UsersRepository { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _dataContext.SaveChangesAsync();
        }
    }
}
