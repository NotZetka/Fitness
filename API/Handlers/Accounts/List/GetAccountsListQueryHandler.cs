using API.Data.Repositories;
using MediatR;

namespace API.Handlers.Accounts.List
{
    public class GetAccountsListQueryHandler : IRequestHandler<GetAccountsListQuery, GetAccountsListQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountsListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GetAccountsListQueryResponse> Handle(GetAccountsListQuery request, CancellationToken cancellationToken)
        {
            var usersList = await _unitOfWork.UsersRepository.GetUsersListAsync();

            return new GetAccountsListQueryResponse() { Users = usersList };
        }
    }
}
