using API.Data.Dtos;

namespace API.Handlers.Accounts.List
{
    public class GetAccountsListResponse
    {
        public IEnumerable<UserDto> Users { get; set; }
    }
}
