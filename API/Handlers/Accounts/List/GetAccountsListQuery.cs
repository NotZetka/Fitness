using API.Data.Dtos;
using API.Utilities;
using MediatR;

namespace API.Handlers.Accounts.List
{
    public class GetAccountsListQuery : IRequest<PagedResult<UserDto>>
    {
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
