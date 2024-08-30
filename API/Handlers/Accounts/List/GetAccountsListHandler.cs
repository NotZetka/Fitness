using API.Data.Dtos;
using API.Data.Repositories;
using API.Utilities;
using MediatR;

namespace API.Handlers.Accounts.List
{
    public class GetAccountsListHandler : IRequestHandler<GetAccountsListQuery, PagedResult<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAccountsListHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PagedResult<UserDto>> Handle(GetAccountsListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.UsersRepository.GetUsersListAsync();
        }
    }
}
