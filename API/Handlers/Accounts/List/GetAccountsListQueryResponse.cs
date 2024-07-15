using API.Data.Dtos;

namespace API.Handlers.Accounts.List
{
    public class GetAccountsListQueryResponse
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}
