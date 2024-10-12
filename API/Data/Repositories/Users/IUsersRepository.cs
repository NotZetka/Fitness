using API.Data.Dtos;
using API.Utilities;

namespace API.Data.Repositories
{
    public interface IUsersRepository
    {
        Task<AppUserBase> FindUserByIdAsync(int id);
        Task<AppUserBase> FindUserByEmailAsync(string email);
        Task<AppUserBase> FindUserByUsernamelAsync(string email);
        Task<PagedResult<UserDto>> GetUsersListAsync(int? pageNumber = null, int? pageSize = null);
    }
}
