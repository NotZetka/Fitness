using API.Data.Repositories;
using MediatR;

namespace API.Handlers.Accounts.List
{
    public class GetAccountsListHandler : IRequestHandler<GetAccountsListQuery, GetAccountsListResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountsListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<GetAccountsListResponse> Handle(GetAccountsListQuery request, CancellationToken cancellationToken)
        {
            var usersList = await _unitOfWork.UsersRepository.GetUsersListAsync();

            return new GetAccountsListResponse() { Users = usersList };
        }
    }
}
