using API.Data.Dtos;
using API.Utilities;

namespace API.Data.Repositories
{
    public interface IUsersRepository
    {
        Task<AppUser> FindUserByIdAsync(int id);
        Task<AppUser> FindUserByEmailAsync(string email);
        Task<AppUser> FindUserByUsernamelAsync(string email);
        Task<PagedResult<UserDto>> GetUsersListAsync(int? pageNumber = null, int? pageSize = null);
    }
}
