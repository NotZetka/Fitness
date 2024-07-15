using API.Data.Dtos;

namespace API.Data.Repositories.UsersRepository
{
    public interface IUsersRepository : IRepository
    {
        Task<AppUser> FindUserByIdAsync(int id);
        Task<AppUser> FindUserByEmailAsync(string email);
        Task<AppUser> FindUserByUsernamelAsync(string email);
        Task<IEnumerable<UserDto>> GetUsersListAsync();
    }
}
