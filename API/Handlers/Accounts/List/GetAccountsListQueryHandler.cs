using API.Data.Repositories.UsersRepository;
using MediatR;

namespace API.Handlers.Accounts.List
{
    public class GetAccountsListQueryHandler : IRequestHandler<GetAccountsListQuery, GetAccountsListQueryResponse>
    {
        private readonly IUsersRepository _usersRepository;

        public GetAccountsListQueryHandler(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<GetAccountsListQueryResponse> Handle(GetAccountsListQuery request, CancellationToken cancellationToken)
        {
            var usersList = await _usersRepository.GetUsersListAsync();

            return new GetAccountsListQueryResponse() { Users = usersList };
        }
    }
}
